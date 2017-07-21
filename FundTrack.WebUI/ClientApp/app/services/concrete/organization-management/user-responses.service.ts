import { Injectable, Inject } from '@angular/core';
import { Http, Response } from '@angular/http';
import { UserResponseOnRequestsViewModel } from '../../../view-models/concrete/user-response-on-requests-view.model';
import { Observable } from 'rxjs/Observable';
import { Headers, RequestOptions } from '@angular/http';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch';

@Injectable()
export class UserResponseService {

    public constructor(private _http: Http) { }

    public getUserResponsesByOrganization(organizationId: number): Observable<UserResponseOnRequestsViewModel[]> {
        let userResponseUrl = 'api/UserResponse/GetUserResponse';
        return this._http.get(userResponseUrl + '/' + organizationId)
            .map((response: Response) => <UserResponseOnRequestsViewModel[]>response.json())
            .catch(this.handleError)
    }

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

    public getUserResponseWithNewStatus(organizationId: number): Observable<number> {
        let userResponseCountNewStatus = 'api/UserResponse/GetUserResonseWithNewStatus';
        return this._http.get(userResponseCountNewStatus + '/' + organizationId)
            .map((response: Response) => <number>response.json())
            .catch(this.handleError)
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
