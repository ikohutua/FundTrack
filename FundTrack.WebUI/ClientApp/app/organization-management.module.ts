import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { OrganizationManagementRoutingModule } from "./routes/organization-management-routing.module";
import { FormsModule } from "@angular/forms";
import { OrganizationManagementEventsService } from "./services/concrete/organization-management/organization-management-events.service";
import { OrganizationBannedComponent } from './shared/components/error-pages/organization-banned.component';
import { OrganizationManagementRequestComponent } from "./components/organization-management-request/organization-management-request.component";
import { OrganizationManageRequestComponent } from "./components/organization-management-request/organization-manage-request.component";
import { OrganizationDeleteRequestComponent } from "./components/organization-management-request/organization-delete-request.component";
import { OrganizationManagementAllEventsComponent } from "./components/organization-management-events/organization-management-all-events.component";
import { OrganizationManagementEventAddComponent } from "./components/organization-management-events/organization-management-event-add.component";
import { OrganizationManadementEventEditComponent } from "./components/organization-management-events/organization-manadement-event-edit.component";
import { OrganizationManagementEventDeleteComponent } from "./components/organization-management-events/organization-management-event-delete.component";
import { SharedModule } from "./shared.module";
import { SpinnerComponent } from "./shared/components/spinner/spinner.component";
import { UserResponseComponent } from './components/user-response/user-response.component';
import { HomeModule } from "./home.module";
import { GalleryComponent } from "./shared/components/gallery/gallery.component";
import { DetailInfoRequestedItemComponent } from "./components/organization-management-request/detail-info-request.component";
import { OrganizationManagementEventDetailComponent } from "./components/organization-management-events/organization-management-event-detail.component";
import { OrganizationEditComponent } from "./components/organization-edit/organization-edit.component";
import { MapModule } from './map.module';
import { AllOrganizationsComponent } from "./components/all-organizations/all-organizations.component";

@NgModule({
    declarations: [
        OrganizationManagementAllEventsComponent,
        OrganizationBannedComponent,
        OrganizationManagementRequestComponent,
        OrganizationManageRequestComponent,
        OrganizationDeleteRequestComponent,
        OrganizationManadementEventEditComponent,
        OrganizationManagementEventAddComponent,
        OrganizationManagementEventDeleteComponent,
        OrganizationManagementEventDetailComponent,
        UserResponseComponent,
        DetailInfoRequestedItemComponent,
        //SpinnerComponent    
        //GalleryComponent
        OrganizationManagementEventDeleteComponent, 
        OrganizationEditComponent,
        AllOrganizationsComponent
    ],
    imports: [
        FormsModule,
        CommonModule,
        OrganizationManagementRoutingModule,
        SharedModule, 
        MapModule
    ],
    exports: [
        SpinnerComponent
    ],
    providers: [
        OrganizationManagementEventsService
    ]
})
export class OrganizationManagementModule { }