import { Component, OnInit } from '@angular/core';
import { ResetPasswordViewModel } from '../../view-models/concrete/reset-password-view-model';
import { ActivatedRoute } from '@angular/router';
import { Router } from '@angular/router';
import { UserService } from '../../services/concrete/user.service'; 
import { GuidViewModel } from '../../view-models/concrete/guid-view-model';
import { FormControl, FormGroup, AbstractControl, Validators, FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { matchingPasswords } from '../registration/match-password.validator';
import 'rxjs/add/operator/switchMap';

@Component({
    selector: 'new-pass-reset',
    template: require('./new-password.component.html'),
    providers: [UserService]
})

export class NewPasswordComponent implements OnInit { 
    public resetPasswordModel: ResetPasswordViewModel = new ResetPasswordViewModel();
    public errorMessage: string = '';
    public guid: string = '';
    public type: string = "password";
    public glyphyconEye: string = "glyphicon glyphicon-eye-open";
    public passwordChanged: boolean = false;
    public passwordForm: FormGroup;

    /**
     * Creates new instance of NewPasswordComponent
     * @param _route
     * @param _userService
     */
    constructor(private _route: ActivatedRoute,
                private _userService: UserService,
                private _navRouter: Router,
                private _formBuilder: FormBuilder) {
        this.buildForm();
    }

    /**
     * Trigers when the Component is created
     */
    ngOnInit() : void {    
        this._route.params.subscribe(params =>
        {
            this.guid = params['id'];

            let model = new GuidViewModel();
            model.guid = params['id'];

            this._userService.checkGuid(model).subscribe((message: string) => {
                if (message.length > 0) {
                    this.errorMessage = message
                }
            });
        })
    }

    /**
     * Calls server to change password
     */
    public changePassword() : void {
        this.resetPasswordModel.guid = this.guid;

        console.log(this.guid);

        this._userService.resetPassword(this.resetPasswordModel).subscribe((responce: string) => {
            if (responce.length == 0) {
                this.passwordChanged = true;
            } else {
                this.errorMessage = responce;
            }          
        });
    }

    /**
     * Shows passwords
     */
    public showPassword() : string {
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
    public formErrors = {        
        "password": "",
        "confirmPassword": "",
        "mismatchingPasswords": ""
    };

    //Object with error messages
    public validationMessages = {       
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
    public buildForm() : void {
        this.passwordForm = this._formBuilder.group({
            "password": [this.resetPasswordModel.newPassword, [
                Validators.required,
                Validators.minLength(6)
            ]],
            "confirmPassword": [this.resetPasswordModel.newPasswordConfirm, [
                Validators.required,
                Validators.minLength(6)
            ]]
        },
            { validator: matchingPasswords('password', 'confirmPassword') });

        this.passwordForm.valueChanges
            .subscribe(data => this.onValueChange(data));

        this.onValueChange();
    }

    /**
     * Subscriber on value changes
     * @param data
     */
    public onValueChange(data?: any) : void {
        if (!this.passwordForm) return;
        let form = this.passwordForm;

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