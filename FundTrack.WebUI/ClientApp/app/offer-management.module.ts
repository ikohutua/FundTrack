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



@NgModule({
    declarations: [
        OfferItemManagementComponent,
        OfferListComponent,
        OfferDetailComponent
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