import { Component, OnInit, Output, EventEmitter } from "@angular/core";
import { IOfferViewModel } from "../../../view-models/abstract/offer-model.interface";
import { OfferViewModel } from "../../../view-models/concrete/offer-view.model";
import { Router } from "@angular/router";
import { GoodsService } from "../../../services/concrete/goods/goods.service";
import { GoodsCategoryViewModel } from "../../../view-models/concrete/goods-category-view.model";
import { GoodsTypeViewModel } from "../../../view-models/concrete/goods-type-view.model";
import { AmazonUploadComponent } from '../../../shared/components/amazonUploader/amazon-upload.component';
import * as key from '../../../shared/key.storage';
import { AuthorizeUserModel } from "../../../view-models/concrete/authorized-user-info-view.model";
import { isBrowser } from "angular2-universal";


@Component({
    selector: 'offer-detail',
    templateUrl: './offer-detail.component.html',
    styleUrls: ['./offer-detail.component.css'],
    providers:[GoodsService]
})
export class OfferDetailComponent implements OnInit{
    @Output() hidePanel = new EventEmitter<boolean>();
    private selectedItem = new GoodsTypeViewModel();
    public uploader: AmazonUploadComponent = new AmazonUploadComponent();

    private categories = new Array<GoodsCategoryViewModel>();
    private types = new Array <GoodsTypeViewModel>();
    private offerItem = new OfferViewModel();
    constructor(private _router: Router,
        private _goodsService: GoodsService
    ) {

    }
    onSelection(typeId) {
        this._goodsService.getCategories()
            .subscribe(categories => {
                this.categories = categories;
                this.categories = this.categories.filter(a => a.goodsTypeId == typeId);
            });
    }
    ngOnInit() {
        this.selectedItem.id = 0;
        this._goodsService.getTypes()
            .subscribe(types => {
                this.types = types;
            });
        if (isBrowser) {
            if (localStorage.getItem(key.keyToken)) {
                var user = JSON.parse(localStorage.getItem(key.keyModel)) as AuthorizeUserModel;
                this.offerItem.userId = user.id;
            }
        };
    }
    private getFileExtension(fileName: string): string {
        return fileName.substring(fileName.lastIndexOf('.') + 1, fileName.length) || fileName;
    }

    private saveFileInAws(fileInput: any): void {
        var that = this;
        var maxFileSize = 4000000;
        let file = fileInput.target.files[0];
        let uploadedFileName = this.offerItem.userId + Date.now().toLocaleString() + '.' + this.getFileExtension(file.name);
        if (file.size != null && file.size < maxFileSize) {
            this.uploader.UploadImageToAmazon(file, uploadedFileName).then(function (data) {
                for (var i = 0; i < that.offerItem.imageUrl.length; i++) {
                    if (that.offerItem.imageUrl[i] == 'https://s3.eu-central-1.amazonaws.com/fundtrack/default-placeholder.png') {
                        that.offerItem.imageUrl[i] = data.Location;
                        break;
                    }
                }
            })
        }
        else {
            alert('Розмр файлу не може перевищувати ' + Math.ceil(maxFileSize / 1000000) + 'МБ');
        }
    }
}