import { Component } from "@angular/core";
import { Router } from "@angular/router";
import { OrgAccountService } from "../../services/concrete/finance/orgaccount.service";
import { OrgAccountViewModel } from "../../view-models/concrete/finance/orgaccount-viewmodel";
import { DecimalPipe } from '@angular/common';
import { CurrencyPipe } from '@angular/common';
import { FormControl, FormGroup, FormBuilder, Validators } from '@angular/forms';
import { CustomValidators } from 'ng2-validation';

@Component({
    selector: 'createorgaccount',
    templateUrl: './createorgaccount.component.html',
    styleUrls: ['./createorgaccount.component.css']
})
export class CreateOrgAccountComponent {
    private account: OrgAccountViewModel = new OrgAccountViewModel();
    private pageTitle: string = 'Створення рахунку організації';
    accountForm: FormGroup;

    constructor(private _accountService: OrgAccountService,
                private _fb: FormBuilder
    ) {
        this.createForm();
    }
    private createForm(): void {
        this.accountForm = this._fb.group({
            accountName: ['', [Validators.required, Validators.maxLength(100)]],
            accountNumber: ['', [Validators.required, Validators.maxLength(20)]],
            accountMfo: ['', [Validators.required]],
            accountEdrpou: ['', [Validators.required]],
            accountBankName: ['', [Validators.required, Validators.maxLength(50)]],
            accountDescription: ['', [Validators.required, Validators.maxLength(200)]],
            accountCurrency: ['', Validators.required]
        });
    }

}