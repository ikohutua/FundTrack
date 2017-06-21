import { Component, Input } from '@angular/core';
import { Injectable, Inject } from '@angular/core';
import { AuthorizeViewModel } from '../../view-models/concrete/authorization-view.model';
import { AuthorizationService } from '../../services/authorization.service';
import { ActivatedRoute, Router } from '@angular/router';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch';
import { AuthorizationType } from '../../view-models/concrete/authorization.type';
import { Headers, RequestOptions } from '@angular/http';
import * as keys from '../../shared/key.storage'

@Component({
    template: require('./authorization.component.html'),
    styles: [require('./authorization.component.css')],
    providers: [AuthorizationService]
})
export class AuthorizationComponent {
    private _authorizationUrl = '/api/User/';
    private errorMessage: string;
    private showPassword: boolean = false;
    @Input() private authorizeModel: AuthorizeViewModel = new AuthorizeViewModel("", "");
    public autType: AuthorizationType;
    constructor(private _authorizationService: AuthorizationService,
        private _router: Router)
    { }

    /**
     * Set request to service to return authorized user and create session
     */
    login() {
        this.errorMessage = "";
        sessionStorage.clear();
        console.log(this.authorizeModel.login);
        this._authorizationService.logIn(this.authorizeModel)
            .subscribe(a => {
                this.autType = a;
                this.errorMessage = a.errorMessage;
                localStorage.setItem(keys.keyLogin, this.autType.login);
                localStorage.setItem(keys.keyToken, this.autType.access_token);
                localStorage.setItem(keys.keyId, this.autType.id.toString());
                console.log("IDFDDD"+this.autType.id);
                localStorage.setItem(keys.keyFirstName, this.autType.firstName);
                localStorage.setItem(keys.keyLastName, this.autType.lastName);
                localStorage.setItem(keys.keyEmail, this.autType.email);
                localStorage.setItem(keys.keyAddress, this.autType.address);
                localStorage.setItem(keys.keyPhoto, this.autType.photoUrl);
                if (!this.errorMessage) {
                    this._router.navigate(['/']);
                }
            })
    }

    /**
     * Check if user is authorized and his law to can access to some method
     * @param Login
     */
    check(login: string) {
        let token = localStorage.getItem(keys.keyToken);
        let loginUser = "";
        this._authorizationService.check(login, token)
            .subscribe((l) => {
                loginUser = l;
                console.log("Result: " + loginUser)
            });
    }

    /**
     * Show or not show password
     */
    changePasswordType() {
        this.showPassword = !this.showPassword;
    }
}