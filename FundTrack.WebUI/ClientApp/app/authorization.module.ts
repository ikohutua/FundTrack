import { NgModule } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { AuthorizationComponent } from './components/authorization/authorization.component';
import { RegistrationComponent } from './components/registration/registration.component';
import { AuthorizationRoutingModule } from "./routes/authorization-routing.module";
import { CommonModule } from "@angular/common";

//create module for authorization users
@NgModule({
    declarations: [
        RegistrationComponent,
        AuthorizationComponent
    ],
    imports: [
        CommonModule,
        FormsModule,
        AuthorizationRoutingModule
    ]
})
export class AuthorizationModule { }