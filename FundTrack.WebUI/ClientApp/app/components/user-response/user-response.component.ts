import { Component, OnInit, Input } from '@angular/core';
import { UserResponseService } from '../../services/concrete/organization-management/user-responses.service';
import { ShowRequestedItemService } from '../../services/concrete/showrequesteditem.service';
import { ActivatedRoute, Router } from "@angular/router";
import { UserResponseOnRequestsViewModel } from '../../view-models/concrete/user-response-on-requests-view.model'
import { GoodsStatusViewModel } from "../../view-models/concrete/goods-status-model";
import { ModalComponent } from '../../shared/components/modal/modal-component';
import { ViewChild } from '@angular/core';
import { StorageService } from "../../shared/item-storage-service";
import { RequestedItemInitViewModel } from "../../view-models/abstract/requesteditem-initpaginationdata-view-model";

@Component({
    template: require('./user-response.component.html'),
    styles: [require('./user-response.component.css')],
    providers: [UserResponseService, ShowRequestedItemService]
})

export class UserResponseComponent implements OnInit {
    private _userResponses: UserResponseOnRequestsViewModel[] = new Array<UserResponseOnRequestsViewModel>();
    private _statuses: GoodsStatusViewModel[] = new Array<GoodsStatusViewModel>();
    private isDeleteResponse: boolean = false;

    public totalItems;
    public itemsPerPage: number = 6;
    public offset: number = 0;
    public currentPage: number = 1;

    private oldStatus: string;
    private newStatus: GoodsStatusViewModel;
    private responseId: number;
    private newStatusName: string;
    private organizationId: number;

    @ViewChild(ModalComponent)
    public modalWindow: ModalComponent

    public constructor(private _userResponseService: UserResponseService,
        private _serviceForStatus: ShowRequestedItemService,
        private _router: Router,
        private _storage: StorageService,
        private _route: ActivatedRoute) { }

    ngOnInit() {
        this._route.params.subscribe(params => {
            this.organizationId = +params["idOrganization"];
        });

        this._userResponseService.getRequestedItemInitData(this.organizationId).subscribe((data: number) => {
            this.totalItems = data;
            this.getUserResponsePerPage(this.currentPage);
        });

        this._serviceForStatus.getStatuses().subscribe(statuses => {
            this._statuses = statuses;
        });
    }

    /**
     * action which happened when select status in dropdown
     * @param oldStatusName
     * @param newStatus
     * @param responseId
     */
    public onSelectStatus(oldStatusName: string, newStatus: GoodsStatusViewModel, responseId: number): void {
        this.oldStatus = oldStatusName;
        this.newStatus = newStatus;
        this.newStatusName = newStatus.name;
        this.responseId = responseId;
        this.isDeleteResponse = false;
        if (this.newStatus.name == 'Виконаний' || this.newStatus.name == 'Неактивний') {
            this.isDeleteResponse = true;
        }
        this.modalWindow.show();
    }

    /**
    * Closes modal window
    */
    public closeModal(): void {
        this.modalWindow.hide();
    }

    /**
     * change status with old status to new status
     */
    public changeStatus() {
        let index = this._userResponses.findIndex(element => element.id == this.responseId);
        if (!this.isDeleteResponse)
            this._userResponseService.changeUserResponseStatus(this.newStatus.id, this.responseId).subscribe(
                response => {
                    this._userResponses[index] = response;
                    this._subscribeOnChangeStatus();
                });
        else {
            this._userResponseService.deleteUserResponse(this.responseId).subscribe(() => {
                this._userResponses.splice(index, 1);
                this._subscribeOnChangeStatus();
            });
        }

    }

    /**
     * action which happened when navigate pagination page
     * @param page
     */
    public onPageChange(page): void {
        this._userResponseService.getUserResponseOnPage(this.organizationId, this.itemsPerPage, page).subscribe(userResponses => {
            this._userResponses = userResponses;
            this.offset = (page - 1) * this.itemsPerPage;
        });
    }

    /**
    * Trigers when user changes items to display on page
    * @param amount
    */
    private itemsPerPageChange(amount: number): void {
        this.itemsPerPage = amount;
        this.getUserResponsePerPage(1);
    }

    private getUserResponsePerPage(page: number) {
        this._userResponseService.getUserResponseOnPage(this.organizationId, this.itemsPerPage, page).subscribe(userResponses => {
            this.offset = 0;
            this._userResponses = userResponses;
        });
    }

    /**
     * event which update count new user response
     */
    private _subscribeOnChangeStatus() {
        this._userResponseService.getUserResponseWithNewStatus(this.organizationId)
            .subscribe(count => this._storage.emitNavChangeEvent(count));
        this.closeModal();
    }

}