import { Component, OnInit } from "@angular/core";
import { IOfferViewModel } from "../../../view-models/abstract/offer-model.interface";
import { OfferViewModel } from "../../../view-models/concrete/offer-view.model";
import { Router } from "@angular/router";
import { OfferFilteringService } from "../../../services/concrete/offer-management/offer-filtering.service";


@Component({
    selector: 'offer-management',
    templateUrl: './offer-management.component.html',
    styleUrls: ['./offer-management.component.css']
})
export class OfferItemManagementComponent implements OnInit{
    private isPanelHidden: boolean = false;
    private showActive: boolean = true;
    private showInactive: boolean = true;
    private showRemoved: boolean = true;
    constructor(private _router: Router,
        private _filterService:OfferFilteringService)
    {
    }
    hidePanel(hide: boolean): void {
        this.isPanelHidden != this.isPanelHidden;
    }
    ngOnInit() {
        
    }
    /*
    Changes value of variable to received
    */
    private ToggleActive(value: boolean) {
        this._filterService.changeActive(value);
    }
    /*
    Changes value of variable to received
    */
    private ToggleInactive(value: boolean) {
        this._filterService.changeInactive(value);
    }
}