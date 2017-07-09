import { Component, OnInit } from "@angular/core";
import { IOfferViewModel } from "../../view-models/abstract/offer-model.interface";
import { OfferViewModel } from "../../view-models/concrete/offer-view.model";
import { Router } from "@angular/router";


@Component({
    selector: 'offer-management',
    templateUrl: './offer-management.component.html',
    styleUrls: ['./offer-management.component.css']
})
export class OfferItemManagementComponent{
    private isPanelHidden: boolean = false;
    private showActive: boolean = true;
    private showInactive: boolean = true;
    private showRemoved: boolean = true;
    constructor(private _router: Router) {

    }
    hidePanel(hide: boolean): void {
        this.isPanelHidden != this.isPanelHidden;
    }
}