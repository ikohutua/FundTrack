import { ViewChild, Component, OnInit, Input, SimpleChange, OnChanges, Output, EventEmitter } from "@angular/core";
import { Router } from "@angular/router";
import { OrgAccountService } from "../../services/concrete/finance/orgaccount.service";
import { FixingBalanceService } from "../../services/concrete/fixing-balance.service";
import { OrgAccountViewModel } from "../../view-models/concrete/finance/orgaccount-viewmodel";
import { DecimalPipe } from '@angular/common';
import { CurrencyPipe } from '@angular/common';
import * as key from '../../shared/key.storage';
import * as constant from '../../shared/default-configuration.storage';
import * as message from '../../shared/common-message.storage';
import { FinOpService } from "../../services/concrete/finance/finOp.service";
import { FinOpListViewModel } from "../../view-models/concrete/finance/finop-list-viewmodel";
import { isBrowser } from "angular2-universal";
import { ModalComponent } from "../../shared/components/modal/modal-component";
import { MoneyOperationViewModel } from "../../view-models/concrete/finance/money-operation-view-model";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { ValidatorsService } from "../../services/concrete/validators/validator.service";
import Core = require("@angular/core");
import Targetviewmodel = require("../../view-models/concrete/finance/donate/target.view-model");
import { Image } from "../../view-models/concrete/image.model";
import { EditOrganizationService } from "../../services/concrete/organization-management/edit-organization.service";
import { ModeratorViewModel } from '../../view-models/concrete/edit-organization/moderator-view.model';
import { AuthorizeUserModel } from "../../view-models/concrete/authorized-user-info-view.model";
import { DonateService } from "../../services/concrete/finance/donate-money.service";
import { CurrencyViewModel } from "../../view-models/concrete/finance/donate/currency.view-model";
import { TargetViewModel } from "../../view-models/concrete/finance/donate/target.view-model";
import { DeleteOrgAccountViewModel } from "../../view-models/concrete/finance/deleteorgaccount-view.model";
import { OrgAccountDetailComponent } from "../../components/finance/orgaccountdetail.component";
import { DonateViewModel} from"../../view-models/concrete/finance/donate/donate.view-model";
import { UserInfo}  from "../../view-models/concrete/user-info.model";
import { UserService } from "../../services/concrete/user.service";


@Component({
    selector: 'orgaccountoperation',
    templateUrl: './orgaccountoperation.component.html',
    styleUrls: ['./orgaccountoperation.component.css'],
    providers: [DonateService, OrgAccountService, EditOrganizationService, UserService, FixingBalanceService]
})
export class OrgAccountOperationComponent implements OnChanges {

    //private accounts: OrgAccountViewModel[] = new Array<OrgAccountViewModel>();
    private accountsTo: OrgAccountViewModel[] = new Array<OrgAccountViewModel>();
    private orgTargets: TargetViewModel[] = new Array<TargetViewModel>();
    private baseTargets: TargetViewModel[] = new Array<TargetViewModel>();
    private subTargets: TargetViewModel[] = new Array<TargetViewModel>();
    private currentAccount: OrgAccountViewModel = new OrgAccountViewModel();
    private accountForUpdate: OrgAccountViewModel = new OrgAccountViewModel();
    private finOps: FinOpListViewModel[] = new Array<FinOpListViewModel>();
    private reservedFinOpsArray: FinOpListViewModel[] = new Array<FinOpListViewModel>();
    private currentFinOp: FinOpListViewModel = new FinOpListViewModel();
    private moderators: ModeratorViewModel[] = new Array<ModeratorViewModel>();
    private accountOwner: ModeratorViewModel = new ModeratorViewModel();
    private currentDate = new Date().toJSON().slice(0, 10);
    private currentTarget: TargetViewModel = new TargetViewModel();
    private finOpTarget: TargetViewModel = new TargetViewModel();
    private user: AuthorizeUserModel = new AuthorizeUserModel();
    private suggestedDonations: DonateViewModel[] = new Array<DonateViewModel>();
    private users: UserInfo[] = new Array<UserInfo>();
    private selectedDonationId: number = undefined;
    private selectedFinOp: FinOpListViewModel = new FinOpListViewModel();
    private selectedUserId: number = undefined;
    images: Image[] = [];
    public deleteModel: DeleteOrgAccountViewModel = new DeleteOrgAccountViewModel();
    public deletedAccountId: number = 0;
    private minDate: string;
    private isCashType: boolean = false;
    private isTransferOperation: boolean = false;
    private isBaseTargetChosen: boolean = false;
    private isWindthraw: boolean = false;
    private isDeposite: boolean = false;
    private originalAmount: number;
    private toasterMessage: string;
    //--------------------Pagination-------------------------
    private totalItems: number[] = new Array<number>();
    private totalItemsForFinOpType: number;
    private offset: number = 0;
    private itemPerPage: number = 10;
    private currentPage: number = 1;
    private currentFinOpType: number = -1;

    @Input() orgId: number;
    @Input() accountId: number;
    @Output() getIsExtractEnable = new EventEmitter<boolean>();
    @Output() onDelete = new EventEmitter<number>();
    @Output() accounts: OrgAccountViewModel[] = new Array<OrgAccountViewModel>();
    //------------------------------------------------------------------------------
    //Initialize modal windows

    @ViewChild("newMoneyTransfer")
    private newMoneyTransferWindow: ModalComponent;

    @ViewChild("newAccountManagment")
    private newAccountManagmentWindow: ModalComponent;

    @ViewChild("newDeleteAccount")
    private newDeleteModalWindow: ModalComponent;

    @ViewChild("newUpdateFinOperation")
    private newUpdateFinOperationWindow: ModalComponent;

    @ViewChild("newBankAccountManagment")
    private newBankAccountManagmentWindow: ModalComponent;

    @ViewChild("fixingBalanceModal")
    private fixingBalanceModal: ModalComponent;

    @ViewChild("suggestedDonationsModal")
    private suggestedDonationsModal : ModalComponent;
    //-------------------------------------------------------------------------------
    //Initialize model and form
    private moneyTransferForm: FormGroup;
    private accountManagmentForm: FormGroup;
    private updateFinOperationForm: FormGroup;
    private bankAccountManagmentForm: FormGroup;


    private moneyOperationModel: FinOpListViewModel = new FinOpListViewModel();
    private moneyTransfer: FinOpListViewModel = new FinOpListViewModel();
    private manageAccount: OrgAccountViewModel = new OrgAccountViewModel();
    private updateFinOperation: FinOpListViewModel = new FinOpListViewModel();
    //-------------------------------------------------------------------------------
    public constructor(private _router: Router,
        private finOpService: FinOpService,
        private fb: FormBuilder,
        private validatorsService: ValidatorsService,
        private accountService: OrgAccountService,
        private donateService: DonateService,
        private userService: UserService,
        private editService: EditOrganizationService,
        private fixingService: FixingBalanceService) {
            this.createManagmantForm();
            this.createFinOperationFormForUpdate();
    }
    private navigateToImportsPage(): void {
        this._router.navigate(['/finance/bank-import']);
    }

    private navigateToIncomeOperationPage() {
        this._router.navigate(['/finance/income/' + this.currentAccount.id]);
    }

    private navigateToSpendingOperationPage() {
        this._router.navigate(['/finance/spending/' + this.currentAccount.id]);
    }

    private navigateToTransferOperationPage() {
        this._router.navigate(['/finance/transfer/' + this.currentAccount.id]);
    }

    /*
    Checks for value changes and assignes new account in the component
    */
    ngOnChanges(changes: { [propKey: string]: SimpleChange }) {
        this.getCurrentOrgAccount();
        this.getFinOpInitData();
        this.getOrgTargetsAndBaseTargets();
    }

    ngOnInit(): void {
        this.getAllCashAccounts();
        this.getModerators();
        this.getLoggedUser();
    }

    ngAfterContentInit(): void {
        this.setOwner();
    }
//--------------------------------------Pagination---------------------------------------------------------------------------
    private onPageChange(page: number): void {
        debugger;
        this.finOpService.getFinOpByOrgAccountIdForPage(this.accountId, page, this.itemPerPage, this.currentFinOpType).subscribe(
            finOps => {
                this.finOps = finOps;
                this.setFinOperations();
                this.offset = (page - 1) * this.itemPerPage;
            }
        );
    }

    private itemsPerPageChange(amount: number): void {
        debugger;
        this.finOpService.getFinOpByOrgAccountIdForPage(this.accountId, 1, amount, this.currentFinOpType).subscribe(
            finOps => {
                this.offset = 0;
                this.finOps = finOps;
                this.setFinOperations();
                this.itemPerPage = amount;
            });
    }

    private onFinOpTypeChange(finOpType: number): void {
        this.finOpService.getFinOpByOrgAccountIdForPage(this.accountId, 1, this.itemPerPage, finOpType).subscribe(
            finOps => {
                this.offset = 0;
                this.currentFinOpType = finOpType;
                this.totalItemsForFinOpType = this.totalItems[+finOpType + 1];
                this.finOps = finOps;
                this.setFinOperations();
            });
    }

    private getFinOpsPerPageByOrganizationId(currentPage: number, pageSize: number): void {
        this.finOpService.getFinOpByOrgAccountIdForPage(this.accountId, currentPage, pageSize)
            .subscribe(finOps => {
                this.finOps = finOps;
                this.setFinOperations();
            });
    }

    private getFinOpInitData(): void {
        this.finOpService.getFinOpInitData(this.accountId).subscribe(response => {
            this.totalItems = response;
            this.totalItemsForFinOpType = this.totalItems[this.currentFinOpType + 1];
            this.getFinOpsPerPageByOrganizationId(this.currentPage, this.itemPerPage);
        });
    }
//--------------------------------------------------Table----------------------------------------------------------------
    private getFinOpById(id: number) {
        this.finOpService.getFinOpById(id).subscribe(f => {
            this.updateFinOperation = f;
        });
    }

    private setFinOperations() {
        for (var i = 0; i < this.finOps.length; i++) {

            if (this.finOps[i].finOpType == constant.incomeId) {
                this.finOps[i].finOpName = constant.incomeUA;
            }

            else if (this.finOps[i].finOpType == constant.spendingId) {
                this.finOps[i].finOpName = constant.spendingUA;
            }

            else if (this.finOps[i].finOpType == constant.transferId && this.finOps[i].cardFromId == this.currentAccount.id) {
                this.finOps[i].finOpName = constant.incomeTransferUA;
            }
            else {
                this.finOps[i].finOpName = constant.spendingTransferUA;
            }
        }
    }
//-------------------------------------------------Accounts------------------------------------------------------------
    private getCurrentOrgAccount() {
        this.accountService.getOrganizationAccountById(this.accountId)
            .subscribe(currAcc => {
                this.currentAccount = currAcc;
                this.accountForUpdate = currAcc;
                this.getAccountType();
                this.getAccontsForTransfer();
                this.getCurrentTarget();
                this.getMinDate();
            });
    }

    private getAllCashAccounts() {
        this.accountService.getAllAccountsOfOrganization()
            .subscribe(acc => {
                this.accounts = acc.filter(a =>
                    a.accountType === constant.cashUA
                );
                this.getAccontsForTransfer();
            });
    }

    private getAccontsForTransfer() {
        if (this.currentAccount.targetId === null) {
            this.accountsTo = this.accounts.filter(acc => acc.id != this.currentAccount.id);
        }
        else {
            this.accountsTo = this.accounts.filter(acc =>
                acc.targetId
                == this.currentAccount.targetId &&
                acc.id != this.currentAccount.id);
        }
    }

    public getAccountType() {
        if (this.currentAccount.accountType === constant.cashUA) {
            this.isCashType = true;
        }
        else {
            this.isCashType = false;
        }
    }
//------------------------------------------------Targets------------------------------------------------------
    private getOrgTargetsAndBaseTargets() {
        this.editService.getTargetsByOrganizationId(this.orgId)
            .subscribe(t => {
                this.orgTargets = t;
                this.getBaseTargets();
            });
    }

    private getBaseTargets() {
        for (let i = 0; i < this.orgTargets.length; i++) {
            if (this.orgTargets[i].parentTargetId == null) {
                this.baseTargets.push(this.orgTargets[i]);
            }
        }
    }

    private getSubTargetsByTargetId(parentTargetId: number) {
        this.subTargets = new Array<TargetViewModel>();
        for (let i = 0; i < this.orgTargets.length; i++) {
            if (this.orgTargets[i].parentTargetId == parentTargetId) {
                this.subTargets.push(this.orgTargets[i]);
            }
        }
        this.isBaseTargetChosen = true;
    }

    private getCurrentTarget() {
        this.currentTarget = this.baseTargets.find(target => target.targetId == this.currentAccount.targetId);
    }
//-------------------------------------------------------Users------------------------------------------------------------
    private getModerators() {
        this.editService.getModerators(this.orgId)
            .subscribe(moder => {
                this.moderators = moder;
            });
    }

    public getLoggedUser() {
        this.user = JSON.parse(localStorage.getItem(key.keyModel)) as AuthorizeUserModel;
    }

    private setOwner() {
        this.accountOwner = this.moderators.find(m => m.id == this.currentAccount.userId);
    }
//-------------------------------------------------------Date------------------------------------------------------------
    public getMinDate() {
        this.fixingService.getFilterByAccId(this.currentAccount.id)
            .subscribe(fix => {
                if (fix.lastFixing.balanceDate == null) {
                    this.minDate = this.currentAccount.creationDate.toJSON().slice(0, 10);
                }
                else {
                    this.minDate = fix.lastFixing.balanceDate.slice(0, 10);
                }
            });
    }

    public setDate(model: FinOpListViewModel, date: Date) {
        model.date = date;
    }
//-------------------------------------------------------Account deleting------------------------------------------------------------
    public preDeleteAccount(): void {
        this.newAccountManagmentWindow.hide();
        this.newDeleteModalWindow.show();
    }

    public cancelAccountDeleting(): void {
        this.deleteModel.administratorPassword = '';
        this.newDeleteModalWindow.hide();
    }

    public deleteAccount(): void {
        this.deleteModel.error = '';
        this.deleteModel.orgAccountId = this.currentAccount.id;
        this.deletedAccountId = this.currentAccount.id;
        this.deleteModel.userId = this.user.id;
        this.deleteModel.organizationId = this.user.orgId;
        this.accountService.deleteOrganizationAccountById(this.deleteModel)
            .subscribe(a => {
                this.deleteModel = a;
                this.onDelete.emit(this.deletedAccountId);
            });
        this.newDeleteModalWindow.hide();
    }

    private createManagmantForm() {
        this.accountManagmentForm = this.fb.group({
            userId: [
                this.accountForUpdate.userId]
        });
        this.accountManagmentForm.valueChanges
            .subscribe(a => this.onValueChange(this.accountManagmentForm, this.formTransferErrors, a));
        this.onValueChange(this.accountManagmentForm, this.formManagmentErrors);
    }

    private setDefaultBoolValues() {
        this.isCashType = false;
        this.isTransferOperation = false;
        this.isBaseTargetChosen = false;
        this.isWindthraw = false;
        this.isDeposite = false;
    }
//-------------------------------------------------------Forms------------------------------------------------------------
    private createFinOperationFormForUpdate() {
        this.updateFinOperationForm = this.fb.group({
            amount: [
                this.updateFinOperation.amount, [this.validatorsService.isMinValue,
                this.validatorsService.isMaxValue,
                this.validatorsService.isNumber
                ]
            ],
            cardFromId: [
                this.updateFinOperation.cardFromId
            ],
            cardToId: [
                this.updateFinOperation.cardToId
            ],
            targetId: [
                this.updateFinOperation.targetId
            ],
            description: [
                this.updateFinOperation.description, [Validators.maxLength(500)]
            ],
            date: [
                this.updateFinOperation.date
            ]
        });
        this.updateFinOperationForm.valueChanges
            .subscribe(a => this.onValueChange(this.updateFinOperationForm, this.formUpdateErrors, a));
        this.onValueChange(this.updateFinOperationForm, this.formUpdateErrors);
    }

    private formTransferErrors = {
        amount: "",
        description: ""
    };

    private formUpdateErrors = {
        amount: "",
        description: ""
    };

    private formBankUpdateErrors = {
        amount: "",
        description: ""
    };

    private formManagmentErrors = {
    };

    private invalidAmountMessage = message.invalidAmountMessage;
    private maxLengthDescription = message.maxLengthDescription;

    private validationMessages = {
        amount: {
            notnumber: this.invalidAmountMessage,
            notminvalue: this.invalidAmountMessage,
            notmaxvalue: this.invalidAmountMessage
        },
        description: {
            maxlength: this.maxLengthDescription
        }
    }

    private onValueChange(formMoney: FormGroup, formErrors, data?: any) {
        if (!formMoney) return;                                                    //check, if fields are correct
        this.checkError(formMoney, formErrors);
    }

    private checkError(formMoney: FormGroup, formErrors) {
        for (let field in formErrors) {
            formErrors[field] = "";
            let control = formMoney.get(field);

            if (control && control.dirty && !control.valid) {
                let message = this.validationMessages[field];
                for (let key in control.errors) {
                    formErrors[field] = message[key.toLowerCase()];
                }
            }
        }
    }
//-------------------------------------------------------Modals------------------------------------------------------------
    private openModal(modal: ModalComponent) {
        modal.show();
    }

    private openUpdateFinOpModal(finOp: FinOpListViewModel) {
        this.currentFinOp = finOp;
        this.getFinOpById(finOp.id);
        if (finOp.finOpType === constant.transferId) {
            this.isTransferOperation = true;
        }
        this.finOpTarget = this.orgTargets.find(target => target.targetId == finOp.targetId);
        if (this.finOpTarget.parentTargetId != null) {
            this.getSubTargetsByTargetId(this.finOpTarget.parentTargetId);
        }
        else {
            this.getSubTargetsByTargetId(finOp.targetId);
        }
        this.openModal(this.newUpdateFinOperationWindow);
    }

    private closeModal(modal: ModalComponent, form: FormGroup) {
        this.setDefaultBoolValues();
        modal.hide();
        form.reset();
    }

    private updateOrgAccount() {
        this.accountService.updateOrganizationAccount(this.accountForUpdate).subscribe(a => {
            this.manageAccount = a;
        })
    }

    private updateFinOp() {
        this.updateFinOperation.userId = this.currentAccount.userId;
        this.currentFinOp = this.updateFinOperation;
        if (this.isWindthraw || this.isDeposite) {
            this.updateFinOperation.finOpType = constant.transferId;
        }
        this.finOpService.editFinOperation(this.updateFinOperation).subscribe(f => {
            this.updateFinOperation = f;
        });
        this.isTransferOperation = false;
        this.isWindthraw = false;
        this.isDeposite = false;
        this.closeModal(this.newUpdateFinOperationWindow, this.updateFinOperationForm);
    }


    public closeWindow(modal: ModalComponent) {
        this.setDefaultBoolValues();
        modal.hide();
    }

    handleCloseModalEvent(isCloseModal) {
        if (isCloseModal) {
            this.fixingBalanceModal.hide();
        }
    }

    onExtractEnableChange(event: boolean) {
        this.getIsExtractEnable.emit(event);
    }

    private getSuggestedDonations(finOp: FinOpListViewModel) {
        this.donateService.getSuggestedDonations(finOp.id).subscribe(result => {
            this.suggestedDonations = result;
            this.selectedFinOp = finOp;
            this.suggestedDonationsModal.show();
            if (this.suggestedDonations.length == 1) {
                this.selectedDonationId = this.suggestedDonations[0].id;
                this.selectedUserId = this.suggestedDonations[0].userId;
            }
        });
        this.userService.getAllUsers().subscribe(result => this.users = result);
    }

    private IsOkButtonEnable(): boolean {
        if (this.selectedUserId == undefined) {
            return false;
        }
        if (this.suggestedDonations.length > 1) {
            if (this.selectedDonationId == undefined) {
                return false;
            }
        }
        return true;
    }

    private radioButtonOnChange(donation: DonateViewModel) {
        this.selectedDonationId = donation.id;
        this.selectedUserId = donation.userId;
    }

    private closeSuggestionsModal() {
        this.selectedDonationId = undefined;
        this.selectedUserId = undefined;
        this.suggestedDonations.length = 0;
        this.suggestedDonationsModal.hide();
    }

    private onClickSuggestionModalButton() {
        if (this.suggestedDonations.length == 0) {
            this.createDonationFromFinOp(this.selectedFinOp.id);
            return;
        }
        this.selectedFinOp.userId = this.selectedUserId;
        this.selectedFinOp.donationId = this.selectedDonationId;
        this.finOpService.bindDonationAndFinOp(this.selectedFinOp).subscribe(result => {
            this.toasterMessage = constant.successMessageUA;
            this.showToast();
            this.closeSuggestionsModal();
        });
    }

    private createDonationFromFinOp(finOpId: number): DonateViewModel {
        this.selectedFinOp.userId = this.selectedUserId;
        var donation: DonateViewModel = new DonateViewModel();
        donation.amount = this.selectedFinOp.amount;
        donation.userId = this.selectedUserId;
        donation.currencyId = this.currentAccount.currencyId;
        donation.targetId = this.selectedFinOp.targetId;
        donation.description = this.selectedFinOp.description;
        donation.donationDate = this.selectedFinOp.date.toString();
        this.donateService.getOrderId().subscribe(result => {
            donation.orderId = result;
            donation.bankAccountId = this.selectedFinOp.cardToId;
            this.donateService.addDonation(donation).subscribe(result => {
                this.selectedFinOp.donationId = result.id;
                this.finOpService.bindDonationAndFinOp(this.selectedFinOp).subscribe(result => {
                    this.toasterMessage = constant.successMessageUA;
                    this.showToast();
                    this.closeSuggestionsModal();
                });
            });
        });
        return donation;
    }

    public showToast() {
        var x = document.getElementById("suggestedToast");
        x.className = "show";
        setTimeout(function () {
            x.className = x.className.replace("show", "");
        }, 3000);
    }

    private onChangeUser($event) {
        this.selectedUserId = $event;
    }

    private isUserSelectDisabled(): boolean {
        if (this.suggestedDonations.length == 0) {
            return false;
        }
        if (this.selectedDonationId != undefined) {
            var d = this.suggestedDonations.find(d => d.id === this.selectedDonationId);
            if (d.userId != undefined) {
                return true; // if selected donation already has user
            }
            return false;
        }
        return true;
    }
}