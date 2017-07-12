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
import { OrganizationRegistrationComponent } from './components/super-admin/organization-registration.component';
import { MapModule } from './map.module';
import { OrganizationRegistrationService } from './services/concrete/organization-registration.service';
import { SuperAdminChatComponent } from './components/super-admin/super-admin-chat.component';

@NgModule({
    declarations: [
        SuperAdminComponent,
        SuperAdminComplaintsComponent,
        SuperAdminOrganizationsComponent,
        SuperAdminUsersComponent,
        SuperAdminGrid,
        SuperAdminBanComponent,
        OrganizationRegistrationComponent,
        SuperAdminChatComponent
    ],
    imports: [
        CommonModule,
        SuperAdminRoutingModule,
        SharedModule, 
        MapModule
    ],
    providers: [OrganizationRegistrationService]
})
export class SuperAdminModule { }
