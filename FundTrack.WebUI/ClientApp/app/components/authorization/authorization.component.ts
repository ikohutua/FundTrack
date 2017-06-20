import { Component, Input } from '@angular/core';
import { Injectable, Inject } from '@angular/core';
import { AuthorizeViewModel } from '../shared/authorization-view.model';
import { AuthorizationService } from '../../services/authorization.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch';
import { AuthorizationType } from '../shared/authorization.type';
import { Headers, RequestOptions } from '@angular/http';
import { AppComponent } from '../app/app.component';

@Component({
    template: require('./authorization.component.html'),
    styles: [require('./authorization.component.css')],
    providers: [AuthorizationService]
})
export class AuthorizationComponent {
    private _authorizationUrl = '/api/User/';
    private tokenKey: string = "accessToken";
    private errorMessage: string;
    private showPassword: boolean = false;
    @Input() private authorizeModel: AuthorizeViewModel = new AuthorizeViewModel("", "");
    /**
    * return type which contains login authorized user, his token and error which can appear in backend
    */
    public autType: AuthorizationType = new AuthorizationType("", "");
    constructor(private _authorizationService: AuthorizationService,
        private _router: Router, private _app: AppComponent) { }
    /**
     * set request to service to return authorized user and create session
     */
    login() {
        this.errorMessage = "";
        sessionStorage.clear();
        console.log(this.authorizeModel.login);
        this._authorizationService.logIn(this.authorizeModel)
            .subscribe(a => this.autType = a,
            errorMessage => this.errorMessage = <any>errorMessage,
            () => {
                sessionStorage.setItem(this.tokenKey, this.autType.access_token);
                this._app.setLogin(this.autType.username);
            });
    }
    /**
     * check if user is authorized and his law to can access to some method
     * @param login
     */
    check(login: string) {
        let token = sessionStorage.getItem(this.tokenKey);
        let loginUser = "";
        this._authorizationService.check(login, token)
            .subscribe((l) => {
                loginUser = l;
                console.log("Result: " + loginUser)
            });
    }
    /**
     * show or not show password
     */
    changePasswordType() {
        this.showPassword = !this.showPassword;
    }
}