import { Component } from '@angular/core';
import { UserService } from '../../services/concrete/user.service';
import { Router } from '@angular/router';
import { FormControl, FormGroup, AbstractControl, Validators, FormBuilder, ReactiveFormsModule } from '@angular/forms';
import { UserEmailViewModel } from '../../view-models/user-email-view-model';

@Component({
    selector: 'begin-pass-reset',
    template: require('./begin-password-reset.component.html'),
    providers: [UserService]
})

export class BeginPasswordResetComponent {
    // emailregex
    private _emailRegex: string = "^([a-z0-9_-]+\.)*[a-z0-9_-]+@[a-z0-9_-]+(\.[a-z0-9_-]+)*\.[a-z]{2,6}$";

    /**
    *recovery email view model
    */
    public recoveryEmail: UserEmailViewModel = new UserEmailViewModel();

    /**
    * Error Message instance
    */
    public errorMessage: string = '';

    /**
    * Indicates if the email has been send
    */
    public emailSend: boolean = false;

    /**
    * FormGroup instance
    */
    public emailForm: FormGroup;

    /**
     * Creates new instance of BeginPasswordResetComponent
     * @param _userService
     * @param _router
     */
    constructor(private _userService: UserService, private _router: Router, private _emailBuilder: FormBuilder) {
        this.buildForm();
    }

    /**
     * Calls server to send Email 
     */
    public sendEmail(): void {
        this._userService.checkEmailStatus((this.recoveryEmail)).subscribe((responce: string) => {
            if (responce.length == 0) {
                this.emailSend = true;
                this._userService.sendRecoveryEmail(this.recoveryEmail).subscribe((responce: string) => { });
            } else {
                this.errorMessage = responce;
            }
        });
        
    }

    // forms errors
    public formErrors = {       
        "email": ""       
    };

    //Object with error messages
    public validationMessages = {        
        "email": {
            "required": "Невірний формат email адреса",
            "pattern": "Невірний формат email адреса"
        }
    };

    /**
     * Builds validation form
     */
    public buildForm() : void {
        this.emailForm = this._emailBuilder.group({
            "email": [this.recoveryEmail.email, [
                Validators.required,
                Validators.pattern(this._emailRegex)
            ]]
        });

        this.emailForm.valueChanges
            .subscribe(data => this.onValueChange(data));

        this.onValueChange();
    }

    /**
     * Subscriber on value changes
     * @param data
     */
    public onValueChange(data?: any) : void {
        if (!this.emailForm) return;
        let form = this.emailForm;

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