import { NgModule } from "@angular/core";
import { OrganizationManagementComponent } from "./components/organization-management/organization-management.component";
import { CommonModule } from "@angular/common";
import { OrganizationManagementRoutingModule } from "./routes/organization-management-routing.module";
import { OrganizationManagementEventComponent } from "./components/organization-management-event/organization-management-event.component";
import { FormsModule } from "@angular/forms";
import { TruncatePipe } from "./shared/pipes/truncate.pipe";

@NgModule({
    declarations: [
        OrganizationManagementComponent,
        OrganizationManagementEventComponent,
        TruncatePipe
    ],
    imports: [
        FormsModule,
        CommonModule,
        OrganizationManagementRoutingModule
    ],
    exports: [
        TruncatePipe
    ]
})
export class OrganizationManagementModule { }