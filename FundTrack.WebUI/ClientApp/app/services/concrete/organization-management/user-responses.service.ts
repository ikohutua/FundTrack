import { Injectable, Inject } from '@angular/core';
import { Http, Response } from '@angular/http';
import { UserResponseOnRequestsViewModel } from '../../../view-models/concrete/user-response-on-requests-view.model';
import { Observable } from 'rxjs/Observable';
import { Headers, RequestOptions } from '@angular/http';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch';
import { RequestedItemInitViewModel } from "../../../view-models/abstract/requesteditem-initpaginationdata-view-model";
import * as key from '../../../shared/key.storage';
import { BaseSpinnerService } from "../../abstract/base-spinner-service";
import { SpinnerComponent } from "../../../shared/components/spinner/spinner.component";

@Injectable()
export class UserResponseService extends BaseSpinnerService< UserResponseOnRequestsViewModel > {

    public constructor(private _http: Http) {
        super(_http);
    }

    /**
     * get user Responses by one organization
     * @param organizationId
     */
    public getUserResponsesByOrganization(organizationId: number, spinner?: SpinnerComponent): Observable<UserResponseOnRequestsViewModel[]> {
        let userResponseUrl = 'api/UserResponse/GetUserResponse';
        return super.getCollection(userResponseUrl + '/' + organizationId, this.getRequestOptions(), spinner);
    }

    /**
     * change user response status by select
     * @param statusId
     * @param responseId
     */
    public changeUserResponseStatus(statusId: number, responseId: number): Observable<UserResponseOnRequestsViewModel> {
        let userChangeStatusUrl = 'api/UserResponse/ChangeUserResponseStatus';
        let body = {
            "id": responseId,
            "newStatusId": statusId
        };
        return this._http.post(userChangeStatusUrl, JSON.stringify(body), this.getRequestOptions())
            .map((response: Response) => <UserResponseOnRequestsViewModel>response.json())
            .catch(this.handleError)
    }

    /**
     * get count user responses with status"new"
     * @param organizationId
     */
    public getUserResponseWithNewStatus(organizationId: number): Observable<number> {
        let userResponseCountNewStatus = 'api/UserResponse/GetUserResonseWithNewStatus';
        return this._http.get(userResponseCountNewStatus + '/' + organizationId, this.getRequestOptions())
            .map((response: Response) => <number>response.json())
            .catch(this.handleError)
    }

    /**
     * get UserResponse on page by pagination
     * @param itemsPerPage
     * @param currentPage
     */
    public getUserResponseOnPage(organizationId: number, itemsPerPage: number, currentPage: number, spinner?: SpinnerComponent): Observable<UserResponseOnRequestsViewModel[]> {
        let _urlGetUserResponseToShowPerPage = "api/UserResponse/GetUserResponseToShowPerPage" + '/' + organizationId + '/' + currentPage + '/' + itemsPerPage;
        return super.getCollection(_urlGetUserResponseToShowPerPage, this.getRequestOptions(), spinner);
    }

    /**
     * Gets initial pagination data about organizations
     */
    public getRequestedItemInitData(organizationId: number): Observable<number> {
        let _urlForPagination = "api/UserResponse/GetUserResponsePaginationData"
        return this._http.get(_urlForPagination + '/' + organizationId, this.getRequestOptions())
            .map((response: Response) => response.json() as number)
            .catch(this.handleError);
    }

    public deleteUserResponse(responseId: number) {
        let _urlForDeleteResponse = "api/UserResponse/DeleteUserResponse";
        return this._http.delete(_urlForDeleteResponse + '/' + responseId, this.getRequestOptions())
            .map((response: Response) => response.json() as UserResponseOnRequestsViewModel )
            .catch(this.handleError);
    }

    /**
    * Catch error
    * @param error
    */
    private handleError(error: Response): any {
        return Observable.throw(error.json().error);
    }

    /**
    * Create RequestOptions
    */
    private getRequestOptions() {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        headers.append("Authorization", localStorage.getItem(key.keyToken));
        let options = new RequestOptions({ headers: headers });
        return options;
    }
}
