import { NgModule } from "@angular/core";
import { DropdownOrganizationsComponent } from "./shared/components/dropdown-filtering/dropdown-filtering.component";
import { DropdownOrganizationFilterPipe } from "./shared/pipes/organization-list.pipe";
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";

@NgModule({
    declarations: [
        DropdownOrganizationsComponent,
        DropdownOrganizationFilterPipe
    ],
    imports: [
        CommonModule,
        FormsModule
    ],
    exports: [
        CommonModule,
        FormsModule,
        DropdownOrganizationsComponent,
        DropdownOrganizationFilterPipe
    ]
})
export class SharedModule { }