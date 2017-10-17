import { Component, OnInit, Output, EventEmitter } from "@angular/core";
import { OfferedItemImageViewModel } from '../../../view-models/concrete/offered-item-image-view.model';
import { IOfferViewModel } from "../../../view-models/abstract/offer-model.interface";
import { UserOfferService } from "../../../services/concrete/offer-management/user-offer.service";
import { OfferViewModel } from "../../../view-models/concrete/offer-view.model";
import { Router, ActivatedRoute, Params } from "@angular/router";
import { GoodsService } from "../../../services/concrete/goods/goods.service";
import { GoodsCategoryViewModel } from "../../../view-models/concrete/goods-category-view.model";
import { GoodsTypeShortViewModel } from "../../../view-models/concrete/goods-type-view.model";
import { AmazonUploadComponent } from '../../../shared/components/amazonUploader/amazon-upload.component';
import * as key from '../../../shared/key.storage';
import { AuthorizeUserModel } from "../../../view-models/concrete/authorized-user-info-view.model";
import { isBrowser } from "angular2-universal";
import { Observable } from "rxjs/Observable";
import { Image } from "../../../view-models/concrete/image.model";



@Component({
    selector: 'offer-detail',
    templateUrl: './offer-detail.component.html',
    styleUrls: ['./offer-detail.component.css'],
    providers: [UserOfferService, GoodsService ]
})
export class OfferDetailComponent implements OnInit {
    public uploader: AmazonUploadComponent = new AmazonUploadComponent();
    @Output() hidePanel = new EventEmitter<boolean>();
    private maxDescriptionLength: number = 2000;
    private header: string;
    private offerItem = new OfferViewModel();
    private showUserRegistrationSpinner: boolean = false;
    private _goodsTypes = new Array<GoodsTypeShortViewModel>();
    private _selectedType = new GoodsTypeShortViewModel();
    private _images = new Array<OfferedItemImageViewModel>();
    images: Image[] = [];
    maxImgCount: number = 5;
    maxImgSize: number = 4000000;

    onImageChange(imgArr: Image[]) {
        debugger;
        this.images = imgArr;
    }
    private _errorMessage: string = "";
    constructor(private _router: Router,
        private _goodsService: GoodsService,
        private _offerService: UserOfferService,
        private _route: ActivatedRoute
    ) {
    }
    private fillGoodtypes() {
        this._offerService.getAllGoodsTypes()
            .subscribe(r => {
                this._goodsTypes = r;
            },
            error => {
                this._errorMessage = <any>error;
            })
    }
    ngOnInit() {
        
        this._route.params
            .switchMap((params: Params) => {
                let id = params['id'];
                if (id) {
                    return this._offerService.getUserOfferById(+id);
                }
                else {
                    return <Observable<OfferViewModel>>Observable.create(observer => {
                        observer.next(new OfferViewModel());
                        observer.complete();
                    })
                }
            })
            .subscribe(data => {
                this.offerItem = data;
                if (this.offerItem.goodsTypeId!=null) {
                    this.setGoodsType(this.offerItem.goodsTypeId);
                }
                if (data.id == null) {
                    this.header = 'Створити пропозицію';

                 }
                else {
                    this.header = 'Редагувати пропозицію';
                    //TODO: перевірити чи є фотки при редагуванні і як вони сейваються на беку
                    this.images = this.ConvertOfferedItemImagesToImages(this.offerItem.images);

                }
            });
        this.fillGoodtypes();
       
        if (isBrowser) {
            if (localStorage.getItem(key.keyToken)) {
                var user = JSON.parse(localStorage.getItem(key.keyModel)) as AuthorizeUserModel;
                this.offerItem.userId = user.id;
            }
        };
    }

    private ConvertOfferedItemImagesToImages(offerImages: OfferedItemImageViewModel[]): Image[] {
        let images: Image[] = [];
        for (var i = 0; i < offerImages.length; i++) {
            let img = new Image(offerImages[i].imageUrl, offerImages[i].imageUrl, null);
            img.id = offerImages[i].id;
            img.isMain = offerImages[i].isMain;
            img.imageExtension = offerImages[i].imageExtension;
            images.push(img);
        }
        return images;

    }

    private setGoodsType(goodTypeId: number) {
        this._selectedType = this._goodsTypes.find(c => c.id == goodTypeId);
    }

    private submit(offerItem: OfferViewModel): void {
        this.showUserRegistrationSpinner = true;
        this.offerItem.images = [];

        debugger;
        for (var i = 0; i < this.images.length; i++) {
            let offItImg = new OfferedItemImageViewModel();
            offItImg.id = this.images[i].id == undefined ? -1 : this.images[i].id;
            offItImg.base64Data = this.images[i].base64Data;
            offItImg.isMain = this.images[i].isMain;
            offItImg.imageUrl = this.images[i].imageSrc;
            offItImg.imageExtension = this.images[i].imageExtension;
            this.offerItem.images.push(offItImg);
        };       


        if (this.offerItem.id == null) {
            this._offerService.createOffer(this.offerItem)
                .subscribe(data => {
                    this.showUserRegistrationSpinner = false;
                    this._router.navigate(['/offer-management/mylist'])
                });
        }
        else {
            this._offerService.editOffer(this.offerItem)
                .subscribe(data => {
                    this.showUserRegistrationSpinner = false;
                    this._router.navigate(['/offer-management/mylist'])
                })
        }
    }
}