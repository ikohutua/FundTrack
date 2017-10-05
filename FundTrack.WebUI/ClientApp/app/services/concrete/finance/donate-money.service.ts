import { Injectable } from "@angular/core";
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
import { UserDonationViewModel } from "../../../view-models/concrete/finance/donate/user-donation-view-model";
import { GlobalUrlService } from "../global-url.service";
import { RequestOptionsService } from "../request-options.service";


@Injectable()
export class DonateService {
    constructor(private _http: Http) {

    }

    sendRequestToFondy(request: any): Observable<any> {
        let requestBody = JSON.stringify({ request: request });
        return this._http.post(DonateUrlsService.sendRequestFondy, requestBody, RequestOptionsService.getRequestOptions()).
            map((response: Response) => { return response.text() });
    }

    checkPaymentRequest(request: any): Observable<FondyCheckPaymentResponseViewModel> {
        let requestBody = JSON.stringify({ request: request });
        return this._http.post(DonateUrlsService.checkPayment, requestBody, RequestOptionsService.getRequestOptions()).
            map((response: Response) => {
                let result = response.json() as FondyCheckPaymentResponseViewModel;
                return result;
            });
    }

    getAccountsForDonate(organizationId: number): Observable<OrganizationDonateAccountsViewModel> {
        return this._http.get(DonateUrlsService.accountsForDonate + organizationId.toString()).
            map((response: Response) => { return response.json() as OrganizationDonateAccountsViewModel });
    }

    getOrderId(): Observable<string> {
        return this._http.get(DonateUrlsService.orderId).
            map((response: Response) => { return response.text() });
    }

    getCurrencies(): Observable<CurrencyViewModel[]> {
        return this._http.get(DonateUrlsService.currencies).
            map((response: Response) => { return response.json() as CurrencyViewModel[]})
    }

    addDonation(item: DonateViewModel): Observable<DonateViewModel>{
        return this._http.post(DonateUrlsService.addDonation, item, RequestOptionsService.getRequestOptions())
            .map((response: Response) => {return  response.json() as DonateViewModel});
    }

    getUserDonations(userId: number): Observable<UserDonationViewModel[]> {
        return this._http.get(GlobalUrlService.userDonations + userId, RequestOptionsService.getRequestOptions())
            .map((response: Response) => {
                return response.json() as UserDonationViewModel[];
            })
            .catch(this.handleErrorHere);;
    }
    getUserDonationsByDate(userId: number, startDate: string, endDate: string): Observable<UserDonationViewModel[]> {
        return this._http.get(GlobalUrlService.userDonationsByDate+ '?userId=' + userId + '&datefrom=' + startDate + '&dateto=' + endDate, RequestOptionsService.getRequestOptions())
            .map((response: Response) => {
                return response.json() as UserDonationViewModel[];
            })
            .catch(this.handleErrorHere);
    }

    private handleErrorHere(error: Response) {
        return Observable.throw(error.json().error || 'Server error');
    };
}