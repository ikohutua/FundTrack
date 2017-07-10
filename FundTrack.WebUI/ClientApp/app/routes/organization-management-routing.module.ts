import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { OrganizationManagementComponent } from "../components/organization-management/organization-management.component";
import { OrganizationManagementEventComponent } from "../components/organization-management-event/organization-management-event.component";
import { BannedOrgGuard } from '../services/concrete/security/banned-org-guard';
import { OrganizationBannedComponent } from '../shared/components/error-pages/organization-banned.component';

@NgModule({
    providers: [BannedOrgGuard],
    imports: [RouterModule.forChild([
        {
            path: 'organization-management/:id', component: OrganizationManagementComponent,
            canActivate: [BannedOrgGuard],
            children: [
                { path: 'all-events', component: OrganizationManagementEventComponent }
            ]
        },
        { path: 'orgbanned', component: OrganizationBannedComponent }
    ])],
    exports: [RouterModule]
})
export class OrganizationManagementRoutingModule { }