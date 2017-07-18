import { Component, OnInit, OnDestroy, Input } from "@angular/core";
import { RequestDetailService } from '../../services/concrete/request-detail.service';
import { RequestedItemDetailViewModel } from '../../view-models/concrete/requested-item-detail-view.model';
import { UserResponseViewModel } from '../../view-models/concrete/user-response-view.model';
import { ModalComponent } from '../../shared/components/modal/modal-component';
import { AuthorizeUserModel } from "../../view-models/concrete/authorized-user-info-view.model";
import { ActivatedRoute } from "@angular/router";
import { Subscription } from "rxjs/Subscription";
import { ViewChild } from '@angular/core';
import * as keys from '../../shared/key.storage';

@Component({
    template: require('./request-detail.component.html'),
    styles: [require('./request-detail.component.css')],
    providers: [RequestDetailService]
})

export class RequestDetailComponent implements OnInit, OnDestroy {

    private _errorMessage: string;
    private _subscription: Subscription;
    private userResponse: UserResponseViewModel = new UserResponseViewModel();
    @Input() phoneNumber:string;
    @Input() responseDescription: string;
    @ViewChild(ModalComponent)

    public modalWindow: ModalComponent

    /**
    * Field which contain information about requested detail
    */
    public requestDetail: RequestedItemDetailViewModel;

    /**
    * Castome Field Touched Indicator
     */
    public customeFieldTouched = false;

    public constructor(private _requestDetailService: RequestDetailService,
        private _activatedRoute: ActivatedRoute) { }

    private getRequestedDetail(id: number) {
        this._requestDetailService.getRequestDetail(id).subscribe(
            request => this.requestDetail = request,
            error => this._errorMessage = error
        )
    }

    ngOnInit() {
        this._subscription = this._activatedRoute.params.subscribe(params => {
            let id = +params['id'];
            this.getRequestedDetail(id);
        });
    }

    /**
    * Closes modal window
    */
    public closeModal(): void {
        this.modalWindow.hide();
        this.customeFieldTouched = false;
    }

    /**
     * Open modal window
     */
    public onActionClick(): void {
        this.modalWindow.show();
    }

    /**
     * initialize user response and send request on server
     */
    public setUserResponse(): void {
        if (localStorage.getItem(keys.keyToken)) {
            let user = JSON.parse(localStorage.getItem(keys.keyModel)) as AuthorizeUserModel;
            this.userResponse.userId = user.id;
        }
        this.userResponse.description = this.phoneNumber+ '_Enter_' +this.responseDescription;
        this.userResponse.requestedItemId = this.requestDetail.id;
        this._requestDetailService.setUserResponse(this.userResponse).subscribe(
            userResponse => {
                this.userResponse = userResponse;
                this.responseDescription = "";
                this.phoneNumber="";
                this.closeModal();
            })
    }

    ngOnDestroy() {
        this._subscription.unsubscribe();
    }
}
