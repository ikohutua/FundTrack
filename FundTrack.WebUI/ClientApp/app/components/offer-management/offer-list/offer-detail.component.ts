import { Component, OnInit, Output, EventEmitter } from "@angular/core";
import { IOfferViewModel } from "../../../view-models/abstract/offer-model.interface";
import { OfferViewModel } from "../../../view-models/concrete/offer-view.model";
import { Router } from "@angular/router";
import { GoodsService } from "../../../services/concrete/goods/goods.service";
import { GoodsCategoryViewModel } from "../../../view-models/concrete/goods-category-view.model";
import { GoodsTypeViewModel } from "../../../view-models/concrete/goods-type-view.model";

@Component({
    selector: 'offer-detail',
    templateUrl: './offer-detail.component.html',
    styleUrls: ['./offer-detail.component.css'],
    providers:[GoodsService]
})
export class OfferDetailComponent implements OnInit{
    @Output() hidePanel = new EventEmitter<boolean>();
    private selectedItem = new GoodsTypeViewModel();
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
    }
}