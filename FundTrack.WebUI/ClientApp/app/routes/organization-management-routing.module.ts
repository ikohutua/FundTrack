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
import { UserResponseComponent } from '../components/user-response/user-response.component';
import { DetailInfoRequestedItemComponent } from "../components/organization-management-request/detail-info-request.component";
import { OrganizationManagementEventDetailComponent } from "../components/organization-management-events/organization-management-event-detail.component";
import { AdminRouteGuard } from "../services/concrete/security/admin-route-guard";
import { OrganizationEditComponent } from '../components/organization-edit/organization-edit.component';
import { AllOrganizationsComponent } from '../components/all-organizations/all-organizations.component';

@NgModule({
    providers: [AdminRouteGuard],
    imports: [RouterModule.forChild([
        { path: 'orgbanned', component: OrganizationBannedComponent },
        {
            path: 'organization/events/:id',
            component: OrganizationManagementAllEventsComponent,
            canActivate: [AdminRouteGuard]
        },
        {
            path: 'organization/event/edit/:id',
            component: OrganizationManadementEventEditComponent,
            canActivate: [AdminRouteGuard]
        },
        {
            path: 'organization/event/add/:id',
            component: OrganizationManagementEventAddComponent,
            canActivate: [AdminRouteGuard]
        },
        {
            path: 'organization/event/detail/:id',
            component: OrganizationManagementEventDetailComponent,
            canActivate: [AdminRouteGuard]
        },
        {
            path: 'organization/requests/:id',
            component: OrganizationManagementRequestComponent,
            canActivate: [AdminRouteGuard]
        },
        {
            path: 'organization/request/manage/:idOrganization',
            component: OrganizationManageRequestComponent,
            canActivate: [AdminRouteGuard]
        },
        {
            path: 'organization/request/manage/:idOrganization/:idRequest',
            component: OrganizationManageRequestComponent,
            canActivate: [AdminRouteGuard]
        },
        {
            path: 'organization/request/delete',
            component: OrganizationDeleteRequestComponent,
            canActivate: [AdminRouteGuard]
        },
        {
            path: 'organization/request/response/:idOrganization',
            component: UserResponseComponent,
            canActivate: [AdminRouteGuard]
        },
        {
            path: 'organization/request/detail',
            component: DetailInfoRequestedItemComponent,
            canActivate: [AdminRouteGuard]
        }, 
        { 
            path: 'organization/edit/:id',
            component: OrganizationEditComponent
        },
        {
            path: 'organization/allOrganizations',
            component: AllOrganizationsComponent
        }
    ])],
    exports: [RouterModule]
})
export class OrganizationManagementRoutingModule { }