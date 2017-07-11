import { Injectable } from "@angular/core";
import { Http, Response, Headers, RequestOptionsArgs, RequestOptions } from "@angular/http";
import { RequestManagementViewModel } from '../../../view-models/abstract/organization-management-view-models/request-management-view-model';
import { Observable } from "rxjs/Observable";
import "rxjs/add/operator/map";
import 'rxjs/add/operator/do';
import "rxjs/add/operator/catch";
import { GoodsTypeViewModel } from "../../../view-models/concrete/goodsType-view.model";

@Injectable()
export class OrganizationManagementRequestService {
    private _organizationItemsUrl: string = "api/requestedItem/GetOrganizationRequestedItems";
    private _goodsTypeUrl: string = "api/requestedItem/GetGoodsType";
    private _requestToAddUrl: string = "api/requestedItem/AddRequestedItem";
    private _requestToDeleteUrl: string = "api/requestedItem/DeleteRequestedItem";
    private _getByIdRequestedItem: string = "api/requestedItem/GetRequestedItem";
    private _updateRequesterItemUrl: string = "api/requestedItem/UpdateRequestedItem";

    public constructor(private _http: Http) { }

    /**
     * Gets all requested items by organization
     * @param id
     */
    public getAllRequestedItemsByOrganization(id: number): Observable<RequestManagementViewModel[]> {
        return this._http.get(this._organizationItemsUrl + '/' + id,
            { headers: new Headers({ 'ContentType': 'application/json' }) })
            .map((response: Response) => <RequestManagementViewModel[]>response.json())
            .catch(this.handleError);
    }

    /**
     * Get by id requested item
     * @param itemId
     */
    public getRequestedItemById(itemId: number): Observable<RequestManagementViewModel> {
        return this._http.get(this._getByIdRequestedItem + '/' + itemId,
            { headers: new Headers({ 'ContentType': 'application/json' }) })
            .map((response: Response) => <RequestManagementViewModel>response.json())
            .catch(this.handleError);
    }

    /**
     * Gets all goods type
     */
    public getAllGoodsTypes(): Observable<GoodsTypeViewModel[]> {
        return this._http.get(this._goodsTypeUrl)
            .map((response: Response) => <GoodsTypeViewModel[]>response.json())
            .catch(this.handleError);
    }

    /**
     * Adds new requested item
     * @param itemToAdd
     */
    public addRequestedItem(itemToAdd: RequestManagementViewModel): Observable<RequestManagementViewModel> {
        let body = JSON.stringify(itemToAdd);
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        return this._http.post(this._requestToAddUrl, body, options)
            .map((response: Response) => <RequestManagementViewModel>response.json())
            .catch(this.handleError);
    }

    /**
     * Delete requested item
     * @param itemId
     */
    public deleteRequestedItem(itemId: number): Observable<RequestManagementViewModel>{
        return this._http.delete(this._requestToDeleteUrl + '/' + itemId,
        { headers: new Headers({ 'ContentType': 'application/json' }) })
            .map((response: Response) => <RequestManagementViewModel>response.json())
            .catch(this.handleError);
    }

    /**
     * Edit requested item
     * @param item
     */
    public editRequestedItem(item: RequestManagementViewModel): Observable<RequestManagementViewModel> {
        let body = JSON.stringify(item);
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        return this._http.put(this._updateRequesterItemUrl, body, options)
            .map((response: Response) => <RequestManagementViewModel>response.json())
            .catch(this.handleError);
    }

    /**
    * Creates RequestOptionsArgs
    * @param body:T
    * @returns interface RequestOptionsArgs
    */
    private getRequestArgs(body: RequestManagementViewModel): RequestOptionsArgs {
        let headers = new Headers({ 'ContentType': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        return { headers: headers, body: body };
    }

    /**
    * Exception handler
    * @param error: Response
    */
    private handleError(error: Response) {
        return Observable.throw(error.json() || 'Server error');
    }
}
