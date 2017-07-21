import { Component, OnInit, Input, HostListener } from "@angular/core";
import { IOfferViewModel } from "../../../view-models/abstract/offer-model.interface";
import { OfferViewModel } from "../../../view-models/concrete/offer-view.model";
import { UserOfferService } from "../../../services/concrete/offer-management/user-offer.service";
import { Router } from "@angular/router";
import { isBrowser } from "angular2-universal";
import * as key from '../../../shared/key.storage';
import { AuthorizeUserModel } from "../../../view-models/concrete/authorized-user-info-view.model";
import { OfferedItemImageViewModel } from "../../../view-models/concrete/offered-item-image-view.model";
import { OfferItemChangeStatusViewModel } from "../../../view-models/concrete/offer-item-change-status-view.model";

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
    private _currentPage: number = 1;
    private _itemsPerPage: number = 4;
    
    constructor(private _router: Router,
        private _offerService: UserOfferService) {

    }
    ngOnInit(): void {
        if (isBrowser) {
            if (localStorage.getItem(key.keyModel)) {
                this.user = JSON.parse(localStorage.getItem(key.keyModel)) as AuthorizeUserModel;
            }
        };
        this._offerService.getInitialData(this.user.id)
            .subscribe(offers => {
                this.offers = offers;
                this.checkForMissingImages(this.offers);
                this.setMainImage(this.offers);
            });
        
    }
    private navigateToEdit(selected: OfferViewModel) {
        this._router.navigate(['add', selected.id]);
    }
    private deleteOfferItem(offerItem: OfferViewModel) {
        this._offerService.deleteOffer(offerItem.id)
            .subscribe(() => {
                this.offers.splice(this.offers.findIndex(o => o.id == offerItem.id), 1);
            });
    }
    private changeOfferItemStatus(offerItem: OfferViewModel) {
        let model = new OfferItemChangeStatusViewModel();
        model.offerItemId = offerItem.id;
        model.userId = this.user.id;
        if (offerItem.statusName == 'Активний') {
            model.offerItemStatus = 'Неактивний';
            this._offerService.changeOfferItemStatus(model)
                .subscribe(data => {
                    if (data.errorMessage == null) {
                        offerItem.statusName = 'Неактивний';
                    }
                    else {
                        alert(data.errorMessage);
                    }
                })
        }
        else if (offerItem.statusName == 'Неактивний') {
            model.offerItemStatus = 'Активний';
            this._offerService.changeOfferItemStatus(model)
                .subscribe(data => {
                    if (data.errorMessage == null) {
                        offerItem.statusName = 'Активний';
                    }
                    else {
                        alert(data.errorMessage);
                    }
                })
        }
        else return;
    }
    ///Delete action confirmation
    private deleteConfirm(offerItem: OfferViewModel) {
        if (confirm("Ви справді хочете видалити цю пропозицію?")) {
            this.deleteOfferItem(offerItem);
        }
    }
    private goToEditPage(selected: OfferViewModel) {
        this._router.navigate(['offer-management/offerdetail', selected.id]);
    }

    private getOfferedItemsOnScroll(): void{
        this._offerService.getPagedUserOffers(this.user.id, this._itemsPerPage, this._currentPage)
            .subscribe(offers => {
                this.offers = this.offers.concat(offers);
                this.checkForMissingImages(this.offers.splice(this._currentPage * this._itemsPerPage, this._currentPage * this._itemsPerPage + this._itemsPerPage));
                this.setMainImage(this.offers);
            });
    }

    @HostListener('window:scroll', ['$event'])
    onScroll($event: Event): void {
        if ((window.innerHeight + window.scrollY) >= document.body.offsetHeight) {
                this._currentPage = this._currentPage + 1;
                this.getOfferedItemsOnScroll();
            
        }
    }
    private checkForMissingImages(offers: OfferViewModel[]): void {
        for (var i = 0; i < this.offers.length; i++) {
            if (this.offers[i].image.length == 0) {
                let fakeImage = new OfferedItemImageViewModel();
                fakeImage.id = 1;
                fakeImage.imageUrl = 'https://s3.eu-central-1.amazonaws.com/fundtrack/default_image_placeholder.png';
                fakeImage.isMain = true;
                fakeImage.offeredItemId = this.offers[i].id;
                this.offers[i].image.push(fakeImage);
            }
        }
    }
    private setMainImage(offers: OfferViewModel[]): void {
        for (var i = 0; i < offers.length; i++) {
            for (var j = 0; j < offers[i].image.length; j++) {
                let currentImage = offers[i].image[j];
                if (currentImage.isMain) {
                    offers[i].mainImage = currentImage;
                    break;
                }
            }
        }
    }
}