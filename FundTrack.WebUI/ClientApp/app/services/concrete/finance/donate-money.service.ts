﻿import { Injectable } from "@angular/core";
import { Http, Response, Headers, RequestOptions, RequestOptionsArgs, RequestMethod } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/do'
import { FondyCheckPaymentResponseViewModel, CheckPaymentResponseViewModel } from "../../../view-models/concrete/finance/donate/check-payment-response.view-model";
import { OrganizationDonateAccountsViewModel } from "../../../view-models/concrete/finance/donate/donate-account.view-model";
import { TargetViewModel } from "../../../view-models/concrete/finance/donate/target.view-model";
import { CurrencyViewModel } from "../../../view-models/concrete/finance/donate/currency.view-model";
import { DonateViewModel } from "../../../view-models/concrete/finance/donate/donate.view-model";

@Injectable()
export class DonateService {
    constructor(private _http: Http) {

    }

    sendRequestToFondy(request: any): Observable<any> {
        let requestBody = JSON.stringify({ request: request });
        return this._http.post('api/Donate/SendRequestFondy', requestBody, this.getOptions()).
            map((response: Response) => { return response.text() });
    }

    checkPaymentRequest(request: any): Observable<FondyCheckPaymentResponseViewModel> {
        let requestBody = JSON.stringify({ request: request });
        return this._http.post('api/Donate/CheckPayment', requestBody, this.getOptions()).
            map((response: Response) => {
                let result = response.json() as FondyCheckPaymentResponseViewModel;
                return result;
            });
    }

    getAccountsForDonate(organizationId: number): Observable<OrganizationDonateAccountsViewModel> {
        return this._http.get('api/Donate/GetAccountsForDonate/' + organizationId.toString()).
            map((response: Response) => { return response.json() as OrganizationDonateAccountsViewModel });
    }

    getOrderId(): Observable<string> {
        return this._http.get('api/Donate/GetOrderId').
            map((response: Response) => { return response.text() });
    }

    getTargets(): Observable<TargetViewModel[]> {
        return this._http.get('api/Donate/GetTargets').
            map((response: Response) => { return response.json() as TargetViewModel[]});
    }

    getCurrencies(): Observable<CurrencyViewModel[]> {
        return this._http.get('api/Donate/GetCurrencies').
            map((response: Response) => { return response.json() as CurrencyViewModel[]})
    }

    addDonation(item: DonateViewModel): Observable<DonateViewModel>{
        return this._http.post('api/Donate/AddDonation', item, this.getOptions())
            .map((response: Response) => {return  response.json() as DonateViewModel});
    }

    private getOptions() : RequestOptions{
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let option = new RequestOptions({ headers: headers });
        return option;
    }

}