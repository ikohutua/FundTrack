import { Component, OnInit, Input, HostListener, SimpleChanges, OnChanges } from "@angular/core";
import { IOfferViewModel } from "../../../view-models/abstract/offer-model.interface";
import { OfferViewModel } from "../../../view-models/concrete/offer-view.model";
import { UserOfferService } from "../../../services/concrete/offer-management/user-offer.service";
import { Router } from "@angular/router";
import { isBrowser } from "angular2-universal";
import * as key from '../../../shared/key.storage';
import { AuthorizeUserModel } from "../../../view-models/concrete/authorized-user-info-view.model";
import { OfferedItemImageViewModel } from "../../../view-models/concrete/offered-item-image-view.model";
import { OfferItemChangeStatusViewModel } from "../../../view-models/concrete/offer-item-change-status-view.model";
import { OfferFilteringService } from "../../../services/concrete/offer-management/offer-filtering.service";
import { Subscription } from "rxjs/Subscription";

@Component({
    selector: 'offer-list',
    templateUrl: './offer-list.component.html',
    styleUrls: ['./offer-list.component.css'],
    providers: [UserOfferService]
})
export class OfferListComponent implements OnInit {
    @Input() showActive: boolean;
    @Input() showInactive: boolean;
    showRemoved: boolean;
    private _subscriptionActive: Subscription;
    private _subscriptionInactive: Subscription;
    private date = new Date().toJSON().slice(0, 10).replace(/-/g, '/');
    private user: AuthorizeUserModel;
    private offers: OfferViewModel[] = new Array<OfferViewModel>();
    private _currentPage: number = 1;
    private _itemsPerPage: number = 4;
    private preFilterOffers:OfferViewModel[] = new Array<OfferViewModel>();
    constructor(private _router: Router,
        private _offerService: UserOfferService,
        private _filterService: OfferFilteringService
    ) {
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
        this._subscriptionActive = this._filterService.activeObs$
            .subscribe(item => {
                if (this.preFilterOffers.length==0) {
                    this.preFilterOffers = this.offers;
                }
                this.showActive = item;
                if (this.showActive == false) {
                    this.offers = this.preFilterOffers.filter(offers => offers.statusName != 'Активний');
                }
                else {
                    this.offers = this.preFilterOffers;
                }
            });
        this._subscriptionInactive = this._filterService.inactiveObs$
            .subscribe(item => {
                if (this.preFilterOffers.length == 0) {
                    this.preFilterOffers = this.offers;
                }
                this.showInactive = item;
                if (this.showInactive == false) {
                    this.offers = this.preFilterOffers.filter(offers => offers.statusName != 'Неактивний');
                }
                else {
                    this.offers = this.preFilterOffers;
                }
            });
    }
    /*
    Navigates to add new offer page
    */
    private navigateToEdit(selected: OfferViewModel) {
        this._router.navigate(['add', selected.id]);
    }
    /*
    Deletes offer item
    */
    private deleteOfferItem(offerItem: OfferViewModel) {
        this._offerService.deleteOffer(offerItem.id)
            .subscribe(() => {
                this.offers.splice(this.offers.findIndex(o => o.id == offerItem.id), 1);
            });
    }
    /*
    Changes offer item status
    */
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
    /*
    Navigates to offer editing page
    */
    private goToEditPage(selected: OfferViewModel) {
        this._router.navigate(['offer-management/offerdetail', selected.id]);
    }
    /*
    Gets offer items, when page scrolling occurs
    */
    private getOfferedItemsOnScroll(): void{
        this._offerService.getPagedUserOffers(this.user.id, this._itemsPerPage, this._currentPage)
            .subscribe(offers => {
                this.offers = this.offers.concat(offers);
                this.checkForMissingImages(this.offers.splice(this._currentPage * this._itemsPerPage, this._currentPage * this._itemsPerPage + this._itemsPerPage));
                this.setMainImage(this.offers);
                this.preFilterOffers = this.offers.concat(offers);
            });
    }
    /*
    Listens to scroll event on the page and performs loading new offer items when page hits bottom
    */
    @HostListener('window:scroll', ['$event'])
    onScroll($event: Event): void {
        if ((window.innerHeight + window.scrollY) >= document.body.offsetHeight) {
                this._currentPage = this._currentPage + 1;
                this.getOfferedItemsOnScroll();
            
        }
    }
    /*
    Checks offer items for missing images and sets image placeholders
    */
    private checkForMissingImages(offers: OfferViewModel[]): void {
        for (var i = 0; i < this.offers.length; i++) {
            if (this.offers[i].images.length == 0) {
                let fakeImage = new OfferedItemImageViewModel();
                fakeImage.id = 1;
                fakeImage.imageUrl = 'https://s3.eu-central-1.amazonaws.com/fundtrack/default_image_placeholder.png';
                fakeImage.isMain = true;
                fakeImage.offeredItemId = this.offers[i].id;
                this.offers[i].images.push(fakeImage);
            }
        }
    }
    /*
    Sets main image of an offer item
    */
    private setMainImage(offers: OfferViewModel[]): void {
        debugger;

        for (var i = 0; i < offers.length; i++) {
            for (var j = 0; j < offers[i].images.length; j++) {
                let currentImage = offers[i].images[j];
                if (currentImage.isMain) {
                    offers[i].mainImage = currentImage;
                    break;
                }
            }
        }
    }

    ngOnDestroy() {
        this._subscriptionActive.unsubscribe();
        this._subscriptionInactive.unsubscribe();
    }
}