import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { Headers, RequestOptions } from '@angular/http';
import { Injectable, Inject } from '@angular/core';
import { AuthorizeViewModel } from '../components/shared/authorization-view.model';
import { AuthorizationType } from '../components/shared/authorization.type';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch'
@Injectable()
export class AuthorizationService {
    private _authorizationUrl = '/api/User/';
    constructor(private _http: Http) { }
    /**
     * send request to controller to authorize user and return his token
     * @param user
     */
    logIn(user: AuthorizeViewModel): Observable<AuthorizationType> {
        let urlLog = "LogIn";
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        return this._http.post(this._authorizationUrl + urlLog, JSON.stringify(user), options)
            .map((response: Response) => response.json() as AuthorizationType,)
            .catch(this.handleError);
    }
    /**
     * send request to controller to check if user is authorized and check his law
     * @param login
     * @param token
     */
    check(login: string, token: string): Observable<string> {
        let urlMethod = "Name";
        let headers = new Headers({ 'Content-Type': 'application/json' });
        headers.append("Authorization", "Bearer " + token);
        let options = new RequestOptions({ headers: headers });
        return this._http.post(this._authorizationUrl + urlMethod, JSON.stringify(login), options)
            .map((response: Response) => response.json() as string)
            .catch(this.handleError);
    }
    /**
     * catch error
     * @param error
     */
    handleError(error: Response) {     
        return Observable.throw(error.json().error );
    }
}