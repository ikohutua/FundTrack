import { Component, Input, NgZone } from '@angular/core';
import { LoginViewModel } from '../../view-models/concrete/login-view.model';
import { UserService } from '../../services/concrete/user.service';
import { Router } from '@angular/router';
import { Response } from '@angular/http';
import { isBrowser } from 'angular2-universal';
import { AuthorizedUserInfoViewModel } from '../../view-models/concrete/authorized-user-info-view.model';
import { AuthService } from "angular2-social-login";
import * as keys from '../../shared/key.storage';
import * as commonMessages from '../../shared/common-message.storage';
import { LoginFacebookViewModel } from '../../view-models/concrete/login-facebook-view.model';
import { StorageService } from "../../shared/item-storage-service";
import { UserResponseService } from "../../services/concrete/organization-management/user-responses.service";

@Component({
    template: require('./authorization.component.html'),
    styles: [require('./authorization.component.css')],
    host: { '(window:keydown)': 'hotkeys($event)' },
    providers: [UserService, UserResponseService]
})
export class AuthorizationComponent {
    private type: string = "password";
    private glyphyconEye: string = "glyphicon glyphicon-eye-open";
    private userAuthorizedInfo: AuthorizedUserInfoViewModel;

    /**
    * Error which contain information about exception which was created on BLL
    */
    public errorMessage: string;

    /**
    * Model which contain password and login which enter user in login form
    */
    @Input() private loginModel: LoginViewModel = new LoginViewModel("", "");

    public constructor(private _authorizationService: UserService,
        private _router: Router,
        private _auth: AuthService,
        private _storage: StorageService,
        private _userResponseService: UserResponseService,
        private _ngZone: NgZone) {
    }

    /**
    * Information which get facebook about user
    */
    private userRecievedFromFacebook = {
        email: "",
        image: "",
        name: "",
        provider: "",
        token: "",
        uid: ""
    }

    /**
     * Send request to service to authorize user from facebook
     * @param provider
     */
    public loginWithFacebook(provider) {
        this.errorMessage = "";
        this.loginModel.login = "";
        this.loginModel.password = "";
        let userNamesFacebook;
        let userForAuthorization: LoginFacebookViewModel = new LoginFacebookViewModel();
        localStorage.clear();
        this._auth.login(provider)
            .subscribe(data => {
                this.userRecievedFromFacebook = data as any;
                userNamesFacebook = this.userRecievedFromFacebook.name.split(" ", 2);
                userForAuthorization.email = this.userRecievedFromFacebook.email;
                userForAuthorization.firstName = userNamesFacebook[0];
                userForAuthorization.lastName = userNamesFacebook[1];
                userForAuthorization.login = this.userRecievedFromFacebook.email;
                userForAuthorization.password = this.userRecievedFromFacebook.provider;
                userForAuthorization.photoUrl = this.userRecievedFromFacebook.image;
                userForAuthorization.fbLink = this.userRecievedFromFacebook.uid;
                this._ngZone.run(() => {
                    this._authorizationService.logInWithFacebook(userForAuthorization)
                        .subscribe(data => {
                            this.subscribeForAuthorization(data);
                        })
                });
                this._ngZone.run(() => {
                    this._auth.logout()
                        .subscribe(data => {
                        });
                });

            });
    }

    /**
     * Send request to service to authorize user
     */
    public login() {
        this.errorMessage = "";
        this._authorizationService.logIn(this.loginModel)
            .subscribe(data => {
                this._authorizationService.getAccessToken(this.loginModel)
                    .subscribe(res => {
                        data.access_token = res;
                        this.subscribeForAuthorization(data);
                    }, error => {
                        this.errorMessage = commonMessages.invalidLoginOrPassword;
                    });
            });
    }

    /**
     * Show or not show password and change the icon
     */
    public showPassword() {
        if (this.type == "password") {
            this.type = "text";
            return this.glyphyconEye = "glyphicon glyphicon-eye-close";
        }
        else {
            this.type = "password";
            return this.glyphyconEye = "glyphicon glyphicon-eye-open";
        }
    }

    /**
     * Create session for authorized user, check is authorization had some error
     * @param user
     */
    private subscribeForAuthorization(user: AuthorizedUserInfoViewModel) {
        this.userAuthorizedInfo = user;
        this.errorMessage = user.errorMessage;
        localStorage.clear();
        localStorage.setItem(keys.keyToken, this.userAuthorizedInfo.access_token);
        if (!this.errorMessage) {
            localStorage.setItem(keys.keyModel, JSON.stringify(this.userAuthorizedInfo.userModel));
            this._userResponseService.getUserResponseWithNewStatus(this.userAuthorizedInfo.userModel.orgId)
                .subscribe(count => {
                    this._storage.emitAuthorizeUserEvent(this.userAuthorizedInfo.userModel);
                    this._storage.emitNavChangeEvent(count)
                    sessionStorage.setItem(keys.keyNewResponse, count.toString());
                });
            this._router.navigate(['/']);
        }
        else {
            this._authorizationService.logOff();
            localStorage.setItem(keys.keyError, this.userAuthorizedInfo.errorMessage);
        }
    }

    public hotkeys(event) {
        if (event.keyCode == 13) {
            this.login();
        }
    }
}