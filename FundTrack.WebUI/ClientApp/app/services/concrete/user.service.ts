import { RegistrationViewModel } from '../../view-models/concrete/registration-view.model';
import { AuthorizationType, AuthorizeUserModel } from '../../view-models/concrete/authorization.type';
import { ChangePasswordViewModel } from '../../view-models/concrete/change-password-view-model';
import { AuthorizeViewModel } from '../../view-models/concrete/authorization-view.model';
import { CoolHttp } from 'angular2-cool-http';
import { CoolLoadingIndicator } from 'angular2-cool-loading-indicator';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { Headers, RequestOptions } from '@angular/http';
import { Injectable, Inject } from '@angular/core';
import { Router } from '@angular/router';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch';
import * as key from '../../shared/key.storage';

@Injectable()
export class UserService {
    private _authorizationUrl = 'api/User/';
    public constructor(private _http: Http, private _router: Router) { }

    /**
     * Send request to controller to authorize user and return his token
     * @param user
     */
    public logIn(user: AuthorizeViewModel): Observable<AuthorizationType> {
        let urlLog = "LogIn";
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        return this._http.post(this._authorizationUrl + urlLog, JSON.stringify(user), options)
            .map((response: Response) => response.json() as AuthorizationType, )
            .catch(this.handleError);
    }

    /**
     * clear local storage and close the session current user
     */
    public logOff():void {
        localStorage.clear();
    }

    /**
     * send requast to controller to create new user
     * @param registrationViewModel
     */
    public create(newItem: RegistrationViewModel): Observable<AuthorizationType> {
        let body = newItem;
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        return this._http.post("api/user/register", body, options)
            .map((response: Response) => response.json() as AuthorizationType);
    }

    

    /**
     * send request to controller to update existing user 
     * @param userModel
     */
    public editUserProfile(userModel: AuthorizeUserModel): Observable<AuthorizeUserModel> {
        let body = JSON.stringify(userModel);
        let headers = new Headers({ 'Content-Type': 'application/json' });
        headers.append("Authorization", "Bearer " + localStorage.getItem("token"));
        let options = new RequestOptions({ headers: headers });
        return this._http.put("api/user/editprofile", body, options)
            .map((response: Response) => {
                if (response.status == 200) {
                    return (response.json() as AuthorizeUserModel);
                }
            })
            .catch((error: any) => {
                if (error.status == 401) {
                    this._router.navigate(['/errorauthorize'])
                    return Observable.throw(new Error(error.status));
                }
            });
    }

     /**
     * Catch error
     * @param error
     */
    public handleError(error: Response):any {
        return Observable.throw(error.json().error);
    }
    /**
     * Sends request to controller to change user's password
     * @param changePasswordViewModel: Model, containing user login and passwords
     */
    public changePassword(changePasswordViewModel: ChangePasswordViewModel): Observable<ChangePasswordViewModel> {
        let body = changePasswordViewModel;
        let headers = new Headers({ 'Content-Type': 'application/json' });
        headers.append("Authorization", "Bearer " + localStorage.getItem(key.keyToken));
        let options = new RequestOptions({ headers: headers });
        return this._http.post("api/user/changepassword", body, options)
            .map((response: Response) => response.json() as ChangePasswordViewModel);
    }
}

