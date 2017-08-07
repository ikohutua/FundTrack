import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { OrgAccountService } from "../../services/concrete/finance/orgaccount.service";
import { OrgAccountViewModel } from "../../view-models/concrete/finance/orgaccount-viewmodel";
import { DecimalPipe } from '@angular/common';
import { CurrencyPipe } from '@angular/common';

@Component({
    selector: 'createorgaccount',
    templateUrl: './createorgaccount.component.html',
    styleUrls: ['./createorgaccount.component.css']
})
export class CreateOrgAccountComponent {
    private account: OrgAccountViewModel = new OrgAccountViewModel();
    private pageTitle: string = 'Створення рахунку організації';

    constructor(private _accountService: OrgAccountService) {
    }
    

}