import { LoginFacebookViewModel } from '../../view-models/concrete/login-facebook-view.model';
import { RegistrationViewModel } from '../../view-models/concrete/registration-view.model';
import { AuthorizedUserInfoViewModel, AuthorizeUserModel } from '../../view-models/concrete/authorized-user-info-view.model';
import { ChangePasswordViewModel } from '../../view-models/concrete/change-password-view-model';
import { LoginViewModel } from '../../view-models/concrete/login-view.model';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { Headers, RequestOptions } from '@angular/http';
import { Injectable, Inject, NgZone } from '@angular/core';
import { Router } from '@angular/router';
import { UserEmailViewModel } from '../../view-models/user-email-view-model';
import { GuidViewModel } from '../../view-models/concrete/guid-view-model';
import { ResetPasswordViewModel } from '../../view-models/concrete/reset-password-view-model';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch';
import * as key from '../../shared/key.storage';
import { AuthService } from "angular2-social-login";
import { OrganizationIdViewModel } from '../../view-models/abstract/organization-id-view-model';
import { UserInfo } from "../../view-models/concrete/user-info.model";
import Requestoptionsservice = require("./request-options.service");
import RequestOptionsService = Requestoptionsservice.RequestOptionsService;
import { GlobalUrlService } from "./global-url.service";

@Injectable()
export class UserService {
    // urls to server
    private _authorizationUrl = 'api/User/';
    private _sendRecoveryEmailUrl: string = 'api/User/SendRecoveryEmail';
    private _checkGuidStatusUrl: string = 'api/User/CheckGuidStatus';
    private _resetUserPasswordUrl: string = 'api/User/ResetUserPassword';
    private _checkEmailStatusUrl: string = 'api/User/CheckEmailStatus';

    public constructor(private _http: Http,
        private _router: Router,
        private _auth: AuthService,
        private _ngZone: NgZone) { }

    // send Request to server
    private sendRequestToServer(url: string, model: any) {
        return this._http.post(url, model, RequestOptionsService.getRequestOptions()).map((response: Response) => {
            return response.json() as string;
        });
    }

    /**
     * Send request to controller to authorize facebook user and return hias access token
     * @param user
     */
    public logInWithFacebook(user: LoginFacebookViewModel): Observable<AuthorizedUserInfoViewModel> {
        let body = user;
        let urlLogFacebook = "LogInFacebook";
        return this._http.post(this._authorizationUrl + urlLogFacebook, JSON.stringify(user), RequestOptionsService.getRequestOptions())
            .map((response: Response) => response.json() as AuthorizedUserInfoViewModel, )
            .catch(this.handleError);
    }

    /**
     * Send request to controller to authorize user and return his token
     * @param user
     */
    public logIn(user: LoginViewModel): Observable<AuthorizedUserInfoViewModel> {
        let urlLog = "LogIn";
        return this._http.post(this._authorizationUrl + urlLog, JSON.stringify(user), RequestOptionsService.getRequestOptions())
            .map((response: Response) => {
                return response.json() as AuthorizedUserInfoViewModel;
            }
            )
            .catch(this.handleError);
    }

    /**
 * Send request to controller to authorize user and return his token
 * @param user
 */
    public getAccessToken(user: LoginViewModel): Observable<string> {
        var body = 'grant_type=password'
        body += '& password=' + user.password;
        body += '& username=' + user.login;
        
        return this._http.post('http://localhost:51469/token', body, this.getOwinRequestOptions())
            .map((response: Response) => {
                return response.json()["access_token"];
            })
            .catch(this.handleError);
    }

    private getOwinRequestOptions() {
        var headers = new Headers();
        headers.append('Content-Type', 'application/x-www-form-urlencoded');
        headers.append('Accept', 'application/json');
        let options = new RequestOptions({ headers: headers });
        return options;
    }

    /**
     * clear local storage and close the session current user
     */
    public logOff(): void {
        localStorage.clear();
        this._router.navigate(['/login']);
    }

    /**
     * send request to controller to create new user
     * @param registrationViewModel
     */
    public create(newItem: RegistrationViewModel): Observable<AuthorizedUserInfoViewModel> {
        let body = newItem;
        return this._http.post("api/user/register", body, RequestOptionsService.getRequestOptions())
            .map((response: Response) => response.json() as AuthorizedUserInfoViewModel);
    }

    /**
     * send request to controller to update existing user 
     * @param userModel
     */
    public editUserProfile(userModel: AuthorizeUserModel): Observable<AuthorizeUserModel> {
        let body = JSON.stringify(userModel);
        return this._http.put("api/user/editprofile", body, RequestOptionsService.getRequestOptions())
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
     * calls server to send email
     * @param email
     */
    public sendRecoveryEmail(email: UserEmailViewModel) {
        return this.sendRequestToServer(this._sendRecoveryEmailUrl, email);
    }

    /**
     * calls server to check if the guid is valid
     * @param guid
     */
    public checkGuid(guid: GuidViewModel) {
        return this.sendRequestToServer(this._checkGuidStatusUrl, guid);
    }

    /**
     * calls server to reset password
     * @param passwordModel
     */
    public resetPassword(passwordModel: ResetPasswordViewModel) {
        return this.sendRequestToServer(this._resetUserPasswordUrl, passwordModel);
    }

    public checkEmailStatus(email: UserEmailViewModel) {
        return this.sendRequestToServer(this._checkEmailStatusUrl, email);
    }

    /*
     * Sends request to controller to change user's password
    * @param changePasswordViewModel: Model, containing user login and passwords
    * */
    public changePassword(changePasswordViewModel: ChangePasswordViewModel): Observable<ChangePasswordViewModel> {
        let body = changePasswordViewModel;
        return this._http.post("api/user/changepassword", body, RequestOptionsService.getRequestOptions())
            .map((response: Response) => response.json() as ChangePasswordViewModel);
    }

    /**
    * Catch error
    * @param error
    */
    private handleError(error: Response): any {
        return Observable.throw(error.json() as AuthorizedUserInfoViewModel);
    }

    /**
     * Gets id of organization 
     * @param login
     */
    public getOrganizationId(login: string): Observable<OrganizationIdViewModel> {
        return this._http.get('api/User/GetIdOfOrganization/' + login)
            .map((response: Response) => response.json() as OrganizationIdViewModel);
    }

    public getAllUsers(): Observable<UserInfo[]> {
        return this._http.get(GlobalUrlService.getAllUsers, RequestOptionsService.getRequestOptions())
            .map((response: Response) => response.json() as UserInfo[]);
    }
}

