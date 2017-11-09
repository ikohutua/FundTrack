import { Injectable } from "@angular/core";
import { FixingBalanceFilteringViewModel } from "../../view-models/concrete/fixing-balance-filtering-view.model";
import { Http, Response } from "@angular/http";
import { RequestOptionsService } from "./request-options.service";
import { GlobalUrlService } from "./global-url.service";
import { Observable } from "rxjs/Observable";
import { BalanceViewModel } from "../../view-models/concrete/finance/balance-view.model";

export class MyClass {
    public balances: BalanceViewModel[] = [];
}

@Injectable()
export class FixingBalanceService {

    public constructor(private _http: Http) {
    }

    public getFilterByAccId(accountId: number): Observable<FixingBalanceFilteringViewModel> {
        return this._http.get(GlobalUrlService.getFixingBalanceUrl + accountId, RequestOptionsService.getRequestOptions())
            .map((response: Response) => response.json() as FixingBalanceFilteringViewModel);
    }

    public fixBalance(balance: BalanceViewModel): Observable<BalanceViewModel> {
        return this._http.post(GlobalUrlService.getFixingBalanceUrl, balance, RequestOptionsService.getRequestOptions())
            .map((response: Response) => response.json() as BalanceViewModel);
    }

    public fixAllBalances(balances: Array<BalanceViewModel>): Observable<BalanceViewModel[]> {
        let my = new MyClass();

        my.balances = balances;
        return this._http.post(GlobalUrlService.fixingAllBalancesUrl, my, RequestOptionsService.getRequestOptions())
            .map((response: Response) => response.json() as BalanceViewModel[]);
    }
}