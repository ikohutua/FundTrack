import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { RegistrationViewModel } from '../../view-models/concrete/registration-view.model';
import { UserService } from "../../services/concrete/user.service";
import { FormGroup, FormControl, Validators, FormArray, FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { AppComponent } from "../app/app.component";
import * as keys from '../../shared/key.storage';
import { AuthorizationType } from '../../view-models/concrete/authorization.type';



@Component({
    selector: 'registration',
    template: require('./registration.component.html'),
    styles: [require('./registration.component.css')],
    providers: [UserService]
})

export class RegistrationComponent {
    private registrationViewModel: RegistrationViewModel = new RegistrationViewModel();
    form: FormGroup;
    private errorMessage: string;
    public autType: AuthorizationType;

    constructor(private _router: Router,
                private _userService: UserService,
                private _formBuilder: FormBuilder,
                private _app: AppComponent) { }

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

    buildForm()
    {
        this.form = this._formBuilder.group({
            firstName: ['', [Validators.required, Validators.maxLength(20)]],
            lastName: ['', [Validators.required, Validators.maxLength(20)]],
            login: ['', [Validators.required, Validators.pattern("^[a-zA-Z](.[a-zA-Z0-9_-]*)$")]],
            email: ['', [Validators.required, Validators.pattern("^([a-z0-9_-]+\.)*[a-z0-9_-]+@[a-z0-9_-]+(\.[a-z0-9_-]+)*\.[a-z]{2,6}$")]],
            password: ['', [Validators.required, Validators.minLength(6)]],
            confirmPassword: ['', [Validators.required, Validators.minLength(6)]]
        });
    }


}