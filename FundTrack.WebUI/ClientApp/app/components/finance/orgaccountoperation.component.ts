import { ViewChild, Component, OnInit, Input, SimpleChange, OnChanges } from "@angular/core";
import { Router } from "@angular/router";
import { OrgAccountService } from "../../services/concrete/finance/orgaccount.service";
import { OrgAccountViewModel } from "../../view-models/concrete/finance/orgaccount-viewmodel";
import { DecimalPipe } from '@angular/common';
import { CurrencyPipe } from '@angular/common';
import * as key from '../../shared/key.storage';
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


@Component({
    selector: 'orgaccountoperation',
    templateUrl: './orgaccountoperation.component.html',
    styleUrls: ['./orgaccountoperation.component.css'],
    providers: [DonateService, OrgAccountService, EditOrganizationService]
})
export class OrgAccountOperationComponent implements OnChanges {

    private accounts: OrgAccountViewModel[] = new Array<OrgAccountViewModel>();
    private accountsTo: OrgAccountViewModel[] = new Array<OrgAccountViewModel>();
    private currencies: CurrencyViewModel[] = new Array<CurrencyViewModel>();
    private targets: TargetViewModel[] = new Array<TargetViewModel>();
    private currentAccount: OrgAccountViewModel = new OrgAccountViewModel();
    private accountForUpdate: OrgAccountViewModel = new OrgAccountViewModel();
    private finOps: FinOpListViewModel[] = new Array<FinOpListViewModel>();
    private reservedFinOpsArray: FinOpListViewModel[] = new Array<FinOpListViewModel>();
    private moderators: ModeratorViewModel[] = new Array<ModeratorViewModel>();
    private accountOwner: ModeratorViewModel = new ModeratorViewModel();
    private currentDate = new Date().toJSON().slice(0, 10);
    private user: AuthorizeUserModel = new AuthorizeUserModel();
    private minDate: string;
    private default: boolean = true;
    private isCashType: boolean = false;

    @Input() orgId: number;
    @Input() accountId: number;
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

    @ViewChild("fixingBalanceModal")
    private fixingBalanceModal: ModalComponent;
    //-------------------------------------------------------------------------------
    //Initialize model and form
    private moneyIncomeForm: FormGroup;
    private moneySpendingForm: FormGroup;
    private moneyTransferForm: FormGroup;
    private accountManagmentForm: FormGroup;

    private moneyOperationModel: MoneyOperationViewModel = new MoneyOperationViewModel();
    private moneyIncome: MoneyOperationViewModel = new MoneyOperationViewModel();
    private moneySpending: MoneyOperationViewModel = new MoneyOperationViewModel();
    private moneyTransfer: MoneyOperationViewModel = new MoneyOperationViewModel();
    //-------------------------------------------------------------------------------

    images: Image[] = [];

    public constructor(private _router: Router,
        private finOpService: FinOpService,
        private fb: FormBuilder,
        private validatorsService: ValidatorsService,
        private accountService: OrgAccountService,
        private donateService: DonateService,
        private editService: EditOrganizationService) {
            this.createIncomeForm();
            this.createSpendingForm();
            this.createTransferForm();
            this.createManagmantForm();
    }
    private navigateToImportsPage(): void {
        this._router.navigate(['/finance/bank-import']);
    }

    /*
    Checks for value changes and assignes new account in the component
    */
    ngOnChanges(changes: { [propKey: string]: SimpleChange }) {
        if (changes['accountId'] && changes['accountId'] != changes['accountId'].currentValue) {
            //code to execute when property changes
            if (this.accountId!=-1) {
                this.finOpService.getFinOpsByOrgAccountId(this.accountId)
                    .subscribe(a => {
                        this.finOps = a;
                        this.setFinOperations();
                        this.reservedFinOpsArray = this.finOps;
                        this.finOps = this.finOps.slice(0, 10);       
                    });
            }
        }
        this.accountService.getOrganizationAccountById(this.accountId)
            .subscribe(currAcc => {
                this.currentAccount = currAcc;                                      //get current organization account
                this.accountForUpdate = currAcc;
                this.getType();
                this.getAccontsForTransfer();
            });

        this.default = false;

    }

    ngOnInit(): void {
        this.accountService.getAllAccountsOfOrganization()
            .subscribe(acc => {
                this.accounts = acc.filter(a =>
                    a.accountType === "Готівка"                                      //get all cash accounts
                );
                this.getAccontsForTransfer();
            });
        this.donateService.getCurrencies()
            .subscribe(curr => {
                this.currencies = curr;
            });
        this.donateService.getTargets()
            .subscribe(targ => {
                this.targets = targ;
            });

        //this.editService.getModerators(this.orgId)
        //    .subscribe(moder => {
        //        this.moderators = moder;
        //        console.log("sssws2s2s2" + moder);
        //    });

        this.user = JSON.parse(localStorage.getItem(key.keyModel)) as AuthorizeUserModel;

        this.getMinDate();
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
                this.finOps[i].finOpName = "Прихід";
            }

            else if (this.finOps[i].finOpType == 0) {
                this.finOps[i].finOpName = "Розхід";
            }

            else {
                this.finOps[i].finOpName = "Переміщення"
            }
        }
    }

    private viewFinOps() {
        this.finOps = this.reservedFinOpsArray;
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
        this.editService.getModerators(this.accountId)
            .subscribe(moder => {
                this.moderators = moder;
            })
    }

    public getMinDate() {
        let date = new Date();
        date.setDate(date.getDate() - 10);
        this.minDate = date.toJSON().slice(0, 10);
    }

    public getType() {
        if (this.currentAccount.accountType === "Готівка") {
            this.isCashType = true;
        }
        else {
            this.isCashType = false;
        }
    }

    public setDate(date: Date): void {
        this.moneyOperationModel.date = date;
    }

    private getOwner() {
        this.accountOwner = this.moderators.filter(m => m.id === this.currentAccount.userId)[0];
    }

    private createIncomeForm() {
        this.moneyIncomeForm = this.fb.group({
            cardToId: [
                this.moneyOperationModel.cardToId
            ],
            sum: [
                this.moneyOperationModel.sum, [Validators.required,
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
            sum: [
                this.moneyOperationModel.sum, [Validators.required,
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
            sum: [
                this.moneyOperationModel.sum, [Validators.required,
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

        });
    }

    private formIncomeErrors = {
        sum: "",
        description: ""
    };

    private formSpendingErrors = {
        sum: "",
        description: ""
    };

    private formTransferErrors = {
        sum: "",
        description: ""
    };

    private invalidSumMessage = "Поле ‘Сума’ може містити лише цілі числа в межах від 5 до 15000 грн";
    private maxLengthDescription = "Поле ‘Опис’ може містити не більше 500 символів";

    private validationMessages = {
        sum: {
            notnumber: this.invalidSumMessage,
            notminvalue: this.invalidSumMessage,
            notmaxvalue: this.invalidSumMessage
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

    private openManageModal() {
        //this.accountOwner = this.moderators.filter(m => m.id === this.currentAccount.userId)[0];
        this.newAccountManagmentWindow.show();
    }

    private closeModal(modal: ModalComponent, form: FormGroup) {
        modal.hide();
        form.reset();
    }

    private makeIncome() {
        this.completeModel();
        this.moneyOperationModel.cardToId = this.currentAccount.id;
        this.moneyOperationModel.finOpType = 1;
        this.finOpService.createIncome(this.moneyOperationModel).subscribe(a => {
            this.moneyIncome = a;
        });
        this.closeModal(this.newMoneyIncomeWindow, this.moneyIncomeForm);
    }

    private makeSpending() {
        this.completeModel();
        this.moneyOperationModel.cardFromId = this.currentAccount.id;
        this.moneyOperationModel.finOpType = 0;
        this.finOpService.createSpending(this.moneyOperationModel).subscribe(a => {
            this.moneySpending = a;
        });
        this.closeModal(this.newMoneySpendingWindow, this.moneySpendingForm);
    }

    private makeTransfer() {
        this.completeModel();
        this.moneyOperationModel.cardFromId = this.currentAccount.id;
        this.moneyOperationModel.finOpType = 2;
        this.finOpService.createTransfer(this.moneyOperationModel).subscribe(a => {
            this.moneyTransfer = a;
        });
        this.closeModal(this.newMoneyTransferWindow, this.moneyTransferForm);
    }

    private completeModel() {
        this.moneyOperationModel.orgId = this.accounts[0].orgId;
        this.moneyOperationModel.userId = this.user.id;
        var arr: string[] = [];
        for (var i = 0; i < this.images.length; i++) {
            arr[i] = this.images[i].base64Data;
        }
        this.moneyOperationModel.images = arr;
    }
    handleCloseModalEvent(isCloseModal) {
        console.log(isCloseModal);
        if (isCloseModal) {
            this.fixingBalanceModal.hide();
        }
    }

}