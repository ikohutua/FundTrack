import { Injectable } from "@angular/core";
import { Http, Response, Headers, RequestOptions } from "@angular/http";
import { BaseService } from "../abstract/base-service";
import { IShowRequestedItem } from "../../view-models/abstract/showrequesteditem-model.interface";
import { Observable } from "rxjs/Observable";
import "rxjs/add/operator/map";
import 'rxjs/add/operator/do';
import "rxjs/add/operator/catch";
import { RequestedItemInitViewModel } from "../../view-models/abstract/requesteditem-initpaginationdata-view-model";
import { ShowRequestedItem } from "../../view-models/concrete/showrequesteditem-model.interface";
import { FilterRequstedViewModel } from '../../view-models/concrete/filter-requests-view.model';

@Injectable()
export class ShowRequestedItemService extends BaseService<IShowRequestedItem>{
    private _urlForPagination: string = 'api/RequestedItem/GetRequestedItemPaginationData';
    private _urlGetRequestedItemToShowPerPage: string = 'api/RequestedItem/GetRequestedItemToShowPerPage';
    private _urlFilterRequestedItem: string = 'api/RequestedItem/GetFilterRequestedItemPaginationData';


   /**
   * @constructor
   * @param http
   */
    constructor(private http: Http) {
        super(http, 'api/RequestedItem/GetRequestedItemToShow');
    }

    /**
     * Gets initial pagination data about organizations
     */
    public getRequestedItemInitData() {
        return this.http.get(this._urlForPagination)
            .map((response: Response) => response.json() as RequestedItemInitViewModel)
    }

    //public getItemsOnScroll(additionString: string, itemsPerPage: number, currentPage: number): Observable<IEventModel[]> {
    //    return this.http.get(additionString + '/' + itemsPerPage + '/' + currentPage)
    //        .map((response: Response) => <IEventModel[]>response.json())
    //        .catch(this.handleErrorHere);
    //}

    public getRequestedItemOnPage(itemsPerPage: number, currentPage: number, filters: FilterRequstedViewModel) {

        let body = {
            "filterOptions": filters,
            "currentPage": currentPage,
            "itemsPerPage": itemsPerPage
        };
        return this.http.post(this._urlGetRequestedItemToShowPerPage, JSON.stringify(body), this.getRequestOptions())
            .map((response: Response) => response.json() as ShowRequestedItem[])
            .catch(this.handleErrorHere);
    }

    /**
     * send request to controller to filter data in the according to 'filters'
     * @param filters
     */
    public getFilterRequestedItemInitData(filters: FilterRequstedViewModel): Observable<RequestedItemInitViewModel> {
        return this.http.post(this._urlFilterRequestedItem, JSON.stringify(filters), this.getRequestOptions())
            .map((response: Response) => response.json() as ShowRequestedItem[])
            .catch(this.handleErrorHere);
    }

    private handleErrorHere(error: Response) {
        return Observable.throw(error.json().error || 'Server error');
    }

    /**
    * Create RequestOptions
    */
    private getRequestOptions() {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        return options;
    }

}