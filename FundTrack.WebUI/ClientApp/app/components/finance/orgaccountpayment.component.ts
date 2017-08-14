import { Component, OnInit, Input } from "@angular/core";
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
export class OrgAccountPaymentComponent {
    @Input('orgId') orgId: number;
}