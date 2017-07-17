import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { BannedOrgGuard } from '../services/concrete/security/banned-org-guard';
import { OrganizationBannedComponent } from '../shared/components/error-pages/organization-banned.component';
import { OrganizationManagementRequestComponent } from "../components/organization-management-request/organization-management-request.component";
import { OrganizationManageRequestComponent } from "../components/organization-management-request/organization-manage-request.component";
import { OrganizationDeleteRequestComponent } from "../components/organization-management-request/organization-delete-request.component";
import { OrganizationManadementEventEditComponent } from "../components/organization-management-events/organization-manadement-event-edit.component";
import { OrganizationManagementEventAddComponent } from "../components/organization-management-events/organization-management-event-add.component";
import { OrganizationManagementAllEventsComponent } from "../components/organization-management-events/organization-management-all-events.component";

@NgModule({
    providers: [BannedOrgGuard],
    imports: [RouterModule.forChild([
        //{
        //    path: 'organization/:id', component: OrganizationManagementComponent,
        //    canActivate: [BannedOrgGuard],
        //},
        { path: 'orgbanned', component: OrganizationBannedComponent },
        { path: 'organization/events/:id', component: OrganizationManagementAllEventsComponent },
        { path: 'organization/event/edit/:id', component: OrganizationManadementEventEditComponent },
        { path: 'organization/event/add/:id', component: OrganizationManagementEventAddComponent },
        { path: 'organization/requests/:id', component: OrganizationManagementRequestComponent },
        { path: 'organization/request/manage/:idOrganization', component: OrganizationManageRequestComponent },
        { path: 'organization/request/manage/:idOrganization/:idRequest', component: OrganizationManageRequestComponent },
        { path: 'organization/request/delete', component: OrganizationDeleteRequestComponent }
    ])],
    exports: [RouterModule]
})
export class OrganizationManagementRoutingModule { }