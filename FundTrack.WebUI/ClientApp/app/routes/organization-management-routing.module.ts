import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { OrganizationManagementComponent } from "../components/organization-management/organization-management.component";
import { OrganizationManagementEventComponent } from "../components/organization-management-event/organization-management-event.component";
import { BannedOrgGuard } from '../services/concrete/security/banned-org-guard';
import { OrganizationBannedComponent } from '../shared/components/error-pages/organization-banned.component';
import { OrganizationManagementRequestComponent } from "../components/organization-management-request/organization-management-request.component";
import { OrganizationCreateRequestComponent } from "../components/organization-management-request/organization-create-request.component";
import { OrganizationDeleteRequestComponent } from "../components/organization-management-request/organization-delete-request.component";

@NgModule({
    providers: [BannedOrgGuard],
    imports: [RouterModule.forChild([
        {
            path: 'organization-management/:id', component: OrganizationManagementComponent,
            canActivate: [BannedOrgGuard],
            children: [
                { path: 'all-events', component: OrganizationManagementEventComponent },
                { path: 'all-requests', component: OrganizationManagementRequestComponent },
                { path: 'create-request/:id', component: OrganizationCreateRequestComponent },
                { path: 'create-request', component: OrganizationCreateRequestComponent },
                { path: 'delete-request', component: OrganizationDeleteRequestComponent }
            ]
        },
        { path: 'orgbanned', component: OrganizationBannedComponent }
    ])],
    exports: [RouterModule]
})
export class OrganizationManagementRoutingModule { }