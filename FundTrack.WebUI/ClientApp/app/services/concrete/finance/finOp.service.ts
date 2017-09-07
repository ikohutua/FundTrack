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
import { TargetViewModel } from "../../../view-models/concrete/finance/donate/target.view-model";
import { OrgAccountSelectViewModel } from "../../../view-models/concrete/finance/org-accounts-select-view.model";
import { FinOpViewModel } from "../../../view-models/concrete/finance/finOp-view.model";
import { SpinnerComponent } from "../../../shared/components/spinner/spinner.component";
import { BaseSpinnerService } from "../../abstract/base-spinner-service";
import { FinOpListViewModel } from "../../../view-models/concrete/finance/finop-list-viewmodel";

@Injectable()
export class FinOpService extends BaseSpinnerService< FinOpViewModel > {

    private getFinOpsUrl: string = 'api/finop/getFinOpsByOrgAccId';

    public constructor(private _http: Http) {
        super(_http)
    }

    /**
     * method for get all targets, when convert bankImport to finOp
     */
    public getTargets(spinner?: SpinnerComponent): Observable<TargetViewModel[]> {
        if (this.checkAuthorization()) {
            let url = "api/FinOp/GetTargets";
            return this._http.get(url, this.getRequestOptions())
                .map((response: Response) => <TargetViewModel[]>response.json())
                .catch(this.handleError);
        }
    }
    /**
     * get orgaccounts by card number this accounts
     * @param orgId
     * @param cardNumber
     */
    public getOrgAccountForFinOp(orgId: number, cardNumber: string, spinner?: SpinnerComponent): Observable<OrgAccountSelectViewModel> {
        if (this.checkAuthorization()) {
            let url = 'api/OrgAccount/GetOrgAccountForFinOp';
            return this._http.get(url + '/' + orgId + '/' + cardNumber, this.getRequestOptions())
                .map((response: Response) => <OrgAccountSelectViewModel>response.json())
                .catch(this.handleError);
        }
    }

    /**
     * create new finOp
     * @param finOp
     */
    public createFinOp(finOp: FinOpViewModel, spinner?: SpinnerComponent): Observable<FinOpViewModel> {
        if (this.checkAuthorization()) {
            let url = 'api/FinOp/CreateFinOp';
            return super.create(url, finOp, this.getRequestOptions())
        }
    }

    /**

    **/
    public getFinOpsByOrgAccountId(orgAccountId: number): Observable<FinOpListViewModel[]> {
        if (this.checkAuthorization()) {
            return this._http.get(this.getFinOpsUrl + '/' + orgAccountId, this.getRequestOptions())
                .map((response: Response) => <FinOpListViewModel[]>response.json())
                .do(data => console.log('Item' + JSON.stringify(data)))
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