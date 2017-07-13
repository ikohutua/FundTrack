import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { OrganizationManagementRoutingModule } from "./routes/organization-management-routing.module";
import { FormsModule } from "@angular/forms";
import { TruncatePipe } from "./shared/pipes/truncate.pipe";
import { OrganizationManagementEventsService } from "./services/concrete/organization-management/organization-management-events.service";
import { OrganizationBannedComponent } from './shared/components/error-pages/organization-banned.component';
import { OrganizationManagementRequestComponent } from "./components/organization-management-request/organization-management-request.component";
import { OrganizationCreateRequestComponent } from "./components/organization-management-request/organization-create-request.component";
import { OrganizationDeleteRequestComponent } from "./components/organization-management-request/organization-delete-request.component";
import { OrganizationManagementComponent } from "./components/organization-management-events/organization-management.component";
import { OrganizationManagementEventsComponent } from "./components/organization-management-events/organization-management-event.component";
import { OrganizationManadementEventEditComponent } from "./components/organization-management-events/organization-manadement-event-edit.component";
import { OrganizationManagementEventAddComponent } from "./components/organization-management-events/organization-management-event-add.component";


@NgModule({
    declarations: [
        OrganizationManagementComponent,
        OrganizationManagementEventsComponent,
        TruncatePipe,
        OrganizationBannedComponent,
        OrganizationManagementRequestComponent,
        OrganizationCreateRequestComponent,
        OrganizationDeleteRequestComponent,
        OrganizationManadementEventEditComponent,
        OrganizationManagementEventAddComponent
    ],
    imports: [
        FormsModule,
        CommonModule,
        OrganizationManagementRoutingModule
    ],
    exports: [
        TruncatePipe
    ],
    providers: [
        OrganizationManagementEventsService
    ]
})
export class OrganizationManagementModule { }