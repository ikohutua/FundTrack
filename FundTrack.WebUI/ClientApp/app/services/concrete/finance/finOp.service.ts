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
import { TargetViewModel } from "../../../view-models/concrete/finance/target-view.model";
import { OrgAccountSelectViewModel } from "../../../view-models/concrete/finance/org-accounts-select-view.model";
import { FinOpViewModel } from "../../../view-models/concrete/finance/finOp-view.model";

@Injectable()
export class FinOpService {

    public constructor(private _http: Http) { }

    public getTargets(): Observable<TargetViewModel[]> {
        if (this.checkAuthorization()) {
            let url = "api/FinOp/GetTargets";
            return this._http.get(url, this.getRequestOptions())
                .map((response: Response) => <TargetViewModel[]>response.json())
                .catch(this.handleError);
        }
    }

    public getOrgAccountsForFinOp(orgId: number): Observable<OrgAccountSelectViewModel[]> {
        if (this.checkAuthorization()) {
            let url = 'api/OrgAccount/GetOrgAccountsForFinOp';
            return this._http.get(url + '/' + orgId, this.getRequestOptions())
                .map((response: Response) => <OrgAccountSelectViewModel[]>response.json())
                .catch(this.handleError);
        }
    }

    public getOrgAccountForFinOp(orgId: number, cardNumber: string): Observable<OrgAccountSelectViewModel> {
        if (this.checkAuthorization()) {
            let url = 'api/OrgAccount/GetOrgAccountForFinOp';
            return this._http.get(url + '/' + orgId + '/' + cardNumber, this.getRequestOptions())
                .map((response: Response) => <OrgAccountSelectViewModel>response.json())
                .catch(this.handleError);
        }
    }

    public createFinOp(finOp: FinOpViewModel): Observable<FinOpViewModel> {
        if (this.checkAuthorization()) {
            let url = 'api/FinOp/CreateFinOp';
            return this._http.post(url, finOp, this.getRequestOptions())
                .map((response: Response) => <FinOpViewModel>response.json())
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