import { UserInfoService } from '../../services/user-info.service';
import { Component, OnInit } from '@angular/core';
import { UserInfo } from '../../view-models/concrete/user-info.model';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { AbstractControl } from '@angular/forms';
import { EqualTextValidator } from "angular2-text-equality-validator";
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms'

@Component({
    selector: 'user-info',
    templateUrl: './user-profile.component.html',
    styleUrls: ['./user-profile.component.css'],
    providers: [UserInfoService, FormBuilder]
})
    //User Profile component handles editing of user profile page
export class UserProfileComponent implements OnInit {
    public user = new UserInfo();
    private errorMessage: string;
    private passwordEdit: boolean = true;
    private userPassword: string = "";
    private userConfirmPassword: string = "";
    //Reactive form
    userForm: FormGroup;
    //Object to keep errors in UI
    formErrors = {
        "userFirstName": "",
        "userLastName": "",
        "userEmail": "",
        "userLogin": "",
        "userAddress": "",
        "userConfirmPassword": ""
    };

    //Object with errors messages
    validationMessages = {
        "userFirstName": {
            "required": "Поле є обов'язковим",
            "minlength": "Значення не може бути коротшим 2х символів",
            "maxlength": "Значення не може бути довшим 20 символів"
        },
        "userLastName": {
            "required": "Поле є обов'язковим",
            "minlength": "Значення не може бути коротшим 2х символів",
            "maxlength": "Значення не може бути довшим 20 символів"
        },
        "userEmail": {
            "required": "Поле є обов'язковим",
            "pattern": "Формат email адреси не вірний"
        },
        "userLogin": {
            "required": "Поле є обов'язковим",
            "minlength": "Значення не може бути коротшим 3х символів",
            "maxlength": "Значення не може бути довшим 20 символів"
        },
        "userAddress": {
            "required": "Поле є обов'язковим"
        },
        "userConfirmPassword": {
            "required": "Поле є обов'язковим"
        }
    }
    //Injecting dependecies
    constructor(private userService: UserInfoService,
        private fb: FormBuilder) {
    }
    ngOnInit(): void {
        this.userService.getCurrentUser()
            .subscribe(user => this.user = user);
        this.buildForm();
    }
    //enables/disables visibility of password fields
    private EditPassword() {
        this.passwordEdit = true;
    }
    //Builds a form using FormBuilder and subscribes to its changes
    buildForm() {
        this.userForm = this.fb.group({
            "userFirstName": [this.user.userFirstName, [
                Validators.required,
                Validators.minLength(2),
                Validators.maxLength(20)
            ]
            ],
            "userLastName": [this.user.userLastName, [
                Validators.required,
                Validators.minLength(2),
                Validators.maxLength(20)
            ]
            ],
            "userEmail": [this.user.userEmail, [
                Validators.required,
                Validators.pattern("[a-zA-Z0-9._%-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}")
            ]],
            "userLogin": [this.user.userLogin, [
                Validators.required,
                Validators.minLength(3),
                Validators.maxLength(20)
            ]
            ],
            "userAddress": [this.user.userAddress, [
                Validators.required
            ]],
            "userConfirmPassword": [this.userConfirmPassword, [
                Validators.required,
            ]],

            "userPassword": [this.userPassword, [
                Validators.required,
            ]]
        });

        this.userForm.valueChanges
            .subscribe(data => this.onValueChange(data));
        this.onValueChange();
    }
    //Handler for changing values in the form
    onValueChange(data?: any) {
        if (!this.userForm) return;
        let form = this.userForm;

        for (let field in this.formErrors) {
            this.formErrors[field] = "";
            //Getting control element
            let control = form.get(field);

            if (control && control.dirty && !control.valid) {
                let message = this.validationMessages[field];
                for (let key in control.errors) {
                    this.formErrors[field] += message[key] + " ";
                }
            }
        }
    }
    onSubmit() {
        console.log("submitted");
        console.log(this.userForm.value);

    }
}
