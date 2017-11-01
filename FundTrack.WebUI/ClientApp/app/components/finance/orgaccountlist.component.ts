﻿import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { OrgAccountService } from "../../services/concrete/finance/orgaccount.service";
import { OrgAccountViewModel } from "../../view-models/concrete/finance/orgaccount-viewmodel";
import { DecimalPipe } from '@angular/common';
import { CurrencyPipe } from '@angular/common';
import * as key from '../../shared/key.storage';
import * as constant from '../../shared/default-configuration.storage';


@Component({
    selector: 'orgaccountlist',
    templateUrl: './orgaccountlist.component.html',
    styleUrls: ['./orgaccountlist.component.css']
})
export class OrgAccountListComponent implements OnInit {
    //Property that keeps an array of organization account
    private accounts: OrgAccountViewModel[] = new Array<OrgAccountViewModel>();
    //Property that indicates spinner display status
    private showSpinner: boolean = false;
    //Property that indicates currently selected account
    public selectAccountId: number = -1;
    //Property that indicates currently organization account
    public orgId: number = -1;
    //Property that keeps data of the selected account
    private selectedAccount: OrgAccountViewModel;
    //Property that keeps title for the page
    private pageTitle: string = 'Рахунки організації';
    private readonly bankType: string = constant.bankUA;
    private readonly cashType: string = constant.cashUA;
    //Property that keeps an array of organization account
    private bankAccounts: OrgAccountViewModel[] = new Array<OrgAccountViewModel>();
    //Property that keeps an array of organization account
    private cashAccounts: OrgAccountViewModel[] = new Array<OrgAccountViewModel>();

    isBankAccountAvailable: boolean;
    isCashAccountAvailable: boolean;

    isDownloaded: boolean;
    ifBankSelectedType: boolean;
    isExtractsMerchantEnable: boolean;

    constructor(private _accountService: OrgAccountService,
        private router: Router) {
    }

    /*
   Executes when component is initialized
   */
    ngOnInit(): void {
        this.showSpinner = true;
        this._accountService.getAllAccountsOfOrganization().
            subscribe(r => {
                this.accounts = r;
                this.filterAccounts();

                if (this.isBankAccountAvailable) {
                    this.setActiveAccount(this.bankAccounts[0]);
                }
                else if (this.isCashAccountAvailable) {
                    this.setActiveAccount(this.cashAccounts[0]);
                }
                this.isDownloaded = true;
                this.showSpinner = false;
            });
    }

    checkIfAccTypesIsAvailable() {
        this.isBankAccountAvailable = this.containItem(this.accounts, this.bankType);
        this.isCashAccountAvailable = this.containItem(this.accounts, this.cashType);
    }

    filterAccounts() {
        this.checkIfAccTypesIsAvailable();

        if (this.isBankAccountAvailable) {
            this.bankAccounts = this.getAccountsByType(this.bankType);
        }

        if (this.isCashAccountAvailable) {
            this.cashAccounts = this.getAccountsByType(this.cashType);
        }
    }

    containItem(arr: OrgAccountViewModel[], item: string): boolean {
        for (let i = 0; i < arr.length; i++) {
            if (arr[i].accountType == item) {
                return true;
            }
        }
        return false;
    }

    getAccountsByType(typeName: string): OrgAccountViewModel[] {
        var a = this.accounts.filter(oa => oa.accountType == typeName);
        return a;
    }

    /*
    Navigates to create account page
    */
    navigateToCreatePage() {
        this.router.navigate(['/finance/createaccount']);
    }

    /*
    Navigates to bank import page
    */
    navigateToBankImportPage() {
        this.router.navigate(['/finance/bank-import']);
    }
    /*
    Sets active account
    */
    private setActiveAccount(account: OrgAccountViewModel): void {
        this.isExtractsMerchantEnable = false;
        this.selectAccountId = account.id;
        this.orgId = account.orgId;
        this.selectedAccount = account;
        this.ifBankSelectedType = account.accountType === this.bankType ? true : false;
    }
    /*
    After account has been deleted - sets first account in the list as active and removes deleted account from the array
    */
    private onDelete(accountNumber: number) {
        this.accounts.splice(this.accounts.findIndex(o => o.id == accountNumber), 1);
        this.setActiveAccount(this.accounts[0]);
        this.filterAccounts();
    }

    private checkIfAccountHasCard(account: OrgAccountViewModel): boolean {
        if (account.cardNumber == '' || account.cardNumber == null) {
            return false;
        }
        else {
            return true;
        }
    }

    onExtractEnableChange(event: boolean) {
        this.isExtractsMerchantEnable = event;
    }
}