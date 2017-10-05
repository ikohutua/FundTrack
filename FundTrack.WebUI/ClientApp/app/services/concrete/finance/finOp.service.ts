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
import { FinOpFromBankViewModel } from "../../../view-models/concrete/finance/finOpFromBank-view.model";
import { SpinnerComponent } from "../../../shared/components/spinner/spinner.component";
import { BaseSpinnerService } from "../../abstract/base-spinner-service";
import { FinOpListViewModel } from "../../../view-models/concrete/finance/finop-list-viewmodel";
import Moneyoperationviewmodel = require("../../../view-models/concrete/finance/money-operation-view-model");
import MoneyOperationViewModel = Moneyoperationviewmodel.MoneyOperationViewModel;

@Injectable()
export class FinOpService extends BaseSpinnerService<FinOpFromBankViewModel> {

    private getFinOpsUrl: string = 'api/finop/getFinOpsByOrgAccId';
    private getTargetsUrl: string = "api/finop/GetTargets";
    private incomeUrl: string = "api/finop/income";
    private spendingUrl: string = "api/finop/spending";
    private transferUrl: string = "api/finop/transfer";
    private editUrl: string = "api/finop/edit";
    private getOrgAccForFinOpsUrl: string = 'api/OrgAccount/GetOrgAccountForFinOp';
    private createUrl: string = 'api/FinOp/CreateFinOp';
    private getFinOpUrl: string = 'api/finop/getFinOpsById';

    /*public*/ constructor(private _http: Http) {
        super(_http)
    }

    /**
     * method for get all targets, when convert bankImport to finOp
     */
    public getTargets(spinner?: SpinnerComponent): Observable<TargetViewModel[]> {
        if (this.checkAuthorization()) {
            return this._http.get(this.getTargetsUrl, this.getRequestOptions())
                .map((response: Response) => <TargetViewModel[]>response.json())
                .catch(this.handleError);
        }
    }

    public createIncome(moneyIncome: FinOpListViewModel): Observable<FinOpListViewModel> {
        let body = moneyIncome;
        return this._http.post(this.incomeUrl, body, this.getRequestOptions())
            .map((response: Response) => <FinOpListViewModel>response.json())
            .catch(this.handleError);
    }

    public createSpending(moneySpending: FinOpListViewModel): Observable<FinOpListViewModel> {
        let body = moneySpending;
        return this._http.post(this.spendingUrl, body, this.getRequestOptions())
            .map((response: Response) => <FinOpListViewModel>response.json())
            .catch(this.handleError)
    }

    public createTransfer(moneyTransfer: FinOpListViewModel): Observable<FinOpListViewModel> {
        let body = moneyTransfer;
        return this._http.post(this.transferUrl, body, this.getRequestOptions())
            .map((response: Response) => <FinOpListViewModel>response.json())
            .catch(this.handleError);
    }

    public editFinOperation(finOp: FinOpListViewModel): Observable<FinOpListViewModel> {
        let body = finOp;
        return this._http.put(this.editUrl, body, this.getRequestOptions())
            .map((response: Response) => <FinOpListViewModel>response.json())
            .catch(this.handleError);
    }
    /**
     * get orgaccounts by card number this accounts
     * @param orgId
     * @param cardNumber
     */
    public getOrgAccountForFinOp(orgId: number, cardNumber: string, spinner?: SpinnerComponent): Observable<OrgAccountSelectViewModel> {
        if (this.checkAuthorization()) {
            return this._http.get(this.getOrgAccForFinOpsUrl + '/' + orgId + '/' + cardNumber, this.getRequestOptions())
                .map((response: Response) => <OrgAccountSelectViewModel>response.json())
                .catch(this.handleError);
        }
    }

    /**
     * create new finOp
     * @param finOp
     */
    public createFinOp(finOp: FinOpFromBankViewModel, spinner?: SpinnerComponent): Observable<FinOpFromBankViewModel> {
        if (this.checkAuthorization()) {
            let url = 'api/FinOp/CreateFinOp';
            return super.create(url, finOp, this.getRequestOptions())
            //return this._http.post(url, finOp)
            //  .map((response: Response) => <FinOpFromBankViewModel>response.json())
            //.catch(this.handleError);
        }
    }

    /**
    **/
    public getFinOpsByOrgAccountId(orgAccountId: number): Observable<FinOpListViewModel[]> {
        if (this.checkAuthorization()) {
            return this._http.get(this.getFinOpsUrl + '/' + orgAccountId, this.getRequestOptions())
                .map((response: Response) => <FinOpListViewModel[]>response.json())
                .catch(this.handleError);
        }
    }

    public getFinOpById(id: number): Observable<FinOpListViewModel> {
        if (this.checkAuthorization()) {
            return this._http.get(this.getFinOpUrl + '/' + id, this.getRequestOptions())
                .map((response: Response) => <FinOpListViewModel>response.json())
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