import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { DropdownOrganizationsComponent } from "./shared/components/dropdown-filtering/dropdown-filtering.component";
import { DropdownOrganizationFilterPipe } from "./shared/pipes/organization-list.pipe";

@NgModule({
    declarations: [
        DropdownOrganizationsComponent,
        DropdownOrganizationFilterPipe
    ],
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule
    ],
    exports: [
        CommonModule,
        FormsModule,
        DropdownOrganizationsComponent,
        DropdownOrganizationFilterPipe
    ]
})
export class SharedModule { }