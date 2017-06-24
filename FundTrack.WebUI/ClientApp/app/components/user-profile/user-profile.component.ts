import { UserInfoService } from '../../services/user-info.service';
import { Component, OnInit, ViewChild, AfterContentChecked } from '@angular/core';
import { UserInfo } from '../../view-models/concrete/user-info.model';
import { FormBuilder, FormControl, FormGroup, Validators } from '@angular/forms';
import { AbstractControl } from '@angular/forms';
import { EqualTextValidator } from "angular2-text-equality-validator";
import { FormsModule } from '@angular/forms';
import { ReactiveFormsModule } from '@angular/forms';
import { ModalComponent } from '../../shared/components/modal/modal-component';
import * as key from '../../shared/key.storage';
import { isBrowser } from "angular2-universal";
import { AuthorizeUserModel } from "../../view-models/concrete/authorization.type";
import { ChangePasswordContainer } from "../../view-models/concrete/change-password";
import { UserService } from '../../services/concrete/user.service';
import { Router } from "@angular/router";

@Component({
    selector: 'user-info',
    templateUrl: './user-profile.component.html',
    styleUrls: ['./user-profile.component.css'],
    providers: [UserInfoService, FormBuilder, UserService]
})
//User Profile component handles editing of user profile page
export class UserProfileComponent implements OnInit{
    @ViewChild(ModalComponent)
    public modal: ModalComponent;
    user: AuthorizeUserModel = new AuthorizeUserModel();
    private errorMessage: string;
    private passwordEdit: boolean = true;
    private passwordContainer: ChangePasswordContainer = new ChangePasswordContainer();

    //Reactive form
     userForm: FormGroup;
     passwordForm: FormGroup;
    //Object to keep errors in UI
    formErrors = {
        "firstName": "",
        "lastName": "",
        "email": "",
        "login": "",
        "address": "",
        "newPasswordConfirmation": ""
    };

    //Object with errors messages
     validationMessages = {
        "firstName": {
            "required": "Поле є обов'язковим",
            "minlength": "Значення не може бути коротшим 2х символів",
            "maxlength": "Значення не може бути довшим 20 символів"
        },
        "lastName": {
            "required": "Поле є обов'язковим",
            "minlength": "Значення не може бути коротшим 2х символів",
            "maxlength": "Значення не може бути довшим 20 символів"
        },
        "email": {
            "required": "Поле є обов'язковим",
            "pattern": "Формат email адреси не вірний"
        },
        "login": {
            "required": "Поле є обов'язковим",
            "minlength": "Значення не може бути коротшим 3х символів",
            "maxlength": "Значення не може бути довшим 20 символів"
        },
        "address": {
            "required": "Поле є обов'язковим"
        },
        "newPasswordConfirmation": {
            "required": "Поле є обов'язковим"
        }
    }
    //Injecting dependecies
    constructor(
        private userService: UserService,
        private fb: FormBuilder,
        private router: Router
    ) {

    }
    //Gets user profile info from localstorage
    //Builds reactive forms for the page
    ngOnInit(): void {
        let data: any;
        if (isBrowser) {
            if (localStorage.getItem(key.keyToken)) {
                this.user = JSON.parse(localStorage.getItem(key.keyModel)) as AuthorizeUserModel;
            }
        };
        this.buildForm();
        this.buildPasswordForm();
        this.user.photoUrl = 'http://orig13.deviantart.net/f725/f/2013/241/4/c/profile_picture_by_doge_intensifies-d6k8a2r.jpg';
    }
    //Builds a form using FormBuilder and subscribes to its changes
    buildPasswordForm() {
        this.passwordForm = this.fb.group({
            "userOldPassword": [this.passwordContainer.oldPassword, [
                Validators.required
            ]],
            "userPassword": [this.passwordContainer.newPassword, [
                Validators.required,
            ]],
            "userConfirmPassword": [this.passwordContainer.newPasswordConfirmation, [
                Validators.required,
            ]]
        })
        this.passwordForm.valueChanges
            .subscribe(data => this.onValueChange(data));
        this.onValueChange();
    }
    //Builds a form using FormBuilder and subscribes to its changes
    buildForm() {
        this.userForm = this.fb.group({
            "firstName": [this.user.firstName, [
                Validators.required,
                Validators.minLength(2),
                Validators.maxLength(20)
            ]
            ],
            "lastName": [this.user.lastName, [
                Validators.required,
                Validators.minLength(2),
                Validators.maxLength(20)
            ]
            ],
            "email": [this.user.email, [
                Validators.required,
                Validators.pattern("[a-zA-Z0-9._%-]+@[a-zA-Z0-9.-]+\.[a-zA-Z]{2,4}")
            ]],
            "login": [this.user.login, [
                Validators.required,
                Validators.minLength(3),
                Validators.maxLength(20)
            ]
            ],
            "address": [this.user.address, [
                Validators.required
            ]]
        });

        this.userForm.valueChanges
            .subscribe(data => this.onValueChange(data));
        this.onValueChange();
    }
    //Handler for changing values in the form
    onValueChange(data?: any) {
        if (!this.userForm || !this.passwordForm) return;
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
        debugger;
        this.userService.editUserProfile(this.user)
            .subscribe(data => 
            {
                localStorage.setItem(key.keyModel, JSON.stringify(this.user));
                this.router.navigate(['/']);
            })
            ;
    }
    //Executes when user clicks Change Password button
    private onPasswordChange() {
        this.passwordContainer.oldPassword = '';
        this.passwordContainer.newPassword = '';
        this.passwordContainer.newPasswordConfirmation = '';
        this.modal.show();
    }
}
