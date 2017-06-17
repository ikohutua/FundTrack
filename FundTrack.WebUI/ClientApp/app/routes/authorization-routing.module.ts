import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { RegistrationComponent } from "../components/registration/registration.component";
import { AuthorizationComponent } from "../components/authorization/authorization.component";

@NgModule({
    imports: [
        RouterModule.forChild([
            { path: 'login', component: AuthorizationComponent },
            { path: 'registration', component: RegistrationComponent }
        ])
    ],
    exports: [RouterModule]
})
export class AuthorizationRoutingModule{ }