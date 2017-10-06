import { Component, OnInit, Input, OnChanges, SimpleChange, ViewChild } from "@angular/core";
import { Router } from "@angular/router";
import { OrgAccountService } from "../../services/concrete/finance/orgaccount.service";
import { OrgAccountViewModel } from "../../view-models/concrete/finance/orgaccount-viewmodel";
import { DecimalPipe } from '@angular/common';
import { CurrencyPipe } from '@angular/common';
import { DonateCredentialsViewModel } from "../../view-models/concrete/finance/donate-credentials.view-model";
import { ModalComponent } from '../../shared/components/modal/modal-component';

@Component({
    selector: 'orgaccountpayment',
    templateUrl: './orgaccountpayment.component.html',
    styleUrls: ['./orgaccountpayment.component.css'], 
    providers: [OrgAccountService]
})
export class OrgAccountPaymentComponent implements OnChanges , OnInit{
    @Input('orgId') orgId: number;
    @Input('accountId') accountId: number = -1;
    isDonationConnected: boolean = false;
    isDonationEnabled: boolean = false;
    donateCredentials: DonateCredentialsViewModel = new DonateCredentialsViewModel();
    connectDonateCredential: DonateCredentialsViewModel = new DonateCredentialsViewModel();
    errorMessage: string;
    bankAccountId: number;
    @ViewChild('disable') disableModal: ModalComponent;   

    constructor(private _orgAccountService: OrgAccountService) {

    }

    /*
    Checks for value changes and assignes new account in the component
    */

    ngOnInit() {
        
    }

    ngOnChanges(changes: { [propKey: string]: SimpleChange }) {
        if (changes['accountId'] && changes['accountId'] != changes['accountId'].currentValue) {
            if (this.accountId!=(-1)) {
                this.errorMessage = null;
                this._orgAccountService.getBankAccId(this.accountId)
                    .subscribe((r) => {
                        this.bankAccountId = r;
                    });
                this._orgAccountService.checkDonateStatus(this.accountId)
                    .subscribe(
                    res => {
                        this.isDonationConnected = res;

                        if (this.isDonationConnected) {
                            this._orgAccountService.getDonateCredentials(this.accountId)
                                .subscribe((res) => {
                                    this.donateCredentials = res;
                                })
                            this._orgAccountService.checkDonateEnable(this.accountId)
                                .subscribe((res) => {
                                    this.isDonationEnabled = res;
                                })

                        }
                    },
                    error => {
                        this.isDonationConnected = false;
                        this.errorMessage = "Некоректний тип рахунку";
                    })
            }
        }
    }

    toggleDonate() {
        this._orgAccountService.toggleDonate(this.accountId)
            .subscribe((res) => {
                this.isDonationEnabled = res;
            },
            (error) => {
            });
    }

    connectDonation() {
        this._orgAccountService.getBankAccId(this.accountId)
            .subscribe((r) => {
                this.connectDonateCredential.bankAccountId = r;
                this._orgAccountService.connectDonation(this.connectDonateCredential)
                    .subscribe((r) => {
                        this.donateCredentials = r;
                        this.isDonationConnected = true;
                        this.isDonationEnabled = true;
                    });
            });      
    }

    disableDonation() {
        this._orgAccountService.getBankAccId(this.accountId)
            .subscribe((r) => {
                this.connectDonateCredential.bankAccountId = r;
                this._orgAccountService.disableDonation(r)
                    .subscribe((r) => {
                        this.connectDonateCredential = r;
                        this.isDonationConnected = false;
                        this.isDonationEnabled = false;
                        this.disableModal.hide();
                    });
            });    
    }

}