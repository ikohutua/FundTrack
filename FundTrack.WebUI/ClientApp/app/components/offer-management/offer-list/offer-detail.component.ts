import { Component, OnInit, Output, EventEmitter } from "@angular/core";
import { IOfferViewModel } from "../../../view-models/abstract/offer-model.interface";
import { OfferViewModel } from "../../../view-models/concrete/offer-view.model";
import { Router, ActivatedRoute, Params } from "@angular/router";
import { GoodsService } from "../../../services/concrete/goods/goods.service";
import { GoodsCategoryViewModel } from "../../../view-models/concrete/goods-category-view.model";
import { GoodsTypeViewModel } from "../../../view-models/concrete/goods-type-view.model";
import { AmazonUploadComponent } from '../../../shared/components/amazonUploader/amazon-upload.component';
import * as key from '../../../shared/key.storage';
import { AuthorizeUserModel } from "../../../view-models/concrete/authorized-user-info-view.model";
import { isBrowser } from "angular2-universal";
import { UserOfferService } from "../../../services/concrete/offer-management/user-offer.service";
import { Observable } from "rxjs/Observable";
import { OfferedItemImageViewModel } from '../../../view-models/concrete/offered-item-image-view.model';

@Component({
    selector: 'offer-detail',
    templateUrl: './offer-detail.component.html',
    styleUrls: ['./offer-detail.component.css'],
    providers: [GoodsService, UserOfferService]
})
export class OfferDetailComponent implements OnInit {
    public uploader: AmazonUploadComponent = new AmazonUploadComponent();
    @Output() hidePanel = new EventEmitter<boolean>();
    private maxDescriptionLength: number = 2000;
    private header: string;
    private offerItem = new OfferViewModel();
    private showUserRegistrationSpinner: boolean = false;
    private _goodsTypes = new Array<GoodsTypeViewModel>();
    private _selectedType=new GoodsTypeViewModel();

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
                this.offerItem.images = new Array<OfferedItemImageViewModel>();
                this.offerItem.images = this.convertFromArray(this.offerItem);
                if (this.offerItem.goodsTypeId!=null) {
                    this.setGoodsType(this.offerItem.goodsTypeId);
                }
                if (data.id == null) {
                    this.header = 'Створити пропозицію';
                }
                else {
                    this.header = 'Редагувати пропозицію';
                }
            });
        this.fillGoodtypes();
        //this._goodsService.getTypes()
        //    .subscribe(types => {
        //        this.types = types;
        //    });
        //this._goodsService.getCategories()
        //    .subscribe(categories => {
        //        this.categories = categories;
        //        this.categories = this.categories.filter(a => a.goodsTypeId == this.offerItem.goodsTypeId);
        //    });
        if (isBrowser) {
            if (localStorage.getItem(key.keyToken)) {
                var user = JSON.parse(localStorage.getItem(key.keyModel)) as AuthorizeUserModel;
                this.offerItem.userId = user.id;
            }
        };
        
        
    }
    private setGoodsType(goodTypeId: number) {
        this._selectedType = this._goodsTypes.find(c => c.id == goodTypeId);
    }

    private getFileExtension(fileName: string): string {
        return fileName.substring(fileName.lastIndexOf('.') + 1, fileName.length) || fileName;
    }
    private submit(offerItem: OfferViewModel): void {
        this.showUserRegistrationSpinner = true;
        this.offerItem.imageUrl = this.convertIntoArray(this.offerItem);
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
    private saveFileInAws(fileInput: any): any {
        
        var that = this;
        var maxFileSize = 4000000;
        let file = fileInput.target.files[0];
        let uploadedFileName = this.offerItem.userId + Date.now().toLocaleString() + '.' + this.getFileExtension(file.name);
        if (file.size != null && file.size < maxFileSize) {
            this.uploader.UploadImageToAmazon(file, uploadedFileName).then(function (data) {
                let offeredItemImage = new OfferedItemImageViewModel();
                offeredItemImage.offeredItemId = 0;
                offeredItemImage.imageUrl = data.Location;

                if (that.offerItem.images == null) {
                    that.offerItem.images = [];
                }
                that.offerItem.images.push(offeredItemImage);
            });
        }
        else {
            alert('Розмір файлу не може перевищувати ' + Math.ceil(maxFileSize / 1000000) + 'МБ');
        }
    }
    private convertIntoArray(offerItem: OfferViewModel) {
        let imageArray = new Array<string>();
        for (var i = 0; i < offerItem.images.length; i++) {
            imageArray.push(offerItem.images[i].imageUrl);
        }
        return imageArray;
    }
    private convertFromArray(offerItem: OfferViewModel) {
        for (var i = 0; i < offerItem.imageUrl.length; i++) {
            let tempImage = new OfferedItemImageViewModel();
            tempImage.imageUrl = offerItem.imageUrl[i];
            tempImage.offeredItemId = this.offerItem.id;
            offerItem.images.push(tempImage);
        }
        return offerItem.images;
    }
}