import { Component, OnInit, Input, SimpleChange, OnChanges } from "@angular/core";
import { Router } from "@angular/router";
import { OrgAccountService } from "../../services/concrete/finance/orgaccount.service";
import { OrgAccountViewModel } from "../../view-models/concrete/finance/orgaccount-viewmodel";
import { DecimalPipe } from '@angular/common';
import { CurrencyPipe } from '@angular/common';
import { FinOpService } from "../../services/concrete/finance/finOp.service";
import { FinOpListViewModel } from "../../view-models/concrete/finance/finop-list-viewmodel";
import { isBrowser } from "angular2-universal";


@Component({
    selector: 'orgaccountoperation',
    templateUrl: './orgaccountoperation.component.html',
    styleUrls: ['./orgaccountoperation.component.css']
})
export class OrgAccountOperationComponent implements OnChanges {
    @Input('orgId') orgId: number;
    @Input() accountId: number = -1;
    private finOps: FinOpListViewModel[] = new Array<FinOpListViewModel>();
    private currentDate = new Date().toJSON().slice(0, 10).replace(/-/g, '/');
    private operations = ['Payment', 'Withdrawn', 'Income', 'etc.'];

    constructor(private _router: Router,
        private _finOpService: FinOpService
                                            ) {
      
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
            if (this.accountId!=-1) {
                this._finOpService.getFinOpsByOrgAccountId(this.accountId)
                    .subscribe(a => {
                        this.finOps = a;
                    })
            }
        }
    }
    private convertDate(date: Date): string {
        var options = {
            weekday: "long", year: "numeric", month: "short",
            day: "numeric", hour: "2-digit", minute: "2-digit"
        };
        return date.toLocaleString("uk-UK", options);
    }
}