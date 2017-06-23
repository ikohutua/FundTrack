import { Component} from '@angular/core';
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

            if (!this.errorMessage) {
                localStorage.setItem(keys.keyLogin, this.autType.login);
                localStorage.setItem(keys.keyToken, this.autType.access_token);
                localStorage.setItem(keys.keyId, this.autType.id.toString());
                localStorage.setItem(keys.keyFirstName, this.autType.firstName);
                localStorage.setItem(keys.keyLastName, this.autType.lastName);
                localStorage.setItem(keys.keyEmail, this.autType.email);
                localStorage.setItem(keys.keyAddress, this.autType.address);
                localStorage.setItem(keys.keyPhoto, this.autType.photoUrl);
                this._router.navigate(['/']);
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