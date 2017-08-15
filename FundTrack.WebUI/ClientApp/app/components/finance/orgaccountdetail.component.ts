import { Component, OnInit, Input, OnChanges, SimpleChange } from "@angular/core";
import { Router } from "@angular/router";
import { OrgAccountService } from "../../services/concrete/finance/orgaccount.service";
import { OrgAccountViewModel } from "../../view-models/concrete/finance/orgaccount-viewmodel";
import { DecimalPipe } from '@angular/common';
import { CurrencyPipe } from '@angular/common';


@Component({
    selector: 'orgaccountdetail',
    templateUrl: './orgaccountdetail.component.html',
    styleUrls: ['./orgaccountdetail.component.css']
})
export class OrgAccountDetailComponent implements OnInit, OnChanges {
    @Input('orgId') orgId: number;
    @Input() accountId: number;
    private currentDate = new Date().toJSON().slice(0, 10).replace(/-/g, '/');
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