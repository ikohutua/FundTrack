import { Component, OnInit, Input, Output, OnChanges, SimpleChange, ViewChild, EventEmitter } from "@angular/core";
import { Router } from "@angular/router";
import { OrgAccountService } from "../../services/concrete/finance/orgaccount.service";
import { OrgAccountViewModel } from "../../view-models/concrete/finance/orgaccount-viewmodel";
import { DecimalPipe } from '@angular/common';
import { CurrencyPipe } from '@angular/common';
import { DonateCredentialsViewModel } from "../../view-models/concrete/finance/donate-credentials.view-model";
import { ModalComponent } from '../../shared/components/modal/modal-component';

@Component({
    selector: 'org-account-extracts',
    templateUrl: './org-account-extracts.component.html',
    styleUrls: ['./org-account-extracts.component.css'],
    providers: [OrgAccountService]
})
export class OrgAccountExtractsComponent implements OnChanges, OnInit {
    @Input('orgId') orgId: number;
    @Input('accountId') accountId: number = -1;
    public isExtractsEnable: boolean = false;
    @Output() getIsExtractEnable = new EventEmitter<boolean>();


    extractsCredentials: DonateCredentialsViewModel = new DonateCredentialsViewModel();
    connectExtractsCredential: DonateCredentialsViewModel = new DonateCredentialsViewModel();
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
        console.log("Org-Accounts-Extracts");
        if (changes['accountId'] && changes['accountId'] != changes['accountId'].currentValue) {
            if (this.accountId != (-1)) {
                this.errorMessage = null;
                this._orgAccountService.getBankAccId(this.accountId)
                    .subscribe((r) => {
                        this.bankAccountId = r;
                    });
                this._orgAccountService.checkExtractsStatus(this.accountId)
                    .subscribe(
                    res => {
                        this.isExtractsEnable = res;
                        if (this.isExtractsEnable) {
                            this._orgAccountService.getExtractsCredentials(this.accountId)
                                .subscribe((res) => {
                                    this.extractsCredentials = res;
                                })
                            this.getIsExtractEnable.emit(this.isExtractsEnable);
                        }
                    },
                    error => {
                        this.isExtractsEnable = false;
                        this.errorMessage = "Некоректний тип рахунку";
                        this.getIsExtractEnable.emit(this.isExtractsEnable);

                    })


            }
        }
    }

    connectExtracts() {
        this._orgAccountService.getBankAccId(this.accountId)
            .subscribe((r) => {
                this.connectExtractsCredential.bankAccountId = r;
                this._orgAccountService.connectExtracts(this.connectExtractsCredential)
                    .subscribe((r) => {
                        this.extractsCredentials = r;
                        this.isExtractsEnable = true;
                    });
            });
    }
}