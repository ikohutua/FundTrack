import { Component, OnInit } from "@angular/core";
import { isBrowser } from "angular2-universal";
import * as key from '../../shared/key.storage';
import { AuthorizeUserModel } from "../../view-models/concrete/authorized-user-info-view.model";
import { OrgAccountService } from "../../services/concrete/finance/orgaccount.service";
import { ImportIntervalViewModel } from "../../view-models/concrete/import-interval-view-model";
import * as constant from '../../shared/default-configuration.storage';
import { BankImportService } from "../../services/concrete/bank-import.service";
import { AutoImportIntervalViewModel } from "../../view-models/concrete/autoimport-interval-view-model";


@Component({
    selector: 'additional-options',
    templateUrl: './additional-options.component.html',
    styleUrls: ['./additional-options.component.css'],
    providers: [OrgAccountService, BankImportService]
})

export class AdditionalOptionsComponent implements OnInit {
    private user: AuthorizeUserModel = new AuthorizeUserModel();
    private showSpinner: boolean = true;
    private isBankAccountsAvailable: boolean;
    private intervals = constant.intervals;
    private model: AutoImportIntervalViewModel;
    constructor(private _service: OrgAccountService,
        private _importService: BankImportService) { }


    ngOnInit() {
        this.showSpinner = true;
        if (isBrowser) {
            if (localStorage.getItem(key.keyToken)) {
                debugger;
                this.user = JSON.parse(localStorage.getItem(key.keyModel)) as AuthorizeUserModel;
            }
            this._service.getBankAccountsAvailable(this.user.orgId)
                .subscribe(response => {
                    this.isBankAccountsAvailable = response;
                    this.showSpinner = false;
                });
        };
    }
    private onChangeSelection($event): void {
        debugger;
        this.model = new AutoImportIntervalViewModel();
        this.model.orgId = this.user.orgId;
        this.model.interval = +$event;
        this._importService.updateInterval(this.model).subscribe();
    }

}