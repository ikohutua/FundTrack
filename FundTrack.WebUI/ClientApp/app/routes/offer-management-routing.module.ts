import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { HomeComponent } from "../components/home/home.component";
import { OfferListComponent } from "../components/offer-management/offer-list/offer-list.component";
import { OfferDetailComponent } from "../components/offer-management/offer-list/offer-detail.component";
import { OfferItemManagementComponent } from "../components/offer-management/offer-list/offer-management.component";

@NgModule({
    imports: [RouterModule.forChild([
        {
            path: 'offer-management', component: OfferItemManagementComponent,
            children: [{ path: 'mylist', component: OfferListComponent },
                { path: 'add', component: OfferDetailComponent },
                { path: 'offerdetail/:id', component: OfferDetailComponent}
            ]
        }

    ])],
    exports: [RouterModule]
})
export class OfferManagementRoutingModule { }