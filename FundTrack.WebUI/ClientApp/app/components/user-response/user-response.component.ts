import { Component, OnInit, Input } from '@angular/core';
import { UserResponseService } from '../../services/concrete/organization-management/user-responses.service';
import { ShowRequestedItemService } from '../../services/concrete/showrequesteditem.service';
import { ActivatedRoute, Router } from "@angular/router";
import { UserResponseOnRequestsViewModel } from '../../view-models/concrete/user-response-on-requests-view.model'
import { GoodsStatusViewModel } from "../../view-models/concrete/goods-status-model";
import { ModalComponent } from '../../shared/components/modal/modal-component';
import { ViewChild } from '@angular/core';
import { StorageService } from "../../shared/item-storage-service";

@Component({
    template: require('./user-response.component.html'),
    styles: [require('./user-response.component.css')],
    providers: [UserResponseService, ShowRequestedItemService]
})

export class UserResponseComponent implements OnInit {
    private _userResponses: UserResponseOnRequestsViewModel[] = new Array<UserResponseOnRequestsViewModel>();
    private _statuses: GoodsStatusViewModel[] = new Array<GoodsStatusViewModel>();

    private oldStatus: string;
    private newStatus: GoodsStatusViewModel;
    private responseId: number;

    @ViewChild(ModalComponent)
    public modalWindow: ModalComponent

    public constructor(private _userResponseService: UserResponseService,
        private _serviceForStatus: ShowRequestedItemService,
        private _storage: StorageService,
        private _router: Router,
        private _route: ActivatedRoute) { }

    ngOnInit() {
        let organizationId;
        this._storage.newUserResponseCount = 2;
        this._route.params.subscribe(params => {
            organizationId = +params["idOrganization"];
        });
        this._userResponseService.getUserResponsesByOrganization(organizationId).subscribe(
            response => {
                this._userResponses = response;
            });
        this._serviceForStatus.getStatuses().subscribe(statuses => {
            this._statuses = statuses;
        });
    }

    public onSelectStatus(oldStatusName: string, newStatus: GoodsStatusViewModel, responseId: number): void {
        console.log("id");
        console.log(newStatus.id);
        this.oldStatus = oldStatusName;
        this.newStatus = newStatus;
        this.responseId = responseId;
        this.modalWindow.show();
    }

    /**
    * Closes modal window
    */
    public closeModal(): void {
        this.modalWindow.hide();
    }

    public changeStatus() {
        this._userResponseService.changeUserResponseStatus(this.newStatus.id, this.responseId).subscribe(
            response => {
                let index = this._userResponses.findIndex(element => element.id == this.responseId);
                this._userResponses[index] = response;
                this.closeModal();
            });
    }

}