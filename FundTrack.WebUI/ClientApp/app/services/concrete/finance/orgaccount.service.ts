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
        if (this.checkAuthorization())
        {
            let body = this.user;
            return this._http.post(this._readAllUrl, body, this.getRequestOptions())
                .map((response: Response) => <OrgAccountViewModel[]>response.json())
                .do(data => console.log('Item' + JSON.stringify(data)))
                .catch(this.handleError);
        }
    }
    public createOrgAccount(model: OrgAccountViewModel): Observable<OrgAccountViewModel> {
        if (this.checkAuthorization())
        {
            return this._http.post(this._createUrl, model, this.getRequestOptions())
                .map((response: Response) => <OrgAccountViewModel>response.json())
                .do(data => console.log('Account data:' + JSON.stringify(data)))
                .catch(this.handleError);
        }
    }
    public getOrganizationAccountById(accountId: number): Observable<OrgAccountViewModel> {
        if (this.checkAuthorization()) {
            return this._http.get(this._getAccountUrl + '/' + accountId.toString(), this.getRequestOptions())
                .map((r: Response) => <OrgAccountViewModel>r.json())
                .do(data => console.log('Item' + JSON.stringify(data)))
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
}
