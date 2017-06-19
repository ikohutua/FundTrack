import { NgModule } from '@angular/core';
import { CommonModule } from "@angular/common";
import { SuperAdminComponent } from './components/super-admin/super-admin.component';
import { SuperAdminRoutingModule } from "./routes/super-admin.routing.module";
import { SuperAdminComplaintsComponent } from './components/super-admin/super-admin-complaints.component';
import { SuperAdminOrganizationsComponent } from './components/super-admin/super-admin-organizations.component';
import { SuperAdminUsersComponent } from './components/super-admin/super-admin-users.component';

@NgModule({
    declarations: [
        SuperAdminComponent,
        SuperAdminComplaintsComponent,
        SuperAdminOrganizationsComponent,
        SuperAdminUsersComponent
    ],
    imports: [
        CommonModule,
        SuperAdminRoutingModule
    ]
})
export class SuperAdminModule { }
