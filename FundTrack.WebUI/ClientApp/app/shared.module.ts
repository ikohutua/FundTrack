import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { DropdownOrganizationsComponent } from "./shared/components/dropdown-filtering/dropdown-filtering.component";
import { DropdownOrganizationFilterPipe } from "./shared/pipes/organization-list.pipe";
import { EventFilterPipe } from "./shared/pipes/events.pipe";
import { UserStatesComponent } from "./shared/components/user-authorize-states/user-states.component";
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { PaginationComponent } from './shared/components/pagination/pagination.component';
import { PageItemComponent } from './shared/components/pagination/page-item.component';
import { ModalComponent } from './shared/components/modal/modal-component';
import { SelectComponent } from './shared/components/select/select.component';
import { SpinnerComponent } from './shared/components/spinner/spinner.component';
import { SidebarComponent } from "./shared/components/sidebar/sidebar.component";
import { GalleryComponent } from './shared/components/gallery/gallery.component';

import { TruncatePipe } from "./shared/pipes/truncate.pipe";
//import { ChatBoxComponent } from './shared/components/chat-box/chat-box.component';
import { ImageItemComponent } from "./shared/components/upload-image/image-item.component/image-item.component";
import { ImageListComponent } from "./shared/components/upload-image/image-list.component/image-list.component";


@NgModule({
    declarations: [
        DropdownOrganizationsComponent,
        DropdownOrganizationFilterPipe,
        UserStatesComponent,
        PaginationComponent,
        PageItemComponent,
        ModalComponent,
        TruncatePipe,
        EventFilterPipe,
        SelectComponent,
        SpinnerComponent,
        SidebarComponent,
        GalleryComponent,
        ImageItemComponent,
        ImageListComponent
    ],
    imports: [
        CommonModule,
        FormsModule,
        RouterModule,
        ReactiveFormsModule
    ],
    exports: [
        RouterModule,
        CommonModule,
        FormsModule,
        DropdownOrganizationsComponent,
        DropdownOrganizationFilterPipe,
        UserStatesComponent,
        PaginationComponent,
        PageItemComponent,
        ModalComponent,
        EventFilterPipe,
        SelectComponent,
        SpinnerComponent,
        SidebarComponent,
        GalleryComponent,
        TruncatePipe,
        ImageItemComponent,
        ImageListComponent
    ]
})
export class SharedModule { }