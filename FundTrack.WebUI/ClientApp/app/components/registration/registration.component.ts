import { Component } from '@angular/core';
import { Router } from '@angular/router';
import { RegistrationViewModel } from '../../view-models/concrete/registration-view.model';
import { UserService } from "../../services/concrete/user.service";
import * as keys from '../../shared/key.storage';
import { AuthorizedUserInfoViewModel } from '../../view-models/concrete/authorized-user-info-view.model';
import { FormControl, FormGroup, AbstractControl, Validators, FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { matchingPasswords } from './match-password.validator';
import { ValidationViewModel } from "../../view-models/concrete/validation-view.model";

@Component({
    selector: 'registration',
    template: require('./registration.component.html'),
    styles: [require('./registration.component.css')],
    providers: [UserService]
})
    
export class RegistrationComponent {
    public autType: AuthorizedUserInfoViewModel;
    private registrationViewModel: RegistrationViewModel = new RegistrationViewModel();
    private errorMessage: string;
    private type: string = "password";
    private glyphyconEye: string = "glyphicon glyphicon-eye-open";
    private registrationForm: FormGroup;

    //Error messages
    private requiredMessage = "Обовязкове поле для заповнення";
    private maxLengthMessage = "Значення не повинно бути більше 20 символів";
    private minLengthMessage = "Мінімальна кількість символів повинна бути більша 7";
    private patternLoginMessage = "Невірний формат login";
    private patternEmailMessage = "Невірний формат email адреса";
    private mismatchedPassword = "Паролі не співпадають";

    //Regex patterns
    private loginRegexPattern: string = "^[a-zA-Z](.[a-zA-Z0-9_-]*)$";
    private emailRegexPattern: string = "^([a-z0-9_-]+\.)*[a-z0-9_-]+@[a-z0-9_-]+(\.[a-z0-9_-]+)*\.[a-z]{2,6}$";

    constructor(private _router: Router,
                private _userService: UserService,
                private _formBuilder: FormBuilder)
    {
        this.buldForm();
    }

    /**
     * Creates new user
     */
    private register() {
        this.errorMessage = "";
        localStorage.clear();
        this._userService.create(this.registrationViewModel).
            subscribe(a => {
            this.autType = a;
            console.log(this.autType.validationSummary);
            this.errorMessage = a.errorMessage;
            localStorage.setItem(keys.keyToken, this.autType.access_token);
            if (this.errorMessage) {
                localStorage.setItem(keys.keyError, this.autType.errorMessage);
            }
            else if (this.autType.validationSummary)
            {
                this.displayValidationSummary(this.autType.validationSummary);
            }
            else {
                localStorage.setItem(keys.keyModel, JSON.stringify(this.autType.userModel));
                this._router.navigate(['/']);              
            }
        });
    }

    private displayValidationSummary(validationSummary: ValidationViewModel[]): void {
        for (var i = 0; i < validationSummary.length; i++)
        {
            var fieldValidation = validationSummary[i];           
            var error: string = "";
            for (var errorMessage of fieldValidation.ErrorsMessages)
            {
                error += errorMessage + " ";
            }
            var key = fieldValidation.FieldName.charAt(0).toLowerCase() + fieldValidation.FieldName.slice(1);
            this.formErrors[key] = error;
        }
    }

    /**
     * Method for show password as a text
     */
    private showPassword(): string {
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
    private formErrors = {
        firstName: "",
        lastName: "",
        login: "",
        email: "",
        password: "",
        confirmPassword: "",
        mismatchingPasswords: ""
    };

    //Object with error messages
     private validationMessages = {
        firstName: {
            required: this.requiredMessage,
            maxlength: this.maxLengthMessage
        },
        lastName: {
            required: this.requiredMessage,
            maxlength: this.maxLengthMessage
        },
        login: {
            required: this.requiredMessage,
            pattern: this.patternLoginMessage
        },
        email: {
            required: this.requiredMessage,
            pattern: this.patternEmailMessage
        },
        password: {
            required: this.requiredMessage,
            minlength: this.minLengthMessage
        },
        confirmPassword: {
            required: this.requiredMessage,
            minlength: this.minLengthMessage,
            mismatchingpasswords: this.mismatchedPassword 
        }
    };

    /**
     * Initialize form
     */
    private buldForm() {
        this.registrationForm = this._formBuilder.group({
            firstName: [this.registrationViewModel.firstName, [
                Validators.required,
                Validators.maxLength(20)
            ]],
            lastName: [this.registrationViewModel.lastName, [
                Validators.required,
                Validators.maxLength(20)
            ]],
            login: [this.registrationViewModel.login, [
                Validators.required,
                Validators.pattern(this.loginRegexPattern)
            ]],
            email: [this.registrationViewModel.email, [
                Validators.required,
                Validators.pattern(this.emailRegexPattern)
            ]],
            password: [this.registrationViewModel.password, [
                Validators.required,
                Validators.minLength(7)
            ]],
            confirmPassword: [this.registrationViewModel.passwordConfrim, [
                Validators.required,
                Validators.minLength(7)
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
    private onValueChange(data?: any)
    {
        if (!this.registrationForm) return;
        let form = this.registrationForm;

        for (let field in this.formErrors) {
            this.formErrors[field] = "";
            let control = form.get(field);

            if (control && control.dirty && !control.valid) {
                let message = this.validationMessages[field];
                for (let key in control.errors) {
                    this.formErrors[field] += message[key.toLowerCase()] + "";
                }
            }
        }
    }
}