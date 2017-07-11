import { Component, OnInit, Input } from '@angular/core';
import { Subscription } from "rxjs/Subscription";
import { ActivatedRoute, Router } from "@angular/router";
import { RequestManagementViewModel } from '../../view-models/abstract/organization-management-view-models/request-management-view-model';
import { OrganizationManagementRequestService } from "../../services/concrete/organization-management/organization-management-request.service";
import { GoodsTypeViewModel } from "../../view-models/concrete/goodsType-view.model";
import { GoodsCategoryViewModel } from "../../view-models/concrete/goodsCategory-view.model";

@Component({
    selector: 'create-request',
    template: require('./organization-create-request.component.html'),
    styles: [require('./organization-create-request.component.css')],
    providers: [OrganizationManagementRequestService]
})

export class OrganizationCreateRequestComponent implements OnInit {
    private _requestedItem: RequestManagementViewModel = new RequestManagementViewModel();
    private _errorMessage: string;
    private _goodsTypes: GoodsTypeViewModel[];
    private _selecteType: GoodsTypeViewModel;
    private _subscription: Subscription;
    private _requestedItemId: number;
    private _currentOrgId: number;

    ngOnInit(): void {
        this.fillGoodtypes();
        this._route.parent.params.subscribe(params => {
            this._currentOrgId = +params["id"];
        });
        this._subscription = this._route
            .params.subscribe(params => {
                this._requestedItemId = +params["id"];
            });
        if (this._requestedItemId > 0) {
            this.getByIdRequestedItem(13);
        }       
    }

    public showData() {
        console.log(this._requestedItem);
    }

    /**
     * Initialize new instance of OrganizationCreateRequestComponent
     * @param _service
     */
    constructor(private _service: OrganizationManagementRequestService,
                private _route: ActivatedRoute,
                private _router: Router) { }

    /**
     * Fills goods type dropdown
     */
    private fillGoodtypes() {
        this._service.getAllGoodsTypes()
            .subscribe(r => {
                this._goodsTypes = r;
            },
            error => {
                this._errorMessage = <any>error;
            })
    }

    /**
     * Add new requested item
     */
    private addRequestedItem() {
        console.log(this._requestedItem);
        this._service.addRequestedItem(this._requestedItem)
            .subscribe(error => this._errorMessage = <any>error);
    }

    /**
     * Edit requested item
     * @param itemId
     */
    private getByIdRequestedItem(itemId: number) {
        this._service.getRequestedItemById(itemId)
            .subscribe(c => this._requestedItem = c,
            error => this._errorMessage = <any>error);
    }

    /**
     * Edit requested item
     * @param item
     */
    private editRequestetItem(item: RequestManagementViewModel) {
        this._service.editRequestedItem(item)
            .subscribe(r => this._requestedItem = r,
            error => this._errorMessage = <any>error);
    }

    /**
     * Manage requested items wich method will be called
     */
    private manageRequestedItems() {
        console.log(this._requestedItem)
        if (this._requestedItemId > 0) {
            this.editRequestetItem(this._requestedItem);
            //this._router.navigate(['/all-requests'])
        }
        else {
            this.addRequestedItem();
        }
    }
}
