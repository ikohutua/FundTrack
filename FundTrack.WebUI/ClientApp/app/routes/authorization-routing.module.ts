import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { RegistrationComponent } from "../components/registration/registration.component";
import { AuthorizationComponent } from "../components/authorization/authorization.component";
import { UserProfileComponent } from '../components/user-profile/user-profile.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            { path: 'login', component: AuthorizationComponent },
            { path: 'registration', component: RegistrationComponent },
            { path: 'userprofile', component: UserProfileComponent }
        ])
    ],
    exports: [RouterModule]
})
export class AuthorizationRoutingModule{ }