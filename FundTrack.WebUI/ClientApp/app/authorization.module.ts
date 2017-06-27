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
import { SharedModule } from './shared.module';

//module for authorization users
@NgModule({
    declarations: [
        RegistrationComponent,
        AuthorizationComponent,
        ErrorAuthorizeComponent,
        UserProfileComponent,
        EqualTextValidator        
    ],
    imports: [
        CommonModule,
        FormsModule,
        AuthorizationRoutingModule,
        ReactiveFormsModule,
        SharedModule
    ]
})
export class AuthorizationModule { }