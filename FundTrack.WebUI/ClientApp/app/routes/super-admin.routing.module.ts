import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { SuperAdminComponent } from "../components/super-admin/super-admin.component";
import { SuperAdminComplaintsComponent } from '../components/super-admin/super-admin-complaints.component';
import { SuperAdminOrganizationsComponent } from '../components/super-admin/super-admin-organizations.component';
import { SuperAdminUsersComponent } from '../components/super-admin/super-admin-users.component';

@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: 'superadmin',
                component: SuperAdminComponent,
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