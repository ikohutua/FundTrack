import { Injectable } from "@angular/core";
import { Http, Response, Headers, RequestOptionsArgs, RequestOptions } from "@angular/http";
import { RequestManagementViewModel } from '../../../view-models/abstract/organization-management-view-models/request-management-view-model';
import { Observable } from "rxjs/Observable";
import "rxjs/add/operator/map";
import 'rxjs/add/operator/do';
import "rxjs/add/operator/catch";
import { GoodsTypeViewModel } from "../../../view-models/concrete/goodsType-view.model";
import { RequestedImageViewModel } from "../../../view-models/abstract/organization-management-view-models/requested-item-view.model";
import { BaseSpinnerService } from "../../abstract/base-spinner-service";
import { SpinnerComponent } from "../../../shared/components/spinner/spinner.component";
import { RequestedItemInitDataViewModel } from "../../../view-models/concrete/requested-item-init-view.model";

@Injectable()
export class OrganizationManagementRequestService extends BaseSpinnerService<RequestManagementViewModel> {
    private _organizationItemsUrl: string = "api/requestedItem/GetOrganizationRequestedItems";
    private _goodsTypeUrl: string = "api/requestedItem/GetGoodsType";
    private _requestToAddUrl: string = "api/requestedItem/AddRequestedItem";
    private _requestToDeleteUrl: string = "api/requestedItem/DeleteRequestedItem";
    private _getByIdRequestedItem: string = "api/requestedItem/GetRequestedItem";
    private _updateRequesterItemUrl: string = "api/requestedItem/UpdateRequestedItem";
    private _deleteCurrentImageUrl: string = "api/requestedItem/DeleteCurrentImage";
    private _getItemPerPageUtl: string = "api/requestedItem/GetRequestedItemPerPage";
    private _getRequestedItemInitDataUrl: string = "api/requestedItem/GetRequestedItemInitData";
    
    public constructor(private _http: Http)
    {
        super(_http);
    }

    /**
     * Gets all requested items by organization
     * @param id
     */
    public getAllRequestedItemsByOrganization(id: number, spinner?: SpinnerComponent): Observable<RequestManagementViewModel[]> {
        return super.getCollection(this._organizationItemsUrl + '/' + id, null, spinner);
    }

    /**
     * Get by id requested item
     * @param itemId
     */
    //public getRequestedItemById(itemId: number, spinner?: SpinnerComponent): Observable<RequestManagementViewModel> {
    //    return super.getById(this._getByIdRequestedItem, itemId, null, spinner);            
    //}

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
     * Adds requested item to database
     * @param itemToAdd
     */
    public addRequestedItem(itemToAdd: RequestManagementViewModel): Observable<RequestManagementViewModel> {
        let body = itemToAdd;
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
     * Delete current image
     * @param imageId
     */
    public deleteCurrentImage(imageId: number) {
        return this._http.delete(this._deleteCurrentImageUrl + '/' + imageId,
            { headers: new Headers({ 'ContentType': 'application/json' }) })
            .map((response: Response) => <RequestedImageViewModel>response.json())
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
     * Gets requested items per page
     * @param organizationId
     * @param currentPage
     * @param pageSize
     */
    public getRequestedItemsPerPage(organizationId: number, currentPage: number, pageSize: number, spinner?: SpinnerComponent): Observable<RequestManagementViewModel[]> {
        var url = this._getItemPerPageUtl + '/' + organizationId + '/' + currentPage + '/' + pageSize;
        return super.getCollection(url, null, spinner);
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
     * Gets requested item init data
     * @param organizationId
     */
    public getRequestedItemsInitData(organizationId: number): Observable<RequestedItemInitDataViewModel> {
        return this._http.get(this._getRequestedItemInitDataUrl + '/' + organizationId)
            .map((response: Response) => <RequestedItemInitDataViewModel>response.json())
            .catch(this.handleError);
    }

    /**
    * Exception handler
    * @param error: Response
    */
    private handleError(error: Response) {
        return Observable.throw(error.json() || 'Server error');
    }
}
