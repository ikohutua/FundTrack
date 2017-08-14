import { Component, OnInit, Input } from "@angular/core";
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
export class OrgAccountDetailComponent {
    @Input('orgId') orgId: number;
}