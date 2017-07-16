import { Component, OnInit, Input } from "@angular/core";
import { IOfferViewModel } from "../../../view-models/abstract/offer-model.interface";
import { OfferViewModel } from "../../../view-models/concrete/offer-view.model";
import { UserOfferService } from "../../../services/concrete/offer-management/user-offer.service";
import { Router } from "@angular/router";
import { isBrowser } from "angular2-universal";
import * as key from '../../../shared/key.storage';
import { AuthorizeUserModel } from "../../../view-models/concrete/authorized-user-info-view.model";

@Component({
    selector: 'offer-list',
    templateUrl: './offer-list.component.html',
    styleUrls: ['./offer-list.component.css'],
    providers: [UserOfferService]
})
export class OfferListComponent implements OnInit {
    @Input('showActive') showActive: boolean;
    @Input('showInactive') showInactive: boolean;
    @Input('showRemoved') showRemoved: boolean;
    private date = new Date().toJSON().slice(0, 10).replace(/-/g, '/');
    private user: AuthorizeUserModel;
    private offers: OfferViewModel[] = new Array<OfferViewModel>();
    constructor(private _router: Router,
        private _offerService: UserOfferService) {

    }
    ngOnInit(): void {
        if (isBrowser) {
            if (localStorage.getItem(key.keyModel)) {
                this.user = JSON.parse(localStorage.getItem(key.keyModel)) as AuthorizeUserModel;
            }
        };
        this._offerService.getUserOffers(this.user.id)
            .subscribe(offers => {
                this.offers = offers;
            });
    }
    public navigateToEdit(selected: OfferViewModel) {
        this._router.navigate(['add', selected.id]);
    }
    public deleteOfferItem(offerItem: OfferViewModel) {
        this._offerService.deleteOffer(offerItem.id)
            .subscribe(() => {
                this.offers.splice(this.offers.findIndex(o => o.id == offerItem.id), 1);
            });
    }
    ///Delete action confirmation
    public deleteConfirm(offerItem: OfferViewModel) {
        if (confirm("Ви справді хочете видалити цю пропозицію?")) {
            this.deleteOfferItem(offerItem);
        }
    }
    public goToEditPage(selected: OfferViewModel) {
        this._router.navigate(['offer-management/offerdetail', selected.id]);
    }
}