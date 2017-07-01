import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AuthorizationComponent } from './components/authorization/authorization.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { UserProfileComponent } from './components/user-profile/user-profile.component';
import { AuthorizationRoutingModule } from "./routes/authorization-routing.module";
import { ErrorAuthorizeComponent } from './shared/components/error-authorize/error-authorize.component';
import { CommonModule } from "@angular/common";
import { ReactiveFormsModule } from '@angular/forms';
import { EqualTextValidator } from "angular2-text-equality-validator";
import * as key from '../app/shared/key.storage';
import { SharedModule } from './shared.module';
import { isBrowser } from 'angular2-universal';
import { Angular2SocialLoginModule } from "angular2-social-login";
import { BeginPasswordResetComponent } from './components/password-reset/begin-password-reset.component';
import { NewPasswordComponent } from './components/password-reset/new-password.component';

let provider = {
    "facebook": {
        "clientId": '108153859816185',
        "apiVersion": "v2.9"
    }
};


//module for authorization users
@NgModule({
    declarations: [
        RegistrationComponent,
        AuthorizationComponent,
        ErrorAuthorizeComponent,
        UserProfileComponent,
        EqualTextValidator,
        BeginPasswordResetComponent,
        NewPasswordComponent
    ],
    imports: [
        CommonModule,
        FormsModule,
        AuthorizationRoutingModule,
        ReactiveFormsModule,
        Angular2SocialLoginModule,
        SharedModule
    ]
})
export class AuthorizationModule { }
if (isBrowser) {
    Angular2SocialLoginModule.loadProvidersScripts(provider);
}



