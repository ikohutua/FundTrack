import { Component, OnInit, Input, SimpleChange, OnChanges, ViewChild} from "@angular/core";
import { Router } from "@angular/router";
import { DatePipe } from '@angular/common';
import { OrgAccountService } from "../../services/concrete/finance/orgaccount.service";
import { DonateService } from "../../services/concrete/finance/donate-money.service";
import { OrgAccountViewModel } from "../../view-models/concrete/finance/orgaccount-viewmodel";
import { CurrencyViewModel } from "../../view-models/concrete/finance/donate/currency.view-model"
import { TargetViewModel } from "../../view-models/concrete/finance/donate/target.view-model";
import { DecimalPipe } from '@angular/common';
import { CurrencyPipe } from '@angular/common';
import { FinOpService } from "../../services/concrete/finance/finOp.service";
import { FinOpListViewModel } from "../../view-models/concrete/finance/finop-list-viewmodel";
import { isBrowser } from "angular2-universal";
import { ModalComponent } from "../../shared/components/modal/modal-component";
import { MoneyOperationViewModel } from "../../view-models/concrete/finance/money-operation-view-model";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { ValidatorsService } from "../../services/concrete/validators/validator.service";
import Core = require("@angular/core");
import Targetviewmodel = require("../../view-models/concrete/finance/donate/target.view-model");
import { enableProdMode } from '@angular/core';

@Component({
    selector: 'orgaccountoperation',
    templateUrl: './orgaccountoperation.component.html',
    styleUrls: ['./orgaccountoperation.component.css'],
    providers: [DonateService, OrgAccountService]
})
export class OrgAccountOperationComponent implements OnChanges {

    private accounts: OrgAccountViewModel[] = new Array<OrgAccountViewModel>();
    private currencies: CurrencyViewModel[] = new Array<CurrencyViewModel>();
    private targets: TargetViewModel[] = new Array<TargetViewModel>();
    private currentAccount: OrgAccountViewModel = new OrgAccountViewModel();

    @Input('orgId') orgId: number;
    @Input() accountId: number;
//------------------------------------------------------------------------------
    //Initialize modal windows
    @ViewChild("newMoneyIncome")
    private newMoneyIncomeWindow: ModalComponent;

    @ViewChild("newMoneySpending")
    private newMoneySpendingWindow: ModalComponent;

    @ViewChild("newMoneyTransfer")
    private newMoneyTransferWindow: ModalComponent;
//-------------------------------------------------------------------------------
    //Initialize model and form
    private moneyIncomeForm: FormGroup;
    private moneySpendingForm: FormGroup;
    private moneyTransferForm: FormGroup;

    private moneyOperationModel: MoneyOperationViewModel = new MoneyOperationViewModel();
//-------------------------------------------------------------------------------
    private finOps: FinOpListViewModel[] = new Array<FinOpListViewModel>();
    private currentDate = new Date().toJSON().slice(0, 10).replace(/-/g, '/');
    private operations = ['Payment', 'Withdrawn', 'Income', 'etc.'];

    public constructor(private _router: Router,
        private finOpService: FinOpService,
        private fb: FormBuilder,
        private validatorsService: ValidatorsService,
        private accountService: OrgAccountService,
        private donateService: DonateService) {
        //this.createForm();
        this.createIncomeForm();
        this.createSpendingForm();
        this.createTransferForm();
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
            if (this.accountId!=1) {
                this.finOpService.getFinOpsByOrgAccountId(this.accountId)
                    .subscribe(a => {
                        this.finOps = a;
                    });
            }
        }
        this.accountService.getOrganizationAccountById(this.accountId)
            .subscribe(currAcc => {
                this.currentAccount = currAcc;                                      //get current organization account
            })

    }
   
    ngOnInit(): void {
        this.accountService.getAllAccountsOfOrganization()
            .subscribe(acc => {
                this.accounts = acc.filter(a => 
                    a.accountType == "Готівка"                                      //get all cash accounts
                );           
            });
        this.donateService.getCurrencies()
            .subscribe(curr => { 
                this.currencies = curr;
            });
        this.donateService.getTargets()
            .subscribe(targ => {
                this.targets = targ;
            });

    }
    
    private convertDate(date: Date): string {
        var options = {
            weekday: "long", year: "numeric", month: "short",
            day: "numeric", hour: "2-digit", minute: "2-digit"
        };
        return date.toLocaleString("uk-UK", options);
    }

    private createIncomeForm() {
        this.moneyIncomeForm = this.fb.group({
            cardFrom: [
                this.moneyOperationModel.cardFrom
            ],
            sum: [
                this.moneyOperationModel.sum, [Validators.required,
                    this.validatorsService.isMinValue,
                    this.validatorsService.isMaxValue,
                    this.validatorsService.isNumber
                ]
            ],
            currency: [
                this.moneyOperationModel.currency
            ],
            target: [
                this.moneyOperationModel.target, [Validators.required]
            ],
            description: [
                this.moneyOperationModel.description, [Validators.required]
            ]
        });
        this.moneyIncomeForm.valueChanges
            .subscribe(a => this.onValueChange(this.moneyIncomeForm, this.formIncomeErrors, a));
        this.onValueChange(this.moneyIncomeForm, this.formIncomeErrors);
    }

    private createSpendingForm() {
        this.moneySpendingForm = this.fb.group({
            cardFrom: [
                this.moneyOperationModel.cardFrom
            ],
            sum: [
                this.moneyOperationModel.sum, [Validators.required,
                this.validatorsService.isMinValue,
                this.validatorsService.isMaxValue,
                this.validatorsService.isNumber
                ]
            ],
            currency: [
                this.moneyOperationModel.currency
            ],
            target: [
                this.moneyOperationModel.target, [Validators.required]
            ],
            description: [
                this.moneyOperationModel.description, [Validators.required]
            ]
        });
        this.moneySpendingForm.valueChanges
            .subscribe(a => this.onValueChange(this.moneySpendingForm, this.formSpendingErrors, a));
        this.onValueChange(this.moneySpendingForm, this.formSpendingErrors);
    }

    private createTransferForm() {
        this.moneyTransferForm = this.fb.group({
            cardFrom: [
                this.moneyOperationModel.cardFrom
            ],
            cardTo: [
                this.moneyOperationModel.cardTo, [Validators.required
                ]
            ],
            sum: [
                this.moneyOperationModel.sum, [Validators.required,
                this.validatorsService.isMinValue,
                this.validatorsService.isMaxValue,
                this.validatorsService.isNumber
                ]
            ],
            currency: [
                this.moneyOperationModel.currency
            ],
            description: [
                this.moneyOperationModel.description, [Validators.required]
            ]
        });
        this.moneyTransferForm.valueChanges
            .subscribe(a => this.onValueChange(this.moneyTransferForm, this.formTransferErrors, a));
        this.onValueChange(this.moneyTransferForm, this.formTransferErrors);
    }

    private formIncomeErrors = {
        sum: "",
        description: "",
        target: "",
        date: ""
    };

    private formSpendingErrors = {
        sum: "",
        description: "",
        target: "",
        date: ""
    };

    private formTransferErrors = {
        sum: "",
        description: "",
        cardTo: "",
        date: ""
    };

    private requiredMessage = "Поле є обов'язковим для заповнення";
    private numericMessage = "Поле повинно містити тільки цифри";
    private numberMessage = "Поле повинно містити тільки число";
    private lengthMessage = "Недопустима кількість символів";
    private cardMessage = "Номер картки повинен складатися з 16 цифр";
    private minSumMessage = "Поле повинно містити число більше нуля";
    private maxSumMessage = "Поле повинно містити число менше мільйона";

    private validationMessages = {
        sum: {
            required: this.requiredMessage,
            notnumber: this.numberMessage,
            notminvalue: this.minSumMessage,
            notmaxvalue: this.maxSumMessage
        },
        cardTo: {
            required: this.requiredMessage
        },
        target: {
            required: this.requiredMessage
        },
        date: {
            required: this.requiredMessage
        },
        description: {
            required: this.requiredMessage
        }

    }

    private onValueChange(formMoney: FormGroup,formErrors, data?: any) {
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

    private cleanForm() {
        this.moneyOperationModel = new MoneyOperationViewModel();
    }

    private openModal(modal: ModalComponent) {
        this.cleanForm();
        modal.show();
    }

    private closeModal(modal: ModalComponent, form: FormGroup) {
        modal.hide();
        form.reset();
    }

    private makeIncome() {
        this.closeModal(this.newMoneyIncomeWindow, this.moneyIncomeForm);
        this.completeModel();
        this.finOpService.createIncome(this.moneyOperationModel);
    }

    private makeSpending() {
        this.closeModal(this.newMoneySpendingWindow, this.moneySpendingForm);
        this.completeModel();
        this.finOpService.createSpending(this.moneyOperationModel);
    }

    private makeTransfer() {
        this.closeModal(this.newMoneyTransferWindow, this.moneyTransferForm);
        this.completeModel();
        this.finOpService.createTransfer(this.moneyOperationModel);
    }

    private completeModel(){
        this.moneyOperationModel.organizationId = this.accounts[0].orgId;
        this.moneyOperationModel.cardFrom = this.currentAccount.orgAccountName;     
        this.moneyOperationModel.currency = this.currentAccount.currency;
    }

}