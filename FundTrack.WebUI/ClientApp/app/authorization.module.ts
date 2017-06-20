import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AuthorizationComponent } from './components/authorization/authorization.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { UserProfileComponent } from './components/user-profile/user-profile.component';
import { AuthorizationRoutingModule } from "./routes/authorization-routing.module";
import { CommonModule } from "@angular/common";
import { ReactiveFormsModule } from '@angular/forms';
import { EqualTextValidator } from "angular2-text-equality-validator";

//create module for authorization users
@NgModule({
    declarations: [
        RegistrationComponent,
        AuthorizationComponent,
        UserProfileComponent,
        EqualTextValidator
    ],
    imports: [
        CommonModule,
        FormsModule,
        AuthorizationRoutingModule,
        ReactiveFormsModule
    ]
})
export class AuthorizationModule { }