import { NgModule } from "@angular/core";
import { OrganizationManagementComponent } from "./components/organization-management/organization-management.component";
import { CommonModule } from "@angular/common";
import { OrganizationManagementRoutingModule } from "./routes/organization-management-routing.module";
import { OrganizationManagementEventComponent } from "./components/organization-management-event/organization-management-event.component";

@NgModule({
    declarations: [
        OrganizationManagementComponent,
        OrganizationManagementEventComponent
    ],
    imports: [
        CommonModule,
        OrganizationManagementRoutingModule
    ]
})
export class OrganizationManagementModule { }