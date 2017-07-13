import { Component, OnInit, Injectable } from '@angular/core';
import { Subscription } from "rxjs/Subscription";
import { ActivatedRoute, Router } from "@angular/router";
import { RequestManagementViewModel } from '../../view-models/abstract/organization-management-view-models/request-management-view-model';
import { OrganizationManagementRequestService } from "../../services/concrete/organization-management/organization-management-request.service";

@Component({
    selector: 'org-management-request',
    template: require('./organization-management-request.component.html'),
    styles: [require('./organization-management-request.component.css')],
    providers: [OrganizationManagementRequestService]
})

@Injectable()
export class OrganizationManagementRequestComponent implements OnInit {
    public currentRequestedItem: RequestManagementViewModel;
    private _allRequestedItems: RequestManagementViewModel[];
    private _currentRequestedItemId: number;
    private _errorMessage: string;
    private _organizationId: number;
    private _subscription: Subscription;

    ngOnInit(): void {
        this._subscription = this._route.params.subscribe(params => {
                this._organizationId = +params["id"];
            });
        this.getAllRequestedItems(1);
    }

    constructor(private _service: OrganizationManagementRequestService,
                private _route: ActivatedRoute,
                private _router: Router)
    { }

    private getAllRequestedItems(id: number) {
        this._service.getAllRequestedItemsByOrganization(id)
            .subscribe(r => {
                this._allRequestedItems = r;
            },
            error => {
                this._errorMessage = <any>error;
            })
    }

    private setCurrentRequestedItem(requestedItem: RequestManagementViewModel) {
        this.currentRequestedItem = requestedItem;
    }

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
        this._router.navigate(['organization/request/manage/' + this._organizationId + "/" + idRequest]);
    }
 
    ngDestroy(): void {
        this._subscription.unsubscribe();
    }

}