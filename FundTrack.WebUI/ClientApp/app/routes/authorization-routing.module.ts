import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { RegistrationComponent } from "../components/registration/registration.component";
import { AuthorizationComponent } from "../components/authorization/authorization.component";
import { UserProfileComponent } from '../components/user-profile/user-profile.component';
import { LoginRouteGuard } from '../services/concrete/security/login-route-guard';
import { ErrorAuthorizeComponent } from '../shared/components/error-authorize/error-authorize.component';

@NgModule({
    providers: [LoginRouteGuard],
    imports: [
        RouterModule.forChild([
            { path: 'login', component: AuthorizationComponent },
            { path: 'registration', component: RegistrationComponent },
            {
                path: 'userprofile',
                component: UserProfileComponent,
                canActivate: [LoginRouteGuard]
            },
            { path: 'errorauthorize', component: ErrorAuthorizeComponent }
        ])
    ],
    exports: [RouterModule]
})
export class AuthorizationRoutingModule { }