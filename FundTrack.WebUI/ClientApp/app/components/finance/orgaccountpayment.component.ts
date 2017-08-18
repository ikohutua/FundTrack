import { Component, OnInit, Input, OnChanges, SimpleChange } from "@angular/core";
import { Router } from "@angular/router";
import { OrgAccountService } from "../../services/concrete/finance/orgaccount.service";
import { OrgAccountViewModel } from "../../view-models/concrete/finance/orgaccount-viewmodel";
import { DecimalPipe } from '@angular/common';
import { CurrencyPipe } from '@angular/common';


@Component({
    selector: 'orgaccountpayment',
    templateUrl: './orgaccountpayment.component.html',
    styleUrls: ['./orgaccountpayment.component.css']
})
export class OrgAccountPaymentComponent implements OnChanges {
    @Input('orgId') orgId: number;
    @Input('accountId') accountId: number;

    /*
    Checks for value changes and assignes new account in the component
    */
    ngOnChanges(changes: { [propKey: string]: SimpleChange }) {
        if (changes['accountId'] && changes['accountId'] != changes['accountId'].currentValue) {
            
        }
    }
}