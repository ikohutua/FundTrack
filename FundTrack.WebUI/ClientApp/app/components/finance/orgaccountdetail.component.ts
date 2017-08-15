import { Component, OnInit, Input, OnChanges, SimpleChange, ViewChild, Output, EventEmitter } from "@angular/core";
import { Router } from "@angular/router";
import { OrgAccountService } from "../../services/concrete/finance/orgaccount.service";
import { OrgAccountViewModel } from "../../view-models/concrete/finance/orgaccount-viewmodel";
import { DeleteOrgAccountViewModel } from "../../view-models/concrete/finance/deleteorgaccount-view.model";
import { DecimalPipe } from '@angular/common';
import { CurrencyPipe } from '@angular/common';
import { ModalComponent } from "../../shared/components/modal/modal-component";
import * as key from '../../shared/key.storage';
import { isBrowser } from "angular2-universal";
import { AuthorizeUserModel } from "../../view-models/concrete/authorized-user-info-view.model";

@Component({
    selector: 'orgaccountdetail',
    templateUrl: './orgaccountdetail.component.html',
    styleUrls: ['./orgaccountdetail.component.css']
})
export class OrgAccountDetailComponent implements OnInit, OnChanges {

    @ViewChild(ModalComponent)
    //Modal component that contains password changes controls
    public modal: ModalComponent;
    @Output() onDelete = new EventEmitter<number>();
    @Input('orgId') orgId: number;
    @Input() accountId: number;
    private deletedAccountId: number = 0;
    private user: AuthorizeUserModel = new AuthorizeUserModel();
    private currentDate = new Date().toJSON().slice(0, 10).replace(/-/g, '/');
    private account: OrgAccountViewModel = new OrgAccountViewModel();
    private deleteModel: DeleteOrgAccountViewModel = new DeleteOrgAccountViewModel();
    constructor(private _service: OrgAccountService)
    {

    }
    ngOnInit(): void {
        this._service.getOrganizationAccountById(this.accountId)
            .subscribe(a => {
                this.account = a;
            });
        if (isBrowser) {
            if (localStorage.getItem(key.keyToken)) {
                this.user = JSON.parse(localStorage.getItem(key.keyModel)) as AuthorizeUserModel;
            }
        };
    }
    ngOnChanges(changes: { [propKey: string]: SimpleChange }) {
        if (changes['accountId'] && changes['accountId'] != changes['accountId'].currentValue) {
            this._service.getOrganizationAccountById(this.accountId)
                .subscribe(a => {
                    this.account = a;
                });
        }
    }
    private preDeleteAccount(): void {
        this.modal.show();
    }
    private cancelAccountDeleting(): void {
        this.deleteModel.administratorPassword = '';
        this.modal.hide();
    }
    private deleteAccount(): void {
        this.deleteModel.error = '';
        this.deleteModel.orgAccountId = this.account.id;
        this.deletedAccountId = this.account.id;
        this.deleteModel.userId = this.user.id;
        this.deleteModel.organizationId = this.user.orgId;
        this._service.deleteOrganizationAccountById(this.deleteModel)
            .subscribe(a => {
                this.deleteModel = a;
                if (a.error == '' || a.error == null) {
                    this.showToast();
                    setTimeout(() => {
                        this.modal.hide();
                        this.onDelete.emit(this.deletedAccountId);
                    }, 2000);
                }
            })
    }
    public showToast() {
    var x = document.getElementById("snackbar")
    x.className = "show";
    setTimeout(function () { x.className = x.className.replace("show", ""); }, 3000);
}
}