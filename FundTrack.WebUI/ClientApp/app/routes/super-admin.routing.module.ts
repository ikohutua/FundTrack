import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { SuperAdminComponent } from "../components/super-admin/super-admin.component";
import { SuperAdminComplaintsComponent } from '../components/super-admin/super-admin-complaints.component';
import { SuperAdminOrganizationsComponent } from '../components/super-admin/super-admin-organizations.component';
import { SuperAdminUsersComponent } from '../components/super-admin/super-admin-users.component';
import { AdminRouteGuard } from '../services/concrete/security/admin-route-guard';
@NgModule({
    providers: [AdminRouteGuard],
    imports: [
        RouterModule.forChild([
            {
                path: 'superadmin',
                component: SuperAdminComponent,
                canActivate: [AdminRouteGuard],
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
                }]
            }          
        ])
    ],
    exports: [RouterModule]
})
export class SuperAdminRoutingModule{ }