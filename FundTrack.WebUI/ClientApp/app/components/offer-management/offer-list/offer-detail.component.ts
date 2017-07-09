import { Component, OnInit, Output, EventEmitter } from "@angular/core";
import { IOfferViewModel } from "../../../view-models/abstract/offer-model.interface";
import { OfferViewModel } from "../../../view-models/concrete/offer-view.model";
import { Router } from "@angular/router";

@Component({
    selector: 'offer-detail',
    templateUrl: './offer-detail.component.html',
    styleUrls: ['./offer-detail.component.css']
})
export class OfferDetailComponent implements OnInit{
    @Output() hidePanel = new EventEmitter<boolean>();
    private offerItem = new OfferViewModel();
    constructor(private _router: Router) {

    }
    ngOnInit() {
        this.hidePanel.emit(false);
    }
}