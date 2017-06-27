import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { DropdownOrganizationsComponent } from "./shared/components/dropdown-filtering/dropdown-filtering.component";
import { DropdownOrganizationFilterPipe } from "./shared/pipes/organization-list.pipe";
import { UserStatesComponent } from "./shared/components/user-authorize-states/user-states.component";
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { PaginationComponent } from './shared/components/pagination/pagination.component';
import { PageItemComponent } from './shared/components/pagination/page-item.component';
import { ModalComponent } from './shared/components/modal/modal-component';
import { SelectComponent } from './shared/components/select/select.component';

@NgModule({
    declarations: [
        DropdownOrganizationsComponent,
        DropdownOrganizationFilterPipe,
        UserStatesComponent,
        PaginationComponent,
        PageItemComponent,
        ModalComponent,
        SelectComponent
    ],
    imports: [
        CommonModule,
        FormsModule,
        RouterModule,
        ReactiveFormsModule,      
    ],
    exports: [
        CommonModule,
        FormsModule,
        DropdownOrganizationsComponent,
        DropdownOrganizationFilterPipe,
        UserStatesComponent,
        PaginationComponent,
        PageItemComponent,
        ModalComponent,
        SelectComponent
    ]
})
export class SharedModule { }