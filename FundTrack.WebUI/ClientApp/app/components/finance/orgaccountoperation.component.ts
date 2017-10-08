import { ViewChild, Component, OnInit, Input, SimpleChange, OnChanges, Output, EventEmitter } from "@angular/core";
import { Router } from "@angular/router";
import { OrgAccountService } from "../../services/concrete/finance/orgaccount.service";
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
    providers: [DonateService, OrgAccountService, EditOrganizationService, UserService]
})
export class OrgAccountOperationComponent implements OnChanges {

    private accounts: OrgAccountViewModel[] = new Array<OrgAccountViewModel>();
    private accountsTo: OrgAccountViewModel[] = new Array<OrgAccountViewModel>();
    private currencies: CurrencyViewModel[] = new Array<CurrencyViewModel>();
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
    private user: AuthorizeUserModel = new AuthorizeUserModel();
    private suggestedDonations: DonateViewModel[] = new Array<DonateViewModel>();
    private users: UserInfo[] = new Array<UserInfo>();
    private selectedDonationId: number = undefined;
    private selectedFinOp: FinOpListViewModel = new FinOpListViewModel();
    private selectedUserId: number = undefined;
    public deleteModel: DeleteOrgAccountViewModel = new DeleteOrgAccountViewModel();
    public deletedAccountId: number = 0;
    private minDate: string;
    private default: boolean = true;
    private isCashType: boolean = false;
    private isTransferOperation: boolean = false;
    private isBaseTargetChosen: boolean = false;
    private originalAmount: number;
    private toasterMessage: string;

    @Input() orgId: number;
    @Input() accountId: number;
    @Output() getIsExtractEnable = new EventEmitter<boolean>();
    @Output() onDelete = new EventEmitter<number>();
    //------------------------------------------------------------------------------
    //Initialize modal windows
    @ViewChild("newMoneyIncome")
    private newMoneyIncomeWindow: ModalComponent;

    @ViewChild("newMoneySpending")
    private newMoneySpendingWindow: ModalComponent;

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
    private moneyIncomeForm: FormGroup;
    private moneySpendingForm: FormGroup;
    private moneyTransferForm: FormGroup;
    private accountManagmentForm: FormGroup;
    private updateFinOperationForm: FormGroup;
    private bankAccountManagmentForm: FormGroup;


    private moneyOperationModel: FinOpListViewModel = new FinOpListViewModel();
    private moneyIncome: FinOpListViewModel = new FinOpListViewModel();
    private moneySpending: FinOpListViewModel = new FinOpListViewModel();
    private moneyTransfer: FinOpListViewModel = new FinOpListViewModel();
    private manageAccount: OrgAccountViewModel = new OrgAccountViewModel();
    private updateFinOperation: FinOpListViewModel = new FinOpListViewModel();
    //-------------------------------------------------------------------------------

    images: Image[] = [];


    public constructor(private _router: Router,
        private finOpService: FinOpService,
        private fb: FormBuilder,
        private validatorsService: ValidatorsService,
        private accountService: OrgAccountService,
        private donateService: DonateService,
        private userService: UserService,
        private editService: EditOrganizationService) {
            this.createIncomeForm();
            this.createSpendingForm();
            this.createTransferForm();
            this.createManagmantForm();
            this.createUpdateFinOperationForm();
    }
    private navigateToImportsPage(): void {
        this._router.navigate(['/finance/bank-import']);
    }

    /*
    Checks for value changes and assignes new account in the component
    */
    ngOnChanges(changes: { [propKey: string]: SimpleChange }) {
        this.getFinOpsByOrgAccountId();
        this.getCurrentOrgAccount();
        this.default = false;
    }

    ngOnInit(): void {
        this.getOrgTargetsAndBaseTargets();
        this.getAllCashAccounts();
        this.getCurrencies();
        this.getModerators();
        this.getLoggedUser();
        this.getMinDate();
    }

    ngAfterContentInit(): void {
        this.setOwner();
    }

    private getAccontsForTransfer() {
        if (this.currentAccount.targetId === null) {
            this.accountsTo = this.accounts.filter(acc => acc.id != this.currentAccount.id);
        }
        else {
            this.accountsTo = this.accounts.filter(acc =>
                acc.targetId === this.currentAccount.targetId &&
                acc.id != this.currentAccount.id);
        }
    }

    private setFinOperations() {
        for (var i = 0; i < this.finOps.length; i++) {

            if (this.finOps[i].finOpType == 1) {
                this.finOps[i].finOpName = constant.incomeUA;
            }

            else if (this.finOps[i].finOpType == 0) {
                this.finOps[i].finOpName = constant.spendingUA;
            }

            else {
                this.finOps[i].finOpName = constant.transferUA;
            }
        }
    }

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

    private getFinOpsByOrgAccountId() {
        this.finOpService.getFinOpsByOrgAccountId(this.accountId)
            .subscribe(a => {
                this.finOps = a;
                this.setFinOperations();
                this.reservedFinOpsArray = this.finOps;
                this.finOps = this.finOps.slice(0, 10);
            });
    }

    private getFinOpById(id: number) {
        this.finOpService.getFinOpById(id).subscribe(f => {
            this.updateFinOperation = f;
        });}

    private getCurrentOrgAccount() {
        this.accountService.getOrganizationAccountById(this.accountId)
            .subscribe(currAcc => {
                this.currentAccount = currAcc;                                     
                this.accountForUpdate = currAcc;
                this.getAccountType();
                this.getAccontsForTransfer();
            });
    }

    private viewFinOps() {
        this.finOps = this.reservedFinOpsArray;
        this.default = false;
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

    private getCurrencies() {
        this.donateService.getCurrencies()
            .subscribe(curr => {
                this.currencies = curr;
            });
    }

    private viewFinOpsByOperation(operation: number) {
        this.finOps = new Array<FinOpListViewModel>();
        for (var i = 0; i < this.reservedFinOpsArray.length; i++) {
            if (this.reservedFinOpsArray[i].finOpType == operation) {
                this.finOps.push(this.reservedFinOpsArray[i]);
            }
        }
        this.finOps = this.finOps.slice(0, 10);    
        this.default = true;
    }

    private getModerators() {
        this.editService.getModerators(this.orgId)
            .subscribe(moder => {
                this.moderators = moder;
            });
    }

    public getMinDate() {
        let date = new Date();
        date.setDate(date.getDate() - 10);
        this.minDate = date.toJSON().slice(0, 10);
    }

    public getLoggedUser() {
        this.user = JSON.parse(localStorage.getItem(key.keyModel)) as AuthorizeUserModel;
    }

    public getAccountType() {
        if (this.currentAccount.accountType === constant.cashUA) {
            this.isCashType = true;
        }
        else {
            this.isCashType = false;
        }
    }

    public setDate(model: FinOpListViewModel, date: Date) {
        model.date = date;
    }

    private setOwner() {
        this.accountOwner = this.moderators.find(m => m.id == this.currentAccount.userId);
    }

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

    private pushReverse(array: Array<any>, element: any) {
        array.reverse();
        array.push(element);
        array.reverse();
    }

    private createIncomeForm() {
        this.moneyIncomeForm = this.fb.group({
            cardToId: [
                this.moneyOperationModel.cardToId
            ],
            amount: [
                this.moneyOperationModel.amount, [Validators.required,
                this.validatorsService.isMinValue,
                this.validatorsService.isMaxValue,
                this.validatorsService.isNumber
                ]
            ],
            targetId: [
                this.moneyOperationModel.targetId, [Validators.required]
            ],
            description: [
                this.moneyOperationModel.description, [Validators.maxLength(500)]
            ],
            date: [
                this.moneyOperationModel.date, [Validators.required]
            ]
        });
        this.moneyIncomeForm.valueChanges
            .subscribe(a => this.onValueChange(this.moneyIncomeForm, this.formIncomeErrors, a));
        this.onValueChange(this.moneyIncomeForm, this.formIncomeErrors);
    }

    private createSpendingForm() {
        this.moneySpendingForm = this.fb.group({
            cardFromId: [
                this.moneyOperationModel.cardFromId
            ],
            amount: [
                this.moneyOperationModel.amount, [Validators.required,
                this.validatorsService.isMinValue,
                this.validatorsService.isMaxValue,
                this.validatorsService.isNumber
                ]
            ],
            targetId: [
                this.moneyOperationModel.targetId, [Validators.required]
            ],
            description: [
                this.moneyOperationModel.description, [Validators.maxLength(500)]
            ],
            date: [
                this.moneyOperationModel.date, [Validators.required]
            ]
        });
        this.moneySpendingForm.valueChanges
            .subscribe(a => this.onValueChange(this.moneySpendingForm, this.formSpendingErrors, a));
        this.onValueChange(this.moneySpendingForm, this.formSpendingErrors);
    }

    private createTransferForm() {
        this.moneyTransferForm = this.fb.group({
            cardFromId: [
                this.moneyOperationModel.cardFromId
            ],
            cardToId: [
                this.moneyOperationModel.cardToId, [Validators.required
                ]
            ],
            amount: [
                this.moneyOperationModel.amount, [Validators.required,
                this.validatorsService.isMinValue,
                this.validatorsService.isMaxValue,
                this.validatorsService.isNumber
                ]
            ],
            description: [
                this.moneyOperationModel.description, [Validators.maxLength(500)]
            ],
            date: [
                this.moneyOperationModel.date, [Validators.required]
            ]
        });
        this.moneyTransferForm.valueChanges
            .subscribe(a => this.onValueChange(this.moneyTransferForm, this.formTransferErrors, a));
        this.onValueChange(this.moneyTransferForm, this.formTransferErrors);
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

    private createUpdateFinOperationForm() {
        this.updateFinOperationForm = this.fb.group({
            amount: [
                this.updateFinOperation.amount, [this.validatorsService.isMinValue,
                this.validatorsService.isMaxValue,
                this.validatorsService.isNumber
                ]
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
        this.accountManagmentForm.valueChanges
            .subscribe(a => this.onValueChange(this.updateFinOperationForm, this.formUpdateErrors, a));
        this.onValueChange(this.updateFinOperationForm, this.formUpdateErrors);
    }

    private formIncomeErrors = {
        amount: "",
        description: ""
    };

    private formSpendingErrors = {
        amount: "",
        description: ""
    };

    private formTransferErrors = {
        amount: "",
        description: ""
    };

    private formUpdateErrors = {
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

    private openModal(modal: ModalComponent) {
        modal.show();
    }

    private openUpdateFinOpModal(finOp: FinOpListViewModel) {
        this.currentFinOp = finOp;
        this.getFinOpById(finOp.id);
        if (finOp.finOpType === constant.transferId) {
            this.isTransferOperation = true;
        }
        this.getSubTargetsByTargetId(finOp.targetId);
        this.openModal(this.newUpdateFinOperationWindow);
    }

    private closeModal(modal: ModalComponent, form: FormGroup) {
        modal.hide();
        form.reset();
        this.isBaseTargetChosen = false;
        this.isTransferOperation = false;
    }

    private makeIncome() {
        this.completeModel();
        this.moneyOperationModel.cardToId = this.currentAccount.id;
        this.moneyOperationModel.finOpType = constant.incomeId;
        this.finOpService.createIncome(this.moneyOperationModel).subscribe(a => {
            this.moneyIncome = a;
        });
        this.closeModal(this.newMoneyIncomeWindow, this.moneyIncomeForm);
    }

    private makeSpending() {
        this.moneyOperationModel.cardFromId = this.currentAccount.id;
        this.moneyOperationModel.finOpType = constant.spendingId;
        this.finOpService.createSpending(this.moneyOperationModel).subscribe(a => {
            this.moneySpending = a;
        });
        this.closeModal(this.newMoneySpendingWindow, this.moneySpendingForm);
    }

    private makeTransfer() {
        this.completeModel();
        this.moneyOperationModel.cardFromId = this.currentAccount.id;
        this.moneyOperationModel.finOpType = constant.transferId;
        this.finOpService.createTransfer(this.moneyOperationModel).subscribe(a => {
            this.moneyTransfer = a;
            this.pushReverse(this.finOps, this.moneyTransfer);
        });
        this.closeModal(this.newMoneyTransferWindow, this.moneyTransferForm);
    }

    private updateOrgAccount() {
        this.accountService.updateOrganizationAccount(this.accountForUpdate).subscribe(a => {
            this.manageAccount = a;
        })
    }

    private updateFinOp() {
        this.updateFinOperation.userId = this.currentAccount.userId;
        this.currentFinOp = this.updateFinOperation;
        this.finOpService.editFinOperation(this.updateFinOperation).subscribe(f => {
            this.updateFinOperation = f;
        });
        this.isTransferOperation = false;
        this.closeModal(this.newUpdateFinOperationWindow, this.updateFinOperationForm);
    }

    private completeModel() {
        this.moneyOperationModel.orgId = this.orgId;
        this.moneyOperationModel.userId = this.user.id;
        var arr: string[] = [];
        for (var i = 0; i < this.images.length; i++) {
            arr[i] = this.images[i].base64Data;
        }
        this.moneyOperationModel.images = arr;
    }

    public closeWindow(modal: ModalComponent) {
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

    private getSuggestedDonations(finOp : FinOpListViewModel) {
        this.donateService.getSuggestedDonations(finOp.id).subscribe(result => {
            this.suggestedDonations = result;
            this.selectedFinOp = finOp;
            this.suggestedDonationsModal.show();
            if (this.suggestedDonations.length == 1) {
                this.selectedDonationId = this.suggestedDonations[0].id;
            }
        });
        this.userService.getAllUsers().subscribe(result => this.users = result);
    }

    private IsOkButtonEnable(): boolean {
        if (this.suggestedDonations.length > 1) {
            if (this.selectedDonationId == undefined || this.selectedUserId == undefined) {
                return false;
            }
        }
        return true;
    }

    private radioButtonOnChange(donation : DonateViewModel) {
        this.selectedDonationId = donation.id;
    }

    private closeSuggestionsModal() {
        this.selectedDonationId = undefined;
        this.selectedUserId = undefined;
        this.suggestedDonationsModal.hide();
    }

    private onClickSuggestionModalButton() {
        if (this.suggestedDonations.length == 0) {
            this.toasterMessage = message.inAccessibleOperation;
            this.showToast();
            this.closeSuggestionsModal();
            return;
        }
        this.selectedFinOp.userId = this.selectedUserId;
        this.selectedFinOp.donationId = this.selectedDonationId;
        this.finOpService.bindDonationAndFinOp(this.selectedFinOp).subscribe(result => {
            this.toasterMessage = message.successfulOperation;
            this.showToast();
            this.closeSuggestionsModal();
        });
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
}