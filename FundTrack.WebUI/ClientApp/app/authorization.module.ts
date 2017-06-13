import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { UniversalModule } from 'angular2-universal';
import { FormsModule } from '@angular/forms';
import { AuthorizationComponent } from './components/authorization/authorization.component';
//create module for authorization users
@NgModule({
    bootstrap: [AuthorizationComponent],
    declarations: [
        AuthorizationComponent
    ],
    imports: [
        UniversalModule, // Must be first import. This automatically imports BrowserModule, HttpModule, and JsonpModule too.
        FormsModule,
        RouterModule.forRoot([
            { path: 'login', component: AuthorizationComponent }
        ])
    ]
})
export class AuthorizationModule {
   
}