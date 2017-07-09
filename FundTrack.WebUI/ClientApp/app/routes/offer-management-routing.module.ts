import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { OfferItemManagementComponent } from "../components/offer-management/offer-management.component";
import { OfferListComponent } from "../components/offer-management/offer-list/offer-list.component";
import { OfferDetailComponent } from "../components/offer-management/offer-list/offer-detail.component";

@NgModule({
    imports: [RouterModule.forChild([
        {
            path: 'offer-management', component: OfferItemManagementComponent,
            children: [{ path: 'mylist', component: OfferListComponent },
                       { path: 'add', component: OfferDetailComponent  }
            ]
        }
    ])],
    exports: [RouterModule]
})
export class OfferManagementRoutingModule { }