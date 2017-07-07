import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { OrganizationManagementComponent } from "../components/organization-management/organization-management.component";
import { OrganizationManagementEventComponent } from "../components/organization-management-event/organization-management-event.component";

@NgModule({
    imports: [RouterModule.forChild([
        {
            path: 'organization-management/:id', component: OrganizationManagementComponent,
            children: [
                { path: 'all-events', component: OrganizationManagementEventComponent }
            ]
        }
    ])],
    exports: [RouterModule]
})
export class OrganizationManagementRoutingModule { }