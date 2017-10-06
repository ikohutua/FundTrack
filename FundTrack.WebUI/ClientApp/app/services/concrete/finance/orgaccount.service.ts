import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/map';
import { OrgAccountViewModel } from "../../../view-models/concrete/finance/orgaccount-viewmodel";
import * as key from '../../../shared/key.storage';
import { AuthorizeUserModel } from "../../../view-models/concrete/authorized-user-info-view.model";
import { isBrowser } from "angular2-universal";
import { DeleteOrgAccountViewModel } from "../../../view-models/concrete/finance/deleteorgaccount-view.model";
import { DonateCredentialsViewModel } from "../../../view-models/concrete/finance/donate-credentials.view-model";
import Targetviewmodel = require("../../../view-models/concrete/finance/donate/target.view-model");
import TargetViewModel = Targetviewmodel.TargetViewModel;
import { GlobalUrlService } from "../global-url.service";
import { RequestOptionsService } from "../request-options.service";
import {BankViewModel} from "../../../view-models/concrete/finance/bank-view.model";

/**
 * Service for super admin actions
 */
@Injectable()
export class OrgAccountService {
    private user: AuthorizeUserModel = new AuthorizeUserModel();
    // urls to server
    private _readAllUrl: string = 'api/orgaccount/readall';
    private _createUrl: string = 'api/orgaccount/create';
    private _getAccountUrl: string = 'api/orgaccount/get';
    private _deleteAccountUrl: string = 'api/orgaccount/delete';

    constructor(private _http: Http) {
    }
    /**
     * Gets organizations to display on page from server
     * @param currentPage
     * @param itemsPerPage
     */
    public getAllAccountsOfOrganization(): Observable<OrgAccountViewModel[]> {
        if (this.checkAuthorization()) {
            let body = this.user;
            return this._http.get(this._readAllUrl + '/' + this.user.orgId, this.getRequestOptions())
                .map((response: Response) => <OrgAccountViewModel[]>response.json())
                .catch(this.handleError);
        }
    }
    public createOrgAccount(model: OrgAccountViewModel): Observable<OrgAccountViewModel> {
        if (this.checkAuthorization()) {
            return this._http.post(this._createUrl, model, this.getRequestOptions())
                .map((response: Response) => <OrgAccountViewModel>response.json())
                .catch(this.handleError);
        }
    }
    public getOrganizationAccountById(accountId: number): Observable<OrgAccountViewModel> {
        if (this.checkAuthorization()) {
            return this._http.get(this._getAccountUrl + '/' + accountId.toString(), this.getRequestOptions())
                .map((r: Response) => <OrgAccountViewModel>r.json())
                .catch(this.handleError);
        }
    }
    public deleteOrganizationAccountById(model: DeleteOrgAccountViewModel): Observable<DeleteOrgAccountViewModel> {
        if (this.checkAuthorization()) {
            return this._http.post(this._deleteAccountUrl, model, this.getRequestOptions())
                .map((r: Response) => <DeleteOrgAccountViewModel>r.json())
                .catch(this.handleError);
        }
    }

    public checkDonateStatus(bankAccountId: number): Observable<boolean> {
        if (this.checkAuthorization()) {
            return this._http.get('api/OrgAccount/GetDonationStatus/' + bankAccountId.toString())
                .map((response: Response) => <boolean>response.json())
                .catch(this.handleError);
        }
    }

    public checkDonateEnable(orgAccountId: number): Observable<boolean> {
        return this._http.get('api/OrgAccount/CheckDonateFunction/' + orgAccountId.toString())
            .map((response: Response) => <boolean>response.json())
            .catch(this.handleError);
    }

    public getDonateCredentials(orgAccountId: number): Observable<DonateCredentialsViewModel> {
        return this._http.get('api/OrgAccount/GetDonateCredentials/' + orgAccountId.toString())
            .map((response: Response) => <DonateCredentialsViewModel>response.json())
            .catch(this.handleError);
    }

    public toggleDonate(orgAccountId: number): Observable<boolean> {
        return this._http.put('api/OrgAccount/ToggleDonateFunction', orgAccountId, this.getRequestOptions())
            .map((response: Response) => <boolean>response.json())
            .catch((error: Response) => this.handleError(error));
    }

    public getBankAccId(orgAccountId: number): Observable<number> {
        return this._http.get('api/OrgAccount/GetBankAccountId/' + orgAccountId.toString())
            .map((r: Response) => <number>r.json())
            .catch(this.handleError);
    }

    public connectDonation(info: DonateCredentialsViewModel): Observable<DonateCredentialsViewModel> {
        return this._http.post('api/OrgAccount/ConnectDonation', info, this.getRequestOptions())
            .map((res: Response) => <DonateCredentialsViewModel>res.json())
            .catch((error: Response) => this.handleError(error));
    }

    public disableDonation(bankAccountId: number): Observable<DonateCredentialsViewModel> {
        return this._http.put('api/OrgAccount/DisableDonateFunction', bankAccountId, this.getRequestOptions())
            .map((res: Response) => <DonateCredentialsViewModel>res.json())
            .catch((error: Response) => this.handleError(error));
    }
  
    private getRequestOptions() {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        headers.append("Authorization", "Bearer " + localStorage.getItem(key.keyToken));
        let options = new RequestOptions({ headers: headers });
        return options;
    }
    ///Error handler to report into console
    private handleError(error: Response) {
        console.error(error);
        return Observable.throw(error.json().error || 'Server error');
    }
    private checkAuthorization(): boolean {
        if (isBrowser) {
            if (localStorage.getItem(key.keyToken)) {
                this.user = JSON.parse(localStorage.getItem(key.keyModel)) as AuthorizeUserModel;
                return true;
            }
            return false;
        };
    }

    public getAllBaseTargetsOfOrganization(orgId: number): Observable<TargetViewModel[]> {
        return this._http.get('api/Target/' + orgId.toString())
            .map((response: Response) => { return response.json() as TargetViewModel[] })
            .catch((error: Response) => this.handleError(error));
    }

    public updateOrganizationAccount(account: OrgAccountViewModel): Observable<OrgAccountViewModel> {
        return this._http.put('api/OrgAccount/UpdateOrganizationAccount', account, this.getRequestOptions())
            .map((res: Response) => <OrgAccountViewModel>res.json())
            .catch((error: Response) => this.handleError(error));
    }

    public getTargetById(targetId: number): Observable<TargetViewModel> {
        return this._http.get('api/Target/GetTarget/' + targetId.toString())
            .map((response: Response) => { return response.json() as TargetViewModel})
            .catch((error: Response) => this.handleError(error));
    }


    public checkExtractsStatus(bankAccountId: number): Observable<boolean> {
        if (this.checkAuthorization()) {
            return this._http.get(GlobalUrlService.getExtractStatus + bankAccountId.toString())
                .map((response: Response) => <boolean>response.json())
                .catch(this.handleError);
        }
    }

    public getExtractsCredentials(orgAccountId: number): Observable<DonateCredentialsViewModel> {
        return this._http.get(GlobalUrlService.getExtractCredentials +"/"+ orgAccountId.toString())
            .map((response: Response) => <DonateCredentialsViewModel>response.json())
            .catch(this.handleError);
    }

    public connectExtracts(info: DonateCredentialsViewModel): Observable<DonateCredentialsViewModel> {
        return this._http.post(GlobalUrlService.connectExtracts, info, RequestOptionsService.getRequestOptions())
            .map((res: Response) => <DonateCredentialsViewModel>res.json())
            .catch((error: Response) => this.handleError(error));
    }

    public getAllBanks(): Observable<BankViewModel[]> {
        return this._http.get(GlobalUrlService.banksUrl, RequestOptionsService.getRequestOptions())
            .map((response: Response) => response.json() as BankViewModel[]);
    }

    public getBankById(bankId : number): Observable<BankViewModel> {
        return this._http.get(GlobalUrlService.banksUrl + '/' + bankId.toString(), RequestOptionsService.getRequestOptions())
            .map((response: Response) => response.json() as BankViewModel);
    }
}
