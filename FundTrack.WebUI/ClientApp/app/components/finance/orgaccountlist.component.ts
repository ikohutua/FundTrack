import { Component, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { OrgAccountService } from "../../services/concrete/finance/orgaccount.service";
import { OrgAccountViewModel } from "../../view-models/concrete/finance/orgaccount-viewmodel";

@Component({
    selector: 'orgaccountlist',
    templateUrl: './orgaccountlist.component.html',
    styleUrls: ['./orgaccountlist.component.css']
})
export class OrgAccountListComponent implements OnInit{
    private accounts: OrgAccountViewModel[] = new Array<OrgAccountViewModel>();

    constructor(private _accountService: OrgAccountService) {
    }
    ngOnInit(): void {
        this._accountService.getAllAccountsOfOrganization().
            subscribe(r => {
                this.accounts = r;
    });
    }
   
}