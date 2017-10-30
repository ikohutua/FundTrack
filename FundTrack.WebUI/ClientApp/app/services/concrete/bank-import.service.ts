import { Injectable } from "@angular/core";
import { Http, RequestOptions, Headers } from "@angular/http";
import { Observable } from "rxjs/Observable";
import { PrivatSessionViewModel } from "../../view-models/concrete/privat-session-view.model";
import { Response } from '@angular/http';
import { Md5 } from 'ts-md5/dist/md5';
import * as xml2js from 'xml2js';
import sha1 = require('sha1');
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch';
import { ImportDetailPrivatViewModel, ImportPrivatViewModel } from "../../view-models/concrete/import-privat-view.model";
import { DataRequestPrivatViewModel } from "../../view-models/concrete/data-request-privat-view.model";
import { BankImportSearchViewModel } from "../../view-models/concrete/finance/bank-import-search-view.model";
import * as key from '../../shared/key.storage';
import { isBrowser } from "angular2-universal";
import { BaseSpinnerService } from "../abstract/base-spinner-service";
import { SpinnerComponent } from "../../shared/components/spinner/spinner.component";
import { GlobalUrlService } from "./global-url.service";

@Injectable()
export class BankImportService extends BaseSpinnerService<ImportDetailPrivatViewModel >{

    public constructor(private _http: Http) {
        super(_http);
    }

    /**
     * method for get bank import from privat24 using api
     * @param dataForRequest
     */
    public getUserExtracts(dataForRequest: DataRequestPrivatViewModel): Observable<ImportPrivatViewModel> {

        if (this.checkAuthorization()) {
            let data = `<oper>cmt</oper>
                        <wait>0</wait>
                        <test>1</test>
                        <payment id="">
                          <prop name="sd" value="${dataForRequest.dataFrom}" />
                          <prop name="ed" value="${dataForRequest.dataTo}" />
                          <prop name="card" value="${dataForRequest.card}" />
                        </payment>`;

            let sign = <string>sha1(<string>Md5.hashStr(data + dataForRequest.password));

            let body = `<?xml version="1.0" encoding="UTF-8"?>
                    <request version="1.0">
                      <merchant>
                        <id>${dataForRequest.idMerchant}</id>
                        <signature>${sign}</signature>
                      </merchant>
                      <data>
                        ${data}
                      </data>
                    </request>`;

            return this._http.post("https://api.privatbank.ua/p24api/rest_fiz", body)
                .map((response: Response) => {
                    let imports = new ImportPrivatViewModel();
                    xml2js.parseString(response.text(), function (err, result) {
                        if (result.response.data[0].hasOwnProperty('error') == false) {
                            for (let i = 0; i < result.response.data[0].info[0].statements[0].statement.length; ++i) {
                                let temp: ImportDetailPrivatViewModel = new ImportDetailPrivatViewModel();
                                let dates = result.response.data[0].info[0].statements[0].statement[i].$.trandate.split('-');
                                let times = result.response.data[0].info[0].statements[0].statement[i].$.trantime.split(':');
                                temp.card = result.response.data[0].info[0].statements[0].statement[i].$.card as string;
                                temp.amount = result.response.data[0].info[0].statements[0].statement[i].$.amount as string;
                                temp.cardAmount = result.response.data[0].info[0].statements[0].statement[i].$.cardamount as string;
                                temp.rest = result.response.data[0].info[0].statements[0].statement[i].$.rest as string;
                                temp.terminal = result.response.data[0].info[0].statements[0].statement[i].$.terminal as string;
                                temp.description = result.response.data[0].info[0].statements[0].statement[i].$.description as string;
                                temp.appCode = result.response.data[0].info[0].statements[0].statement[i].$.appcode as string;
                                temp.trandate = new Date(+dates[0], (+dates[1]) - 1, +dates[2], (+times[0]) + 3, +times[1], +times[2]);
                                temp.isLooked = false;
                                temp.id = 0;
                                imports.importsDetail.push(temp);
                            }
                        }
                        else {
                            imports.error = result.response.data[0].error[0].$.message as string
                        }
                    });
                    return imports;
                })
                .catch(this.handleError)
        }
    }

    /**
     * method for register bank importss in db
     * @param bankImport
     */
    public registerBankExtracts(bankImport: ImportDetailPrivatViewModel[]): Observable<ImportDetailPrivatViewModel[]> {
        if (this.checkAuthorization()) {
            let url = 'api/BankImport/RegisterNewExtracts';
            return this._http.post(url, bankImport, this.getRequestOptions())
                .map((response: Response) => <ImportDetailPrivatViewModel[]>response.json())
                .catch(this.handleError);
        }
    }

    /**
     * method for get bankImports which satisfy filters
     * @param bankSearchModel
     */
    public getRawExtracts(bankSearchModel: BankImportSearchViewModel): Observable<ImportDetailPrivatViewModel[]> {
        if (this.checkAuthorization()) {
            let url = "api/BankImport/SearchRawExtractsOnPeriod";
            return this._http.post(url, bankSearchModel, this.getRequestOptions())
                .map((response: Response) => <ImportDetailPrivatViewModel[]>response.json())
                .catch(this.handleError);
        }
    }

    /**
     * method for get all bank imports fin one org accounts
     * @param card
     */
    public getAllExtracts(card: string, spinner?: SpinnerComponent): Observable<ImportDetailPrivatViewModel[]> {
        if (this.checkAuthorization()) {
            let url = 'api/BankImport/GetAllExtracts/' + card;
            return super.getCollection(url, this.getRequestOptions(), spinner)
        }
    }

     /**
     * method for get the count all bank imports fin one org accounts
     * @param card
     */
    public getCountExtractsOnCard(card: string): Observable<number> {
        if (this.checkAuthorization()) {
            let url = 'api/BankImport/GetCountExtracts';
            return this._http.get(url + '/' + card, this.getRequestOptions())
                .map((response: Response) => <number>response.json())
                .catch(this.handleError);
        }
    }

    /**
    * Create RequestOptions
    */
    private getRequestOptions() {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        headers.append("Authorization", "Bearer " + localStorage.getItem(key.keyToken));
        let options = new RequestOptions({ headers: headers });
        return options;
    }

    public getAllSuggestedBankImports(amount: number, date: Date): Observable<ImportDetailPrivatViewModel[]> {
        if (this.checkAuthorization()) {
            //debugger;
            return this._http.get(GlobalUrlService.getAllSuggestedBankImportUrl + '/' + amount + '/' + date, this.getRequestOptions())
                .map((response: Response) => <ImportDetailPrivatViewModel[]>response.json())
                .catch(this.handleError);
        }
    }

    /**
    * Catch error
    * @param error
    */
    private handleError(error: Response): any {
        return Observable.throw(error.json().error);
    }

    private checkAuthorization(): boolean {
        if (isBrowser) {
            if (localStorage.getItem(key.keyToken)) {
                return true;
            }
            return false;
        };
    }
}
