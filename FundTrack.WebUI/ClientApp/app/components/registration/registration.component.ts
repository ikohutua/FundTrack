import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { RegistrationViewModel } from '../../view-models/concrete/registration-view.model';
import { UserService } from "../../services/concrete/user.service";
import * as keys from '../../shared/key.storage';
import { AuthorizationType } from '../../view-models/concrete/authorization.type';

@Component({
    selector: 'registration',
    template: require('./registration.component.html'),
    styles: [require('./registration.component.css')],
    providers: [UserService]
})

export class RegistrationComponent {
    public autType: AuthorizationType;
    private registrationViewModel: RegistrationViewModel = new RegistrationViewModel();
    private errorMessage: string;
    private type: string = "password";
    private glyphyconEye: string = "glyphicon glyphicon-eye-open";

    constructor(private _router: Router,
        private _userService: UserService)
    { }

    register() {
        this.errorMessage = "";
        sessionStorage.clear();
        this._userService.create(this.registrationViewModel).subscribe(a => {
            this.autType = a;
            this.errorMessage = a.errorMessage;
            localStorage.setItem(keys.keyToken, this.autType.access_token);
            if (!this.errorMessage) {
                localStorage.setItem(keys.keyModel, JSON.stringify(this.autType.userModel));
                this._router.navigate(['/']);
            }
            else {
                localStorage.setItem(keys.keyError, this.autType.errorMessage);
            }
        });
    }

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