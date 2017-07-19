import { Injectable, Inject } from '@angular/core';
import { Http, Response } from '@angular/http';
import { UserResponseOnRequestsViewModel } from '../../../view-models/concrete/user-response-on-requests-view.model';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch';

@Injectable()
export class UserResponseService {

    public constructor(private _http: Http) { }

    public getUserResponsesByOrganization(organizationId: number): Observable<UserResponseOnRequestsViewModel[]> {
        let userResponseUrl = 'api/UserResponse/GetUserResponse'
        return this._http.get(userResponseUrl + '/' + organizationId)
            .map((response: Response) => <UserResponseOnRequestsViewModel[]>response.json())
    }

    /**
    * Catch error
    * @param error
    */
    private handleError(error: Response): any {
        return Observable.throw(error.json().error);
    }
}
