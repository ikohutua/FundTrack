import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { SuperAdminComponent } from "../components/super-admin/super-admin.component";
import { SuperAdminComplaintsComponent } from '../components/super-admin/super-admin-complaints.component';
import { SuperAdminOrganizationsComponent } from '../components/super-admin/super-admin-organizations.component';
import { SuperAdminUsersComponent } from '../components/super-admin/super-admin-users.component';
import { OrganizationRegistrationComponent } from '../components/super-admin/organization-registration.component';
import { SuperAdminRouteGuard } from '../services/concrete/security/superadmin-route-guard';
@NgModule({
    providers: [SuperAdminRouteGuard],
    imports: [
        RouterModule.forChild([
            {
                path: 'superadmin',
                component: SuperAdminComponent,
                canActivate: [SuperAdminRouteGuard],
                children: [              
                {
                    path: 'complaints',
                    component: SuperAdminComplaintsComponent
                },
                {
                    path: 'organizations',
                    component: SuperAdminOrganizationsComponent
                },
                {
                    path: 'users',
                    component:SuperAdminUsersComponent
                },
                {
                    path: 'register-organization',
                    component: OrganizationRegistrationComponent
                }]
            }          
        ])
    ],
    exports: [RouterModule]
})
export class SuperAdminRoutingModule{ }