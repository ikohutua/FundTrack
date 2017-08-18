import { Component, OnInit, Input } from "@angular/core";
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
export class OrgAccountOperationComponent {
    @Input('orgId') orgId: number;

    constructor(private _router: Router) {

    }
    private navigateToImportsPage(): void {
        this._router.navigate(['/finance/bank-import']);
    }

}