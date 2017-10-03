import { Component } from "@angular/core";
import * as key from '../../shared/key.storage';
import { isBrowser } from "angular2-universal";
import { Router, Route } from "@angular/router";
import { OrgAccountService } from "../../services/concrete/finance/orgaccount.service";
import { OrgAccountViewModel } from "../../view-models/concrete/finance/orgaccount-viewmodel";
import { DecimalPipe } from '@angular/common';
import { CurrencyPipe } from '@angular/common';
import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CustomValidators } from 'ng2-validation';
import { ValidatorsService } from "../../services/concrete/validators/validator.service";
import { AuthorizeUserModel } from "../../view-models/concrete/authorized-user-info-view.model";
import { Location } from '@angular/common';
import Targetviewmodel1 = require("../../view-models/concrete/finance/donate/target.view-model");
import TargetViewModel = Targetviewmodel1.TargetViewModel;

@Component({
    selector: 'createorgaccount',
    templateUrl: './createorgaccount.component.html',
    styleUrls: ['./createorgaccount.component.css']
})
export class CreateOrgAccountComponent {
    //Property, that keeps data about logged in user
    private user: AuthorizeUserModel;
    //Property, that keeps account data
    private account: OrgAccountViewModel = new OrgAccountViewModel();
    //Property that keeps page title value
    private pageTitle: string = 'Створення рахунку організації';
    //Property that keeps regex that allows only integer and decimal numbers with a point
    private decimalValidationRegex: string = "^[0-9]{1,7}([.][0-9]{1,2})?$";
    //Property that keeps accountForm object;
    private accountForm: FormGroup;
    //Property that keeps smallAccountForm object;
    private smallAccountForm: FormGroup;
    // Property that keeps all targets of current organization
    private targets: TargetViewModel[] = new Array<TargetViewModel>();

    constructor(private _accountService: OrgAccountService,
        private _fb: FormBuilder,
        private _validatorsService: ValidatorsService,
        private _location: Location,
        private _router: Router
    ) {
        this.createForm();
        this.createSmallForm();
        this.account.accountType = 'cash';
    }

    ngOnInit(): void {
        console.log("Create-Account");
        if (isBrowser) {
            if (localStorage.getItem(key.keyToken)) {
                this.user = JSON.parse(localStorage.getItem(key.keyModel)) as AuthorizeUserModel;
            }
        };
        this._accountService.getAllBaseTargetsOfOrganization(this.user.orgId).subscribe((result) => {
            this.targets.push({
                targetId: undefined,
                name: "Не вказано",
                organizationId: undefined,
                parentTargetId: undefined
            });
            for (var target of result) {
                this.targets.push(target);
            }
        });
    }

    /*
    Creates main form and assigns validators to specified form controls
    */
    private createForm(): void {
        this.accountForm = this._fb.group({
            accountNumber: [this.account.accNumber, [Validators.required, Validators.maxLength(20), this._validatorsService.isInteger]],
            accountMfo: [this.account.mfo, [Validators.required, Validators.minLength(6), Validators.maxLength(6), this._validatorsService.isInteger]],
            accountEdrpou: [this.account.edrpou, [Validators.required, Validators.minLength(8), Validators.maxLength(8), this._validatorsService.isInteger]],
            accountBankName: [this.account.bankName, [Validators.required, Validators.maxLength(50)]],
            accountDescription: [this.account.description, [Validators.required, Validators.maxLength(200)]],
        });
        this.accountForm.valueChanges
            .subscribe(a => this.onValueChange(a));
    }

     /*
    Creates small form and assigns validators to specified form controls
    */
    private createSmallForm(): void {
        this.smallAccountForm = this._fb.group({
            accountName: [this.account.orgAccountName, [Validators.required, Validators.maxLength(100)]],
            currentBalance: [this.account.currentBalance, Validators.pattern(this.decimalValidationRegex)],
            accountCurrency: [this.account.currency, Validators.required],
            accountTarget: [this.account.targetId]
        });
        this.smallAccountForm.valueChanges
            .subscribe(a => this.onValueChangeSmallForm(a));

        //this.onValueChangeSmallForm();
    }

    /*
    Errors to be displayed on the UI
    */
    private formErrors = {
        accountNumber: "",
        accountMfo: "",
        accountEdrpou: "",
        accountBankName: "",
        accountDescription: ""
    }
    private smallFormErrors = {
        accountName: "",
        currentBalance: "",
        accountCurrency: ""
    }
    /*
    Error messages
    */
    private requiredMessage = "Поле є обов'язковим для заповнення";
    private numberMessage = "Поле повинно містити тільки цифри";
    private LengthMessage = "Недопустима кількість символів";
    private accountNameWrongLength = "Максимально допустима довжина імені рахунку складає 100 символів";
    private mfolength = "МФО повинно містити 6 цифр";
    private edrpoulength = "ЄДРПОУ повинно містити 8 цифр";
    private banknamelength = "Ім'я банку не може перевищувати 50 символів";
    private descriptionlength = "Опис рахунку не може перевищувати 200 символів";
    private currencyselection = "Необхідно вибрати валюту";
    private wrongBalanceMessage = "Баланс повинен бути у форматі 1 або 1.23";

    /*
    Assigning error messages to certain validations
    */
    private validationMessages = {
        accountName: {
            required: this.requiredMessage,
            maxlength: this.accountNameWrongLength
        },
        accountNumber: {
            required: this.requiredMessage,
            notnumeric: this.numberMessage,
            maxlength: this.LengthMessage
        },
        accountMfo: {
            required: this.requiredMessage,
            notnumeric: this.numberMessage,
            minlength: this.mfolength,
            maxlength: this.mfolength
        },
        accountEdrpou: {
            required: this.requiredMessage,
            notnumeric: this.numberMessage,
            minlength: this.edrpoulength,
            maxlength: this.edrpoulength
        },
        accountBankName: {
            required: this.requiredMessage,
            maxlength: this.banknamelength
        },
        accountDescription: {
            required: this.requiredMessage,
            maxlength: this.descriptionlength
        },
        accountCurrency: {
            required: this.currencyselection
        },
        currentBalance: {
            pattern: this.wrongBalanceMessage
        }
    }

    /**
     * Subscriber on value changes
     * @param data
     */
    private onValueChange(data?: any) {
        if (!this.accountForm) return;
        let form = this.accountForm;

        for (let field in this.formErrors) {
            this.formErrors[field] = "";
            let control = form.get(field);

            if (control && control.dirty && !control.valid) {
                let message = this.validationMessages[field];
                for (let key in control.errors) {
                    this.formErrors[field] = message[key.toLowerCase()];
                }
            }
        }
    }

    /**
     * Subscriber on value changes
     * @param data
     */
    private onValueChangeSmallForm(data?: any) {
        if (!this.smallAccountForm) return;
        let form = this.smallAccountForm;

        for (let field in this.smallFormErrors) {
            this.smallFormErrors[field] = "";
            let control = form.get(field);

            if (control && control.dirty && !control.valid) {
                let message = this.validationMessages[field];
                for (let key in control.errors) {
                    this.smallFormErrors[field] = message[key.toLowerCase()];
                }
            }
        }
    }

    /*
    Verifies if form status is valid
    */
    private checkFormValidity(): boolean {
        if (this.account.accountType == 'cash' && !this.smallAccountForm.invalid) {
            return false;
        }
        else if (this.account.accountType == 'bank' && !this.smallAccountForm.invalid && !this.accountForm.invalid) {
            return false;
        }
        else {
            return true;
        }
    }

    /*
    Creates new organization account
    */
    private createAccount(account: OrgAccountViewModel) {
        if (isBrowser) {
            this.user = JSON.parse(localStorage.getItem(key.keyModel)) as AuthorizeUserModel;
        }
        else {
            return;
        }
        this.account.orgId = this.user.orgId;
        this.account.creationDate = new Date();
        this.account.userId = this.user.id;
        this._accountService.createOrgAccount(this.account)
            .subscribe(a => {
                if (a.error == "" || a.error == null) {
                    this.showToast();
                    setTimeout(() => {
                        this._router.navigate(['finance/orgaccounts']);
                    }, 2000);
                    
                }
                else {
                    this.account.error = a.error;
                }
            })
    }

    /*
    Navigates back to the previous page
    */
    private navigateBack(): void {
        this._location.back();
    }

    /*
    Displays toast, that pops up when account is successfuly created
    */
    public showToast() {
        var x = document.getElementById("snackbar")
        x.className = "show";
        setTimeout(function () { x.className = x.className.replace("show", ""); }, 3000);
    }

    private onChangeParentTarget($event): void {
        this.account.targetId = $event;
    }

}