import { Component, OnInit, Input, OnChanges, SimpleChange } from "@angular/core";
import { Router } from "@angular/router";
import { OrgAccountService } from "../../services/concrete/finance/orgaccount.service";
import { OrgAccountViewModel } from "../../view-models/concrete/finance/orgaccount-viewmodel";
import { DecimalPipe } from '@angular/common';
import { CurrencyPipe } from '@angular/common';


@Component({
    selector: 'orgaccountoperation',
    templateUrl: './orgaccountoperation.component.html',
    styleUrls: ['./orgaccountoperation.component.css']
})
export class OrgAccountOperationComponent implements OnInit, OnChanges {
    @Input('orgId') orgId: number;
    @Input() accountId: number;
    private account: OrgAccountViewModel = new OrgAccountViewModel();
    constructor(private _service: OrgAccountService)
    {

    }
    ngOnInit(): void {
        this._service.getOrganizationAccountById(this.accountId)
            .subscribe(a => {
                this.account = a;
            });
    }
    ngOnChanges(changes: { [propKey: string]: SimpleChange }) {
        if (changes['accountId'] && changes['accountId'] != changes['accountId'].currentValue) {
            this._service.getOrganizationAccountById(this.accountId)
                .subscribe(a => {
                    this.account = a;
                });
        }
    }
}