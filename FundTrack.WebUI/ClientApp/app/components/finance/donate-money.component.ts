import { Component, OnInit, Input, DoCheck, OnChanges, ViewChild, OnDestroy } from "@angular/core";
import { RequestFondyViewModel } from "../../view-models/concrete/finance/donate/fondy.view-model";
import { CheckPaymentStautsViewModel } from "../../view-models/concrete/finance/donate/check-payment-status.view-model";
import { DonateService } from "../../services/concrete/finance/donate-money.service";
import { FormGroup, FormControl, FormBuilder, Validators } from '@angular/forms';
import { isBrowser } from "angular2-universal";
import * as key from '../../shared/key.storage';

import sha1 = require('sha1');
import { CheckPaymentResponseViewModel } from "../../view-models/concrete/finance/donate/check-payment-response.view-model";
import { DonateAccountViewModel } from "../../view-models/concrete/finance/donate/donate-account.view-model";
import { TargetViewModel } from "../../view-models/concrete/finance/donate/target.view-model";
import { CurrencyViewModel } from "../../view-models/concrete/finance/donate/currency.view-model";
import { ModalComponent } from "../../shared/components/modal/modal-component";
import { OrganizationGetGeneralInfoService } from "../../services/concrete/organization-management/organization-get-general-info.service";
import { MerchantDataViewModel } from "../../view-models/concrete/finance/donate/merchant-data.view-model";
import { AuthorizeUserModel } from "../../view-models/concrete/authorized-user-info-view.model";
import { DonateViewModel } from "../../view-models/concrete/finance/donate/donate.view-model";
import { DonateMessages } from "../../shared/messages/donate-page.messages";
import { StorageService } from "../../shared/item-storage-service";


@Component({
    selector: 'donate-money',
    templateUrl: './donate-money.component.html',
    styleUrls: ['./donate-money.component.css'],
    providers: [DonateService, OrganizationGetGeneralInfoService]
})
export class MakeDonationComponent implements OnInit, DoCheck, OnDestroy{

    fondyPayModel: RequestFondyViewModel = new RequestFondyViewModel();
    checkPaymentModel: CheckPaymentStautsViewModel = new CheckPaymentStautsViewModel();
    organizationId: number = parseInt(sessionStorage.getItem("id"));
    organizationIdForCheck: number = this.organizationId;
    hasAccountForDonate: boolean = false;
    donateAccounts: Array<DonateAccountViewModel> = new Array<DonateAccountViewModel>();
    targets: Array<TargetViewModel> = new Array<TargetViewModel>();
    currencies: Array<CurrencyViewModel> = new Array<CurrencyViewModel>();
    accountForDonation: DonateAccountViewModel = new DonateAccountViewModel();
    merchantPassword: string = "test";
    donateAmount: string ;
    donateOrganization: string;
    isApproved: boolean = false;
    user: any;
    merchantData: MerchantDataViewModel = new MerchantDataViewModel();
    paymentInfo: MerchantDataViewModel = new MerchantDataViewModel();
    currency: CurrencyViewModel = new CurrencyViewModel();
    target: TargetViewModel = new TargetViewModel();
    donate: DonateViewModel = new DonateViewModel();
    messages: DonateMessages = new DonateMessages();
    message: string;
    @ViewChild("gratitude")
    public modal: ModalComponent;

    public donateForm: FormGroup;
    public submitted: boolean; 

    public formErrors = {
        'amount': '',
        'currency': '',        
        'target': '',
        'accountForDonation': ''
    }

    public errorMessages = {
        'amount': {
            'required': "Поле є обов'язковим для заповнення", 
            'pattern': "Поле може містити цифри та лише два знаки після коми"
        }, 
        'currency': {
            'required': 'Будь ласка, оберіть значення з випадаючого списку'
        }, 
        'target': {
            'required': 'Будь ласка, оберіть значення з випадаючого списку'
        }, 
        'accountForDonation':{
            'required': 'Будь ласка, оберіть значення з випадаючого списку'
        }

    }

    constructor(private _donateService: DonateService, private _fb: FormBuilder,
        private _storageService: StorageService) {

    }


    ngOnInit() {

        this._storageService.showDropDown = false;

        if (localStorage.getItem("order_id")) {
            this.paymentInfo = JSON.parse(localStorage.getItem("merchantData"));
            this.checkPaymentModel.merchant_id = parseInt(this.paymentInfo.merchantId);
            this.checkPaymentModel.order_id = localStorage.getItem("order_id");
            this.checkPaymentModel.signature = this.create(this.checkPaymentModel);
            console.log(this.checkPaymentModel);
            this.checkPayment();
        }

        if (isBrowser) {
            if (localStorage.getItem(key.keyToken)) {
                this.user = JSON.parse(localStorage.getItem(key.keyModel)) as AuthorizeUserModel;
            }
        };

        this.donateForm = this._fb.group({
            amount: ['', [Validators.required, Validators.pattern("[0-9]+(\.[0-9]{1,2})?$")]],
            currency: ['', [Validators.required]],
            description: [''],
            target: ['', [Validators.required]],
            accountForDonation: ['', [Validators.required]], 
            email: ['']
        });

        this._donateService.getOrderId().subscribe((result) => this.fondyPayModel.order_id = result);
        this._donateService.getTargets().subscribe((result) => { this.targets = result; console.log(this.targets) });
        this._donateService.getCurrencies().subscribe((result) => { this.currencies = result, console.log(this.currencies) });
        this.fondyPayModel.server_callback_url = "http://localhost:51116/finance/donate";
        this.fondyPayModel.response_url = "http://fundtrack3.azurewebsites.net/finance/donate";
 

        this.donateForm.valueChanges.subscribe(() => {
            this.showErrorMessages();
                this.fondyPayModel.amount = this.getAppropriateAmount(this.donateForm.controls.amount.value);
                this.fondyPayModel.order_desc = this.donateForm.controls.description.value;
                this.fondyPayModel.signature = this.createSignature(this.fondyPayModel);
            }
        )        
    }

    ngDoCheck() {
        if (sessionStorage.getItem('id')) {
            this.organizationId = parseInt(sessionStorage.getItem('id'));
            console.log(this.organizationId);
            this.getAccountsForDonate();
            sessionStorage.removeItem('id');
            this.hasAccountForDonate = false;
        }
    }

    ngOnDestroy(): void {
        this._storageService.showDropDown = true;
    }

    createSignature(parameters: RequestFondyViewModel): string {
        let result: string;        
        result = this.merchantPassword + '|' + parameters.amount + '|' + parameters.currency + '|' + parameters.merchant_id
            + '|' + parameters.order_desc + '|' + parameters.order_id + '|'
            + parameters.response_url + '|' + parameters.server_callback_url;
        console.log(result);
        result = <string>sha1(result);
        console.log(result);
        return result;
    }

    create(parameters: CheckPaymentStautsViewModel): string {
        let result: string;
        let merchantPassword: string = "test";
        result = merchantPassword + '|' + parameters.merchant_id + '|' + parameters.order_id;
        result = <string>sha1(result);
        return result;
    }

    sendDonateRequest(): void {
        if (this.fondyPayModel.merchant_id == undefined) {

        }

        this.merchantData.merchantId = this.fondyPayModel.merchant_id.toString();

        this.merchantData.bankAccountId = this.accountForDonation.bankAccountId.toString();
        if (this.currency.currencyId == undefined) {
            this.showErrorsForSelect();
            return;
        }
        
        this.merchantData.currencyId = this.currency.currencyId.toString();
        this.fondyPayModel.currency = this.currency.currencyShortName;       
        this.fondyPayModel.signature = this.createSignature(this.fondyPayModel);

        if (this.target.targetId == undefined) {
            this.showErrorsForSelect();
            return;
        }

        this.merchantData.targetId = this.target.targetId.toString();  
        
        if (this.user) {
            this.merchantData.userId = this.user.id;
        };
        if (this.donateForm.valid) {
           
            if (this.fondyPayModel.order_desc = " ") {
                this.fondyPayModel.order_desc = "Благочинний внесок";
                this.fondyPayModel.signature = this.createSignature(this.fondyPayModel);
            }

            if (this.fondyPayModel.amount == NaN) {
                this.showErrorMessages;
                return;
            }

            this.merchantData.donateOrganization = this.donateOrganization;
            
            this.merchantData.orderDesc = this.fondyPayModel.order_desc;
            localStorage.setItem("order_id", this.fondyPayModel.order_id);
            localStorage.setItem("merchantData", JSON.stringify(this.merchantData));
            this._donateService.sendRequestToFondy(this.fondyPayModel).
                subscribe((response) => {
                    console.log(response);
                    window.location.href = response;
                });  
        }

        else {
            this.showErrorMessages();
            //this.showErrorsForSelect();            
        }
              
    }

    checkPayment(): void {
        this._donateService.checkPaymentRequest(this.checkPaymentModel).
            subscribe((response) => {
                console.log(response);
                if (response.response.order_status == "approved") {
                    this.donateAmount = parseFloat(response.response.actual_amount) / 100 + " " + response.response.currency;
                    this.paymentInfo = JSON.parse(localStorage.getItem("merchantData"));
                    this.donateOrganization = this.paymentInfo.donateOrganization;
                    this.message = this.messages.getApprovedMessage((parseFloat(response.response.actual_amount) / 100).toString(),
                        response.response.currency, this.paymentInfo.donateOrganization);
                    this.modal.show();
                    this.donate.amount = parseFloat(this.donateAmount);
                    this.donate.bankAccountId = parseInt(this.paymentInfo.bankAccountId);
                    this.donate.currencyId = parseInt(this.paymentInfo.currencyId);
                    this.donate.description = this.paymentInfo.orderDesc;
                    this.donate.donatorEmail = '';
                    this.donate.orderId = response.response.order_id;
                    this.donate.targetId = parseInt(this.paymentInfo.targetId);
                    this.donate.userId = parseInt(this.paymentInfo.userId);
                    this._donateService.addDonation(this.donate).subscribe();
                }

                else if (response.response.order_status == 'declined') {
                    this.message = this.messages.declinedPayment;
                    this.modal.show();
                }
                localStorage.removeItem("merchantData");
                localStorage.removeItem("order_id");
            });
        
    }

    getAccountsForDonate(): void {
        this._donateService.getAccountsForDonate(this.organizationId).
            subscribe((result) => {
                console.log(result);
                if (result.accounts.length != 0) {
                    this.donateOrganization = result.orgName;
                    this.hasAccountForDonate = true;
                    this.donateAccounts = result.accounts;
                    this.accountForDonation = result.accounts[0];
                }
            });
    }

    onSelectChange(selectedAccount): void {
        this.accountForDonation = selectedAccount;
        this.fondyPayModel.merchant_id = this.accountForDonation.merchantId;
        this.merchantPassword = this.accountForDonation.merchantPassword;
        this.fondyPayModel.signature = this.createSignature(this.fondyPayModel);
    }

    currencyChange(selectedCurrency): void {
        this.currency = selectedCurrency;
        this.fondyPayModel.currency = this.currency.currencyShortName;
    }

    targetChange(selectedTarget): void {
        this.target = selectedTarget;
    }

    getAppropriateAmount(value: string): number {
        let multiplier = 100;
        if (value.includes(',')) {
            value = value.replace(',', '.');
        }
        return Math.round((parseFloat(value) * multiplier));
    }

    showErrorMessages(): void {
        let form = this.donateForm;
        for (let field in this.formErrors) {
            this.formErrors[field] = "";
            let control = form.get(field);
            if (control && control.dirty && !control.valid) {
                let message = this.errorMessages[field];
                for (let key in control.errors) {
                    this.formErrors[field] += message[key] + " ";
                }
            }
        }
    }

    showErrorsForSelect(): void {
        let form = this.donateForm;
        for (let field in this.formErrors) {
            this.formErrors[field] = "";
            let control = form.get(field);
            if (control && control.untouched) {
                let message = this.errorMessages[field];
                for (let key in control.errors) {
                    this.formErrors[field] += message[key] + " ";
                }
            }
        }
    }
}