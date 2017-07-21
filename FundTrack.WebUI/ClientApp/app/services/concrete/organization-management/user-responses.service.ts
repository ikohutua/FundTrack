import { Injectable, Inject } from '@angular/core';
import { Http, Response } from '@angular/http';
import { UserResponseOnRequestsViewModel } from '../../../view-models/concrete/user-response-on-requests-view.model';
import { Observable } from 'rxjs/Observable';
import { Headers, RequestOptions } from '@angular/http';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch';
import { RequestedItemInitViewModel } from "../../../view-models/abstract/requesteditem-initpaginationdata-view-model";

@Injectable()
export class UserResponseService {

    public constructor(private _http: Http) { }

    /**
     * get user Responses by one organization
     * @param organizationId
     */
    public getUserResponsesByOrganization(organizationId: number): Observable<UserResponseOnRequestsViewModel[]> {
        let userResponseUrl = 'api/UserResponse/GetUserResponse';
        return this._http.get(userResponseUrl + '/' + organizationId)
            .map((response: Response) => <UserResponseOnRequestsViewModel[]>response.json())
            .catch(this.handleError)
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
        return this._http.get(userResponseCountNewStatus + '/' + organizationId)
            .map((response: Response) => <number>response.json())
            .catch(this.handleError)
    }

    /**
     * get UserResponse on page by pagination
     * @param itemsPerPage
     * @param currentPage
     */
    public getUserResponseOnPage(organizationId: number, itemsPerPage: number, currentPage: number): Observable<UserResponseOnRequestsViewModel[]> {
        let _urlGetUserResponseToShowPerPage = "api/UserResponse/GetUserResponseToShowPerPage";
        return this._http.get(_urlGetUserResponseToShowPerPage + '/' + organizationId + '/' + currentPage + '/' + itemsPerPage)
            .map((response: Response) => response.json() as UserResponseOnRequestsViewModel[])
            .catch(this.handleError);
    }

    /**
     * Gets initial pagination data about organizations
     */
    public getRequestedItemInitData(organizationId: number): Observable<number> {
        let _urlForPagination = "api/UserResponse/GetUserResponsePaginationData"
        return this._http.get(_urlForPagination + '/' + organizationId)
            .map((response: Response) => response.json() as number)
            .catch(this.handleError);
    }

    public deleteUserResponse(responseId: number) {
        let _urlForDeleteResponse = "api/UserResponse/DeleteUserResponse";
        return this._http.delete(_urlForDeleteResponse + '/' + responseId,
            { headers: new Headers({ 'ContentType': 'application/json' }) })
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
        let options = new RequestOptions({ headers: headers });
        return options;
    }
}
