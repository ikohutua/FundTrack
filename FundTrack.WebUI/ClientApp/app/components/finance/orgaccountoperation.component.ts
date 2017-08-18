import { Component, OnInit, Input, SimpleChange, OnChanges } from "@angular/core";
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
export class OrgAccountOperationComponent implements OnChanges {
    @Input('orgId') orgId: number;
    @Input() accountId: number;

    constructor(private _router: Router) {

    }
    private navigateToImportsPage(): void {
        this._router.navigate(['/finance/bank-import']);
    }

    /*
    Checks for value changes and assignes new account in the component
    */
    ngOnChanges(changes: { [propKey: string]: SimpleChange }) {
        if (changes['accountId'] && changes['accountId'] != changes['accountId'].currentValue) {
            //code to execute when property changes
        }
    }

}