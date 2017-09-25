﻿import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { OrgAccountService } from "../../services/concrete/finance/orgaccount.service";
import { OrgAccountViewModel } from "../../view-models/concrete/finance/orgaccount-viewmodel";
import { DecimalPipe } from '@angular/common';
import { CurrencyPipe } from '@angular/common';


@Component({
    selector: 'orgaccountlist',
    templateUrl: './orgaccountlist.component.html',
    styleUrls: ['./orgaccountlist.component.css']
})
export class OrgAccountListComponent implements OnInit {

    //Property that keeps an array of organization account
    private accounts: OrgAccountViewModel[] = new Array<OrgAccountViewModel>();
    //Property that keeps an array of cash organization account
    private cashAccounts: OrgAccountViewModel[] = new Array<OrgAccountViewModel>();
    //Property that indicates spinner display status
    //Property that keeps an array of cash organization account
    private bankAccounts: OrgAccountViewModel[] = new Array<OrgAccountViewModel>();
    //Property that indicates spinner display status
    private showSpinner: boolean = false;
    //Property that indicates currently selected account
    public selectAccountId: number = 1;
    //Property that keeps data of the selected account
    private selectedAccount: OrgAccountViewModel;
    //Property that keeps title for the page
    private pageTitle: string = 'Рахунки організації';

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
                this.cashAccounts = this.accounts.filter(cash => cash.accountType == "Готівка");
                this.bankAccounts = this.accounts.filter(bank => bank.accountType == "Банк");
                console.log(this.cashAccounts);
                console.log(this.bankAccounts);
                this.setActiveAccount(this.cashAccounts[0]);
                this.showSpinner = false;
    });
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
        this.selectAccountId = account.id;
        this.selectedAccount = account;
    }
    /*
    After account has been deleted - sets first account in the list as active and removes deleted account from the array
    */
    private onDelete(accountNumber: number) {
        this.accounts.splice(this.accounts.findIndex(o => o.id == accountNumber), 1);
        this.setActiveAccount(this.accounts[0]);
    }

    private checkIfAccountHasCard(account: OrgAccountViewModel): boolean {
        if (account.cardNumber == '' || account.cardNumber==null) {
            return false;
        }
        else {
            return true;
        }
    }
}