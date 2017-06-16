import { NgModule } from "@angular/core";
import { DropdownOrganizationsComponent } from "./shared/components/dropdown-filtering/dropdown-filtering.component";
import { CommonModule } from "@angular/common";
import { DropdownOrganizationFilterPipe } from "./shared/pipes/organization-list.pipe";
import { FormsModule } from "@angular/forms";
import { RouterModule } from "@angular/router";

/** This component has added because dropdown has links. In future it will be changed on OrganizationPageComponent. */
import { HomeComponent } from "./components/home/home.component";

@NgModule({
    declarations: [
        DropdownOrganizationsComponent,
        DropdownOrganizationFilterPipe
    ],
    imports: [
        CommonModule,
        FormsModule,
        RouterModule.forChild([
            { path: '', component: HomeComponent },
        ])
    ],
    exports: [
        CommonModule,
        FormsModule,
        DropdownOrganizationsComponent,
        DropdownOrganizationFilterPipe
    ]
})
export class SharedModule { }