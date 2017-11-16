import { ViewChild, Component, OnInit, Input, SimpleChange, OnChanges, Output, EventEmitter } from "@angular/core";
import { Router } from "@angular/router";
import { OrgAccountService } from "../../services/concrete/finance/orgaccount.service";
import { OrgAccountViewModel } from "../../view-models/concrete/finance/orgaccount-viewmodel";
import { FixingBalanceService } from "../../services/concrete/fixing-balance.service";
import * as key from '../../shared/key.storage';
import * as constant from '../../shared/default-configuration.storage';
import * as message from '../../shared/common-message.storage';
import { FinOpService } from "../../services/concrete/finance/finOp.service";
import { FinOpListViewModel } from "../../view-models/concrete/finance/finop-list-viewmodel";
import { isBrowser } from "angular2-universal";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { ValidatorsService } from "../../services/concrete/validators/validator.service";
import Core = require("@angular/core");
import { EditOrganizationService } from "../../services/concrete/organization-management/edit-organization.service";
import { ModeratorViewModel } from '../../view-models/concrete/edit-organization/moderator-view.model';
import { AuthorizeUserModel } from "../../view-models/concrete/authorized-user-info-view.model";
import { Location } from '@angular/common';
import { ActivatedRoute } from "@angular/router";
import { UserService } from "../../services/concrete/user.service";

@Component({
    selector: 'transferoperation',
    templateUrl: './transferoperation.component.html',
    styleUrls: ['./transferoperation.component.css'],
    providers: [OrgAccountService, EditOrganizationService, UserService, FixingBalanceService]
})

export class TransferOperationComponent {

    private moneyTransferForm: FormGroup;
    private moneyOperationModel: FinOpListViewModel = new FinOpListViewModel();
    private moneyTransfer: FinOpListViewModel = new FinOpListViewModel();
    private cashAccounts: OrgAccountViewModel[] = new Array<OrgAccountViewModel>();
    private cashAccountsTo: OrgAccountViewModel[] = new Array<OrgAccountViewModel>();
    private currentAccount: OrgAccountViewModel = new OrgAccountViewModel();
    private currentDate = new Date().toJSON().slice(0, 10);
    private currentAccountId: number;
    private user: AuthorizeUserModel = new AuthorizeUserModel();
    private minDate: string;

    public constructor(private router: Router,
        private location: Location,
        private route: ActivatedRoute,
        private finOpService: FinOpService,
        private fb: FormBuilder,
        private validatorsService: ValidatorsService,
        private accountService: OrgAccountService,
        private userService: UserService,
        private editService: EditOrganizationService,
        private fixingService: FixingBalanceService) {
        this.createTransferForm();
    }

    ngOnInit(): void {
        this.initialize();
    }

    private getCurrentOrgAccount(accountId: number) {
        this.accountService.getOrganizationAccountById(accountId)
            .subscribe(currAcc => {
                this.currentAccount = currAcc;
                this.getMinDate();
            });
    }

    private initialize() {
        this.moneyOperationModel.date = new Date();
        this.getLoggedUser();
        this.route.params.subscribe(params => {
            this.currentAccountId = params['id'];
            this.getCurrentOrgAccount(this.currentAccountId);
        });
        this.getOrganizationCashAccounts();

    }

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

    private getOrganizationCashAccounts() {
        this.accountService.getAllAccountsOfOrganization()
            .subscribe(acc => {
                this.cashAccounts = acc.filter(a =>
                    a.accountType == constant.cashUA
                );
                this.getAccountsForTransfer();
            });
    }

    private getAccountsForTransfer() {
        if (this.currentAccount.targetId == null) {
            this.cashAccountsTo = this.cashAccounts.filter(acc => acc.id != this.currentAccount.id);
        }
        else {
            this.cashAccountsTo = this.cashAccounts.filter(acc =>
                acc.targetId
                == this.currentAccount.targetId &&
                acc.id != this.currentAccount.id);
        }
    }

    public getLoggedUser() {
        this.user = JSON.parse(localStorage.getItem(key.keyModel)) as AuthorizeUserModel;
    }

    public setDate(model: FinOpListViewModel, date: Date) {
        model.date = date;
    }

    private createTransferForm() {
        this.moneyTransferForm = this.fb.group({
            cardFromId: [
                this.moneyOperationModel.accFromId
            ],
            cardToId: [
                this.moneyOperationModel.accToId, [Validators.required]
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
                this.moneyOperationModel.date
            ]
        });
        this.moneyTransferForm.valueChanges
            .subscribe(a => this.onValueChange(a));
        this.onValueChange();
    }

    private formTransferErrors = {
        amount: "",
        description: ""
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

    private onValueChange(data?: any) {
        if (!this.moneyTransferForm) return;
        let form = this.moneyTransferForm;

        for (let field in this.formTransferErrors) {
            this.formTransferErrors[field] = "";
            let control = form.get(field);

            if (control && control.dirty && !control.valid) {
                let message = this.validationMessages[field];
                for (let key in control.errors) {
                    this.formTransferErrors[field] = message[key.toLowerCase()];
                }
            }
        }
    }

    private makeTransfer() {
        this.completeModel();
        this.finOpService.createTransfer(this.moneyOperationModel).subscribe(a => {
            if (a.error == "" || a.error == null) {
                this.showToast();
                setTimeout(() => {
                    this.router.navigate(['finance/orgaccounts']);
                },
                    2000);

            } else {
                this.moneyOperationModel.error = a.error;
            }
        });
    }

    private completeModel() {
        this.moneyOperationModel.accFromId = this.currentAccount.id;
        this.moneyOperationModel.finOpType = constant.transferId;
        this.moneyOperationModel.orgId = this.user.orgId;
        this.moneyOperationModel.userId = this.user.id;
    }

    public showToast() {
        var x = document.getElementById("snackbar");
        x.className = "show";
        setTimeout(function () { x.className = x.className.replace("show", ""); }, 3000);
    }


    private navigateBack(): void {
        this.location.back();
    }
}