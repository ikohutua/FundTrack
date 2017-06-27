import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { RegistrationViewModel } from '../../view-models/concrete/registration-view.model';
import { UserService } from "../../services/concrete/user.service";
import * as keys from '../../shared/key.storage';
import { AuthorizationType } from '../../view-models/concrete/authorization.type';
import { FormControl, FormGroup, AbstractControl, Validators, FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { matchingPasswords } from './match-password.validator';

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
    registrationForm: FormGroup;

    constructor(private _router: Router,
                private _userService: UserService,
                private _formBuilder: FormBuilder)
    {
        this.buldForm();
    }

    /**
     * Creates new user
     */
    register() {
        this.errorMessage = "";
        localStorage.clear();
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

    /**
     * Sauron eye :)
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

    //Object with errors wich will be displayed on interface
    formErrors = {
        "firstName": "",
        "lastName": "",
        "login": "",
        "email": "",
        "password": "",
        "confirmPassword": "",
        "mismatchingPasswords": ""
    };

    //Object with error messages
    validationMessages = {
        "firstName": {
            "required": "Обовязкове поле для заповнення",
            "maxLength": "Значення не повинно бути більше 20 символів"
        },
        "lastName": {
            "required": "Обовязкове поле для заповнення",
            "maxLength": "Значення не повинно бути більше 20 символів"
        },
        "login": {
            "required": "Обовязкове поле для заповнення",
            "pattern": "Невірний формат login"
        },
        "email": {
            "required": "Обовязкове поле для заповнення",
            "pattern": "Невірний формат email адреса"
        },
        "password": {
            "required": "Обовязкове поле для заповнення",
            "minLength": "Мінімальна кількість символів повинна бути більша 7"
        },
        "confirmPassword": {
            "required": "Обовязкове поле для заповнення",
            "minLength": "Мінімальна кількість символів повинна бути більша 7",
            "mismatchingPasswords": "Паролі не співпадають"
        }
    };

    /**
     * Initialize form
     */
    public buldForm() {
        this.registrationForm = this._formBuilder.group({
            "firstName": [this.registrationViewModel.firstName, [
                Validators.required,
                Validators.maxLength(20)
            ]],
            "lastName": [this.registrationViewModel.lastName, [
                Validators.required,
                Validators.maxLength(20)
            ]],
            "login": [this.registrationViewModel.login, [
                Validators.required,
                Validators.pattern("^[a-zA-Z](.[a-zA-Z0-9_-]*)$")
            ]],
            "email": [this.registrationViewModel.email, [
                Validators.required,
                Validators.pattern("^([a-z0-9_-]+\.)*[a-z0-9_-]+@[a-z0-9_-]+(\.[a-z0-9_-]+)*\.[a-z]{2,6}$")
            ]],
            "password": [this.registrationViewModel.password, [
                Validators.required,
                Validators.minLength(6)
            ]],
            "confirmPassword": [this.registrationViewModel.passwordConfrim, [
                Validators.required,
                Validators.minLength(6)
            ]]
        },
            { validator: matchingPasswords('password', 'confirmPassword') });

        this.registrationForm.valueChanges
            .subscribe(data => this.onValueChange(data));

        this.onValueChange();
    }

    /**
     * Subscriber on value changes
     * @param data
     */
    onValueChange(data?: any)
    {
        if (!this.registrationForm) return;
        let form = this.registrationForm;

        for (let field in this.formErrors) {
            this.formErrors[field] = "";
            //Form get
            let control = form.get(field);

            if (control && control.dirty && !control.valid) {
                let message = this.validationMessages[field];
                for (let key in control.errors) {
                    this.formErrors[field] += message[key] + "";
                }
            }
        }
    }
}