import { Component, OnInit, Input, OnChanges, SimpleChange, ViewChild, Output, EventEmitter } from "@angular/core";
import { OrgAccountService } from "../../services/concrete/finance/orgaccount.service";
import { OrgAccountViewModel } from "../../view-models/concrete/finance/orgaccount-viewmodel";
import { DeleteOrgAccountViewModel } from "../../view-models/concrete/finance/deleteorgaccount-view.model";
import { ModalComponent } from "../../shared/components/modal/modal-component";
import * as key from '../../shared/key.storage';
import { isBrowser } from "angular2-universal";
import { AuthorizeUserModel } from "../../view-models/concrete/authorized-user-info-view.model";
import Targetviewmodel = require("../../view-models/concrete/finance/donate/target.view-model");
import TargetViewModel = Targetviewmodel.TargetViewModel;

@Component({
    selector: 'orgaccountdetail',
    templateUrl: './orgaccountdetail.component.html',
    styleUrls: ['./orgaccountdetail.component.css']
})
export class OrgAccountDetailComponent implements OnInit, OnChanges {

    //modal component with delete account controls
    @ViewChild(ModalComponent)
    public modal: ModalComponent;
    //Event emitter to send notifications to parent component
    @Output() onDelete = new EventEmitter<number>();
    //Input property getting organizationId
    @Input('orgId') orgId: number;
    //Input property getting accountId
    @Input() accountId: number = 0;
    //Id of the deleted account
    private deletedAccountId: number = 0;
    //Property indicating if current account has card number assigned
    private hasCardNumber: boolean = false; 
    //Property, containing data about logged in user
    private user: AuthorizeUserModel = new AuthorizeUserModel();
    //Property, containing today's date
    private currentDate = new Date().toJSON().slice(0, 10).replace(/-/g, '/');
    //Property, containing current organization account
    private account: OrgAccountViewModel = new OrgAccountViewModel();
    //Property, containing model, used for account deleting
    private deleteModel: DeleteOrgAccountViewModel = new DeleteOrgAccountViewModel();
    // Property that keeps all targets of current organization
    private targets: TargetViewModel[] = new Array<TargetViewModel>();
    //Property indicating if current account has card number description
    private hasDescription: boolean = false; 

    constructor(private _service: OrgAccountService)
    {   
    }
   
    ngOnInit(): void {
        if (isBrowser) {
            if (localStorage.getItem(key.keyToken)) {
                this.user = JSON.parse(localStorage.getItem(key.keyModel)) as AuthorizeUserModel;
            }
        };
        this._service.getAllBaseTargetsOfOrganization(this.user.orgId).subscribe((result) => {
            this.targets.push({
                targetId: undefined,
                name: "Не вказано",
                organizationId: undefined,
                parentTargetId: undefined,
                isDeletable:true
            });
            for (var target of result) {
                this.targets.push(target);
            }
        });
        
    }
    /*
    Checks for value changes and assignes new account in the component
    */
    ngOnChanges(changes: { [propKey: string]: SimpleChange }) {
        //this.accountId = 97;
        if (changes['accountId'] && changes['accountId'] != changes['accountId'].currentValue) {
            if (this.accountId!=1) {
                this._service.getOrganizationAccountById(this.accountId)
                    .subscribe(a => {
                        this.account = a;
                        this.hasCardNumber = this.checkForCardPresence(this.account);
                        this.hasDescription = this.checkForDescription(this.account);
                        if (this.hasCardNumber) {
                            sessionStorage.setItem(key.keyCardNumber, this.account.cardNumber);
                        }
                        else {
                            sessionStorage.removeItem(key.keyCardNumber);
                        }
                    });
            }
        }
    }

    /*
    Opens modal window
    */
    private preDeleteAccount(): void {
        this.modal.show();
    }

    /*
    Cleans input and closes modal window
    */
    private cancelAccountDeleting(): void {
        this.deleteModel.administratorPassword = '';
        this.modal.hide();
    }

    /*
    Deletes organization account
    */
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

    /*
    Displays successful account deleting toast
    */
    public showToast() {
    var x = document.getElementById("snackbar")
    x.className = "show";
    setTimeout(function () { x.className = x.className.replace("show", ""); }, 3000);
    }

    /*
    Verifies if org account has linked cardnumber
    */
    private checkForCardPresence(account: OrgAccountViewModel): boolean {
        if (account.accountType == 'Готівка' || account.accountType == 'cash') {
            return false;
        }
        if (account.cardNumber === '' || account.cardNumber===null) {
            return false;
        }
        else {
            return true;
        }
    }

    private checkForDescription(account: OrgAccountViewModel): boolean {
        if (account.description === '' || account.description === null) {
            return false;
        }
        else {
            return true;
        }
    }
    /*
    Update target of current account with selected value
    */
    private onChangeTarget($event): void {
        this.account.targetId = $event;
        this._service.updateOrganizationAccount(this.account).subscribe();
    }
}