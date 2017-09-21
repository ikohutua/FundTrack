import { NgModule } from "@angular/core";
import { OfferManagementRoutingModule } from "./routes/offer-management-routing.module";
import { CommonModule } from "@angular/common";
import { OrganizationManagementModule } from "./organization-management.module";
import { FormsModule } from '@angular/forms';
import { OfferItemManagementComponent } from "./components/offer-management/offer-list/offer-management.component";
import { OfferListComponent } from "./components/offer-management/offer-list/offer-list.component";
import { OfferDetailComponent } from "./components/offer-management/offer-list/offer-detail.component";
import { SharedModule } from "./shared.module";
import { OfferFilteringService } from "./services/concrete/offer-management/offer-filtering.service";
import { ImageUploadModule } from "./image-upload.module";
//import { ImageItemComponent } from "./shared/components/upload-image/image-item.component/image-item.component";
//import { ImageListComponent } from "./shared/components/upload-image/image-list.component/image-list.component";


@NgModule({
    declarations: [
        OfferItemManagementComponent,
        OfferListComponent,
        OfferDetailComponent,
        //ImageItemComponent,
        //ImageListComponent
    ],
    imports: [
        CommonModule,
        OfferManagementRoutingModule,
        OrganizationManagementModule,
        FormsModule,
        SharedModule        
    ],
    providers:[OfferFilteringService]
})
export class OfferManagementModule { }