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
import { RequestOptionsService } from "./request-options.service";
import { GlobalUrlService } from "./global-url.service";

@Injectable()
export class BankImportService extends BaseSpinnerService<ImportDetailPrivatViewModel>{

    public constructor(private _http: Http) {
        super(_http);
    }

    /**
     * method for get bank import from privat24 using api
     * @param dataForRequest
     */
    public getUserExtracts(orgAccountId: number, spinner?: SpinnerComponent) {
        if (this.checkAuthorization()) {
            return this._http.post(GlobalUrlService.PrivatExtract, orgAccountId, RequestOptionsService.getRequestOptions())
                .map((response: Response) => response.json());
        }
    }

    public getPrivatExtracts(data: DataRequestPrivatViewModel, spinner?: SpinnerComponent) {
        if (this.checkAuthorization()) {
            return this._http.post(GlobalUrlService.PrivatExtractWithDate, data, RequestOptionsService.getRequestOptions())
                .map((response: Response) => response.json());
        }
    }

    public UpdateDate(orgId: number):Observable<Date> {
        if (this.checkAuthorization()) {
            return this._http.get(GlobalUrlService.UpdateDate + '/' + orgId , RequestOptionsService.getRequestOptions())
                .map((response: Response) => response.json() as Date);
        }
    }

    public getLastPrivatUpdate(orgId: number): Observable<Date> {
        if (this.checkAuthorization()) {
            return this._http.get(GlobalUrlService.LastUpdate + '/' + orgId, RequestOptionsService.getRequestOptions())
                .map((response: Response) => { return response.json() as Date })
                .catch(this.handleError);
        }
    }


    /**
     * method for register bank importss in db
     * @param bankImport
     */
    public registerBankExtracts(bankImport: ImportDetailPrivatViewModel[]): Observable<ImportDetailPrivatViewModel[]> {
        if (this.checkAuthorization()) {
            return this._http.post(GlobalUrlService.RegisterNewExtracts, bankImport, RequestOptionsService.getRequestOptions())
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
            return this._http.post(url, bankSearchModel, RequestOptionsService.getRequestOptions())
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
            return super.getCollection(url, RequestOptionsService.getRequestOptions(), spinner)
        }
    }

    /**
    * method for get the count all bank imports fin one org accounts
    * @param card
    */
    public getCountExtractsOnCard(card: string): Observable<number> {
        if (this.checkAuthorization()) {
            let url = 'api/BankImport/GetCountExtracts';
            return this._http.get(url + '/' + card, RequestOptionsService.getRequestOptions())
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
        if (this.checkAuthorization())
        {
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
