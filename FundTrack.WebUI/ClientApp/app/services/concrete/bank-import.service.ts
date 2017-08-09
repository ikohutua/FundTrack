import { Injectable } from "@angular/core";
import { Http, RequestOptions, Headers } from "@angular/http";
import { Observable } from "rxjs/Observable";
import { PrivatSessionViewModel } from "../../view-models/concrete/privat-session-view.model";
import { Response } from '@angular/http';
import { Md5 } from 'ts-md5/dist/md5';
import * as xml2js from 'xml2js';
import * as sha1 from 'js-sha1';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch';
import { ImportPrivatViewModel, ImportDetailPrivatViewModel } from "../../view-models/concrete/import-privat-view.model";
import { DataRequestPrivatViewModel } from "../../view-models/concrete/data-request-privat-view.model";

@Injectable()
export class BankImportService {

    public constructor(private _http: Http) { }

    public getUserExtracts(dataForRequest: DataRequestPrivatViewModel): Observable<ImportPrivatViewModel> {

        let data = `<oper>cmt</oper>
                        <wait>0</wait>
                        <test>0</test>
                        <payment id="">
                          <prop name="sd" value="${dataForRequest.dataFrom}" />
                          <prop name="ed" value="${dataForRequest.dataTo}" />
                          <prop name="card" value="${dataForRequest.card}" />
                        </payment>`;

        let sign = sha1(Md5.hashStr(data + dataForRequest.password));

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
                let res: ImportPrivatViewModel = new ImportPrivatViewModel();
                xml2js.parseString(response.text(), function (err, result) {
                    console.log(result);
                    res.importsDetail = Array<ImportDetailPrivatViewModel>();
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
                        res.importsDetail.push(temp);
                    }
                });
                return res;
            })
            .catch(this.handleError)
    }

    public registerBankExtracts(bankImport: ImportPrivatViewModel): Observable<ImportPrivatViewModel> {
        let url = 'api/BankImport/RegisterNewExtracts';
        return this._http.post(url, bankImport, this.getRequestOptions())
            .map((response: Response) => <ImportPrivatViewModel>response.json())
            .catch(this.handleError);
    }

    /**
    * Create RequestOptions
    */
    private getRequestOptions() {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        return options;
    }

    /**
    * Catch error
    * @param error
    */
    private handleError(error: Response): any {
        return Observable.throw(error.json().error);
    }
}
