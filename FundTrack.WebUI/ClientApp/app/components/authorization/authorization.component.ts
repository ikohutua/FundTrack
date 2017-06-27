import { Component, Input } from '@angular/core';
import { Injectable, Inject } from '@angular/core';
import { AuthorizeViewModel } from '../../view-models/concrete/authorization-view.model';
import { UserService } from '../../services/concrete/user.service';
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
    providers: [UserService]
})
export class AuthorizationComponent {
    private errorMessage: string;
    private type: string = "password";
    private glyphyconEye: string = "glyphicon glyphicon-eye-open";
    @Input() private authorizeModel: AuthorizeViewModel = new AuthorizeViewModel("", "");
    public autType: AuthorizationType;
    constructor(private _authorizationService: UserService,
        private _router: Router)
    { }

    /**
     * Set request to service to return authorized user and create session
     */
    login() {
        this.errorMessage = "";
        localStorage.clear();
        this._authorizationService.logIn(this.authorizeModel)
            .subscribe(a => {
                this.autType = a;
                this.errorMessage = a.errorMessage;
                localStorage.setItem(keys.keyToken, this.autType.access_token);
                if (!this.errorMessage) {
                    localStorage.setItem(keys.keyModel, JSON.stringify(this.autType.userModel));
                    this._router.navigate(['/']);
                    //console.log("role" + this.autType.userModel.role);
                    //console.log("token " + this.autType.access_token);
                }
                else {
                    localStorage.setItem(keys.keyError, this.autType.errorMessage);
                }
                
            })
    }

    /**
     * Show or not show password and change the icon
     */
    showPassword() {
        if (this.type == "password") {
            this.type = "text";
            return this.glyphyconEye = "glyphicon glyphicon-eye-close";
        }
        else {
            this.type = "password";
            return this.glyphyconEye = "glyphicon glyphicon-eye-open";
        }
    }
}