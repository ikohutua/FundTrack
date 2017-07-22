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
                this._images = this.offerItem.image;
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
        this.offerItem.image = this._images;
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
        debugger;
        var that = this;
        var maxFileSize = 4000000;
        let file = fileInput.target.files[0];
        let uploadedFileName = this.offerItem.userId + Date.now().toLocaleString() + '.' + this.getFileExtension(file.name);
        if (file.size != null && file.size < maxFileSize) {
            this.uploader.UploadImageToAmazon(file, uploadedFileName).then(function (data) {
                if (!data.Location) {
                    that.saveFileInAws(fileInput);
                }
                else {
                    let offeredItemImage = new OfferedItemImageViewModel();
                    offeredItemImage.offeredItemId = 0;
                    offeredItemImage.imageUrl = data.Location;
                    if (!that._images || that._images.length==0) {
                        that._images = new Array<OfferedItemImageViewModel>();
                        offeredItemImage.isMain = true;
                    }
                    that._images.push(offeredItemImage);
                }
                
            });
        }
        else {
            alert('Розмір файлу не може перевищувати ' + Math.ceil(maxFileSize / 1000000) + 'МБ');
        }
    }

    private deleteCurrentImage(image: OfferedItemImageViewModel) {
        this.offerItem.image.splice(this.offerItem.image.findIndex(a => a.imageUrl == image.imageUrl), 1);
        if (image.isMain == true) {
            this.offerItem.image[0].isMain = true;
        }
    }
    private setCurrentImageAsMain(image: OfferedItemImageViewModel) {
        if (this.offerItem.image) {
            for (var i = 0; i < this.offerItem.image.length; i++) {
                this.offerItem.image[i].isMain = false;
            }
            image.isMain = true;
        }
    }
}