import { Component, OnInit, Injectable, ViewChild, AfterViewInit } from '@angular/core';
import { Subscription } from "rxjs/Subscription";
import { ActivatedRoute, Router } from "@angular/router";
import { RequestManagementViewModel } from '../../view-models/abstract/organization-management-view-models/request-management-view-model';
import { OrganizationManagementRequestService } from "../../services/concrete/organization-management/organization-management-request.service";
import { SpinnerComponent } from "../../shared/components/spinner/spinner.component";
import { RequestedImageViewModel } from "../../view-models/abstract/organization-management-view-models/requested-item-view.model";

@Component({
    selector: 'org-management-request',
    template: require('./organization-management-request.component.html'),
    styles: [require('./organization-management-request.component.css')],
    providers: [OrganizationManagementRequestService]
})

@Injectable()
export class OrganizationManagementRequestComponent implements OnInit {
    public currentRequestedItem: RequestManagementViewModel;
    private _allRequestedItems: RequestManagementViewModel[] = [];
    private _currentRequestedItemId: number;
    private _errorMessage: string;
    private _organizationId: number;
    private _subscription: Subscription;
    private _totalItems: number;
    private _offset: number = 0;
    private _itemPerPage: number = 4;
    private _currentPage: number = 1;

    @ViewChild(SpinnerComponent) spinner: SpinnerComponent;

    ngOnInit(): void {
        this._subscription = this._route.params.subscribe(params => {
            this._organizationId = +params["id"];
            this.getRequestedItemsInitData(this._organizationId );
        });      
    }

    /**
     * Initialize new instance of OrganizationManagementRequestComponent
     * @param _service
     */
    constructor(private _service: OrganizationManagementRequestService,
                private _route: ActivatedRoute,
                private _router: Router)
    { }

    /**
     * Gets all requested items
     * @param id
     */
    private getAllRequestedItems(id: number) {
        this._service.getAllRequestedItemsByOrganization(id, this.spinner)
            .subscribe(r => {
                debugger;
                this._allRequestedItems = r;   
                this.setMainImage(this._allRequestedItems);
            },
            error => {
                this._errorMessage = <any>error;
            })
    }

    private setMainImage(offers: RequestManagementViewModel[]): void {
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
    /**
     * Sets current requested item
     * @param requestedItem
     */
    private setCurrentRequestedItem(requestedItem: RequestManagementViewModel) {
        this.currentRequestedItem = requestedItem;
    }

    /**
     * Delete requested item
     */
    private deleteRequestedItem() {
        this._service.deleteRequestedItem(this.currentRequestedItem.id)
            .subscribe(data => this._allRequestedItems
                .splice(this._allRequestedItems.findIndex(i => i.id == this.currentRequestedItem.id), 1),
            error => this._errorMessage = <any>error);
    }

    /**
     * Redirect to 'manage requests page' in organization management page
     * @param idOrganization
     */
    public redirectToManageRequestPage(idRequest: number): void {
        this._router.navigate(['organization/request/manage/' + this._organizationId.toString() + '/' + idRequest.toString()]);
    }

    /**
     * Gets requested items per page
     * @param page
     */
    public onPageChange(page: any): void {
        this._service.getRequestedItemsPerPage(this._organizationId, page, this._itemPerPage, this.spinner)
            .subscribe(requests => {
                this._allRequestedItems = requests,
                    this._offset = (page - 1) * this._itemPerPage;
            },
            error => {
                this._errorMessage = <any>error;
            });
    }

    /**
     * Gets requested items initial data
     * @param organizationId
     */
    private getRequestedItemsInitData(organizationId): void {
        this._service.getRequestedItemsInitData(organizationId)
            .subscribe(r => {
                this._totalItems = r.totalRequestedItemsCount;
                this._itemPerPage = r.requestedItemsPerPage;
                this.getRequestedItemsPerPage(organizationId, this._currentPage, this._itemPerPage);                
            })
    }

    /**
     * Gets requested item per page
     * @param organizationId
     * @param currentPage
     * @param pageSize
     */
    private getRequestedItemsPerPage(organizationId: number, currentPage: number, pageSize: number) {
        this._service.getRequestedItemsPerPage(organizationId, currentPage, pageSize, this.spinner)
            .subscribe(requests => {
                this._allRequestedItems = requests;
                this.setMainImage(this._allRequestedItems);
            });
    }

    /**
    * Trigers when user changes items to display on page
    * @param amount
    */
    private itemsPerPageChange(amount: number): void {
        this._service.getRequestedItemsPerPage(this._organizationId, 1, amount, this.spinner).subscribe(
            requests => {
                this._offset = 0;
                this._allRequestedItems = requests;
                this._itemPerPage = amount;
            });
    }

    ngDestroy(): void {
        this._subscription.unsubscribe();
    }

}