import { Component, OnInit, Input, Output, OnChanges, SimpleChange, ViewChild, EventEmitter } from "@angular/core";
import { Router } from "@angular/router";
import { OrgAccountService } from "../../services/concrete/finance/orgaccount.service";
import { OrgAccountViewModel } from "../../view-models/concrete/finance/orgaccount-viewmodel";
import { DecimalPipe } from '@angular/common';
import { CurrencyPipe } from '@angular/common';
import { BankCredentialsViewModel } from "../../view-models/concrete/finance/donate-credentials.view-model";
import { ModalComponent } from '../../shared/components/modal/modal-component';
import * as message from '../../shared/common-message.storage';
import { FormGroup, FormControl } from "@angular/forms";

@Component({
    selector: 'org-account-extracts',
    templateUrl: './org-account-extracts.component.html',
    styleUrls: ['./org-account-extracts.component.css'],
    providers: [OrgAccountService]
})
export class OrgAccountExtractsComponent implements OnChanges {
    @Input('accountId') accountId: number = -1;
    isExtractsEnable: boolean;
    isExtractsConnected: boolean;
    @Output() getIsExtractEnable = new EventEmitter<boolean>();

    extractsCredentials: BankCredentialsViewModel = new BankCredentialsViewModel();
    connectExtractsCredential: BankCredentialsViewModel = new BankCredentialsViewModel();
    errorMessage: string;
    bankAccountId: number;
    @ViewChild('disable') disableModal: ModalComponent;

    lengthNotZero: boolean = this.connectExtractsCredential.merchantId > 0;

    constructor(private _orgAccountService: OrgAccountService) {
    }

    ngOnChanges(changes: { [propKey: string]: SimpleChange }) {
        this.connectExtractsCredential = new BankCredentialsViewModel();
        if (changes['accountId'] && changes['accountId'] != changes['accountId'].currentValue) {
            if (this.accountId >= 0) {
                this.errorMessage = null;
                this._orgAccountService.getBankAccId(this.accountId)
                    .subscribe((r) => {
                        this.bankAccountId = r;
                    });
                this._orgAccountService.checkExtractsStatus(this.accountId)
                    .subscribe(
                    res => {
                        this.isExtractsConnected = res;
                        if (this.isExtractsConnected) {
                            this._orgAccountService.getExtractsCredentials(this.accountId)
                                .subscribe((res) => {
                                    this.extractsCredentials = res;
                                })
                            this._orgAccountService.checkExtractsEnable(this.accountId)
                                .subscribe((res) => {
                                    this.isExtractsEnable = res;
                                    this.emitIsExtractEnable();
                                })
                        }
                    },
                    error => {
                        this.isExtractsConnected = false;
                        this.errorMessage = message.uncorrectAccountType;
                        this.emitIsExtractEnable();
                    })
            }
        }
    }

    /**
     * Attach data to get extracts
     */
    connectExtracts() {
        this._orgAccountService.getBankAccId(this.accountId)
            .subscribe((r) => {
                this.connectExtractsCredential.bankAccountId = r;
                this._orgAccountService.connectExtracts(this.connectExtractsCredential)
                    .subscribe((r) => {
                        this.extractsCredentials = r;
                        this.isExtractsConnected = true;
                        this.isExtractsEnable = true;
                        this.emitIsExtractEnable();
                    },
                    (error) => {
                        alert(error);
                    });
            });
    }

    /**
     *Stop the opportunity to get extracts
     */
    toggleExtracts() {
        this._orgAccountService.toggleExtracts(this.accountId)
            .subscribe((res) => {
                this.isExtractsEnable = res;
                this.emitIsExtractEnable();
            });
    }

    /**
     * Delete current MerchantId & Password. Getting extracts won't be available
     */
    disableExtracts() {
        this._orgAccountService.getBankAccId(this.accountId)
            .subscribe((r) => {
                this.connectExtractsCredential.bankAccountId = r;
                this._orgAccountService.disableExtracts(r)
                    .subscribe((r) => {
                        this.connectExtractsCredential = r;
                        this.isExtractsConnected = false;
                        this.isExtractsEnable = false;
                        this.disableModal.hide();
                        this.emitIsExtractEnable();
                    });
            });
    }

    private emitIsExtractEnable() {
        this.getIsExtractEnable.emit(this.isExtractsEnable);
    }
}