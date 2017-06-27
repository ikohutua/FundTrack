import { NgModule } from '@angular/core';
import { CommonModule } from "@angular/common";
import { SuperAdminComponent } from './components/super-admin/super-admin.component';
import { SuperAdminRoutingModule } from "./routes/super-admin.routing.module";
import { SuperAdminComplaintsComponent } from './components/super-admin/super-admin-complaints.component';
import { SuperAdminOrganizationsComponent } from './components/super-admin/super-admin-organizations.component';
import { SuperAdminUsersComponent } from './components/super-admin/super-admin-users.component';
import { SuperAdminGrid } from './components/super-admin/super-admin-grid.component';
import { SharedModule } from './shared.module';
import { SuperAdminBanComponent } from './components/super-admin/super-admin-ban.component';

@NgModule({
    declarations: [
        SuperAdminComponent,
        SuperAdminComplaintsComponent,
        SuperAdminOrganizationsComponent,
        SuperAdminUsersComponent,
        SuperAdminGrid,
        SuperAdminBanComponent
    ],
    imports: [
        CommonModule,
        SuperAdminRoutingModule,
        SharedModule
    ]
})
export class SuperAdminModule { }
