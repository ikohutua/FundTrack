import { Component, Input, ViewChild, Output, OnInit } from "@angular/core";
import { PrivatSessionViewModel } from "../../view-models/concrete/privat-session-view.model";
import { DataRequestPrivatViewModel } from "../../view-models/concrete/data-request-privat-view.model";
import { ModalComponent } from "../../shared/components/modal/modal-component";
import { BankImportService } from "../../services/concrete/bank-import.service";
import { ImportDetailPrivatViewModel, ImportPrivatViewModel } from "../../view-models/concrete/import-privat-view.model";
import { BankImportSearchViewModel } from "../../view-models/concrete/finance/bank-import-search-view.model";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { ValidatorsService } from "../../services/concrete/validators/validator.service";
import { FinOpFromBankViewModel } from "../../view-models/concrete/finance/finOpFromBank-view.model";
import { isBrowser } from "angular2-universal";
import { FinOpService } from "../../services/concrete/finance/finOp.service";
import { TargetViewModel } from "../../view-models/concrete/finance/donate/target.view-model";
import { OrgAccountSelectViewModel } from "../../view-models/concrete/finance/org-accounts-select-view.model";
import * as key from '../../shared/key.storage'
import * as message from '../../shared/common-message.storage'
import { AuthorizeUserModel } from "../../view-models/concrete/authorized-user-info-view.model";
import { SpinnerComponent } from "../../shared/components/spinner/spinner.component";
import { OrgAccountService } from "../../services/concrete/finance/orgaccount.service";
import { Location } from '@angular/common';


@Component({
    selector: 'bank-import',
    template: require('./bank-import.component.html'),
    styles: [require('./bank-import.component.css')],
    providers: [BankImportService, FinOpService, OrgAccountService]
})

export class BankImportComponent implements OnInit {
    //form for reactive validation
    public bankImportForm: FormGroup;
    //card which belong to select org account
    public card: string;
    //id merchant to make request to privar24
    private idMerchant: number;
    //password to make request to privar24
    private password: string;
    //data which sent in request to privat24
    private dataForPrivat: DataRequestPrivatViewModel = new DataRequestPrivatViewModel();

    //modal window to get bankImports
    @ViewChild("newBankImport")
    public newBankImportModalWindow: ModalComponent;
    //modal window to create finOp
    @ViewChild("finOp")
    public finOpModalWindow: ModalComponent;
    @ViewChild(SpinnerComponent) spinner: SpinnerComponent;
    @ViewChild("warning")
    public finOpWarningWindow: ModalComponent;


    //model which contain data to create new finOp
    private _newFinOp: FinOpFromBankViewModel = new FinOpFromBankViewModel();
    //data which was received from privat24
    private importData: ImportPrivatViewModel = new ImportPrivatViewModel();
    //array which contain register bank imports in db
    private _dataForFinOp: ImportDetailPrivatViewModel[] = new Array<ImportDetailPrivatViewModel>();
    //model for filtering bank imports
    private _bankSearchModel: BankImportSearchViewModel = new BankImportSearchViewModel();
    //strings for contain date
    @Input() dataPrivatFrom: string;
    @Input() dataPrivatTo: string;

    //array which contains targets to create new finOp
    private targets: TargetViewModel[] = new Array<TargetViewModel>();
    //model which contain select orgaccount(name,number,id)
    private currentOrgAccount: OrgAccountSelectViewModel = new OrgAccountSelectViewModel();
    private currentOrgAccountNumber: string;

    //current user in system
    private user: AuthorizeUserModel = new AuthorizeUserModel();
    //index selected bank import in table
    private index: number;
    //count bank imports in selected orgAccounts
    private count: number;
    private orgaccountId: number;
    private isOrgAccountHaveTarget: boolean;
    private showSpinner: boolean = false;
    private lastPrivatUpdate: Date;

    //constructor
    public constructor(private _service: BankImportService,
        private _finOpService: FinOpService,
        private _fb: FormBuilder,
        private _validatorsService: ValidatorsService,
        private _orgAccountService: OrgAccountService,
        private _location: Location) {
        this.createForm();
    }

    private createForm(): void {
        this.bankImportForm = this._fb.group({
            cardNumber: [this.dataForPrivat.card, [Validators.required, Validators.maxLength(16), Validators.minLength(16), this._validatorsService.isInteger]],
            dataTo: [this.dataForPrivat.dataTo, Validators.required],
            dataFrom: [this.dataForPrivat.dataFrom, Validators.required]
        });
        this.bankImportForm.valueChanges
            .subscribe(a => this.onValueChange(a));
        this.onValueChange();
    }

    ngOnInit() {
        this.showSpinner = true;
        if (isBrowser) {
            if (sessionStorage.getItem(key.keyCardNumber)) {
                this.card = sessionStorage.getItem(key.keyCardNumber);
                this._service.getCountExtractsOnCard(this.card)
                    .subscribe(response => {
                        this.count = response;
                        if (localStorage.getItem(key.keyToken)) {
                            this.user = JSON.parse(localStorage.getItem(key.keyModel)) as AuthorizeUserModel;
                            this._orgAccountService.getAllBaseTargetsOfOrganization(this.user.orgId)
                                .subscribe(response => this.targets = response);
                            this._service.getLastPrivatUpdate(this.user.orgId)
                                .subscribe(response => this.lastPrivatUpdate = response);
                            this._finOpService.getOrgAccountForFinOp(this.user.orgId, this.card)
                                .subscribe(response => {
                                    this.currentOrgAccount = response;
                                    if (this.currentOrgAccount.targetId != null) {
                                        this.isOrgAccountHaveTarget = true;
                                        this._orgAccountService.getTargetById(this.currentOrgAccount.targetId)
                                            .subscribe(response => {
                                                this.targets = new Array<TargetViewModel>();
                                                this.targets[0] = response;
                                            });
                                        this._orgAccountService.getExtractsCredentials(this.currentOrgAccount.id)
                                            .subscribe((res) => {
                                                this.idMerchant = res.merchantId;
                                                this.password = res.merchantPassword;
                                            });
                                    }
                                    this.showSpinner = false;
                                });
                            this.getAllExtracts();
                        }

                    });
            }
        }
    }

    private getAllExtracts() {
        this._service.getAllExtracts(this.card, this.spinner)
            .subscribe(response => {
                this._dataForFinOp = response;
            });
    }

    /*
    Errors to be displayed on the UI
    */
    private formErrors = {
        cardNumber: "",
        idMerchant: "",
        password: "",
        dataTo: "",
        dataFrom: ""
    };

    /*
    Error messages
    */
    private requiredMessage = message.requiredField;
    private numberMessage = message.requiredNumberFielsd;
    private lengthMessage = message.wrongLength;
    private cardMessage = message.invalidCardNumberMessage;

    private validationMessages = {
        cardNumber: {
            required: this.requiredMessage,
            notnumeric: this.numberMessage,
            maxlength: this.cardMessage,
            minlength: this.cardMessage,
        },
        idMerchant: {
            required: this.requiredMessage,
            notnumeric: this.numberMessage,
            minlength: this.lengthMessage,
        },
        password: {
            required: this.requiredMessage,
        },
        dataTo: {
            required: this.requiredMessage,
        },
        dataFrom: {
            required: this.requiredMessage,
        }
    }

    /**
     * Subscriber on value changes
     * @param data
     */
    private onValueChange(data?: any) {
        if (!this.bankImportForm) return;
        let form = this.bankImportForm;

        for (let field in this.formErrors) {
            this.formErrors[field] = "";
            let control = form.get(field);

            if (control && control.dirty && !control.valid) {
                let message = this.validationMessages[field];
                for (let key in control.errors) {
                    this.formErrors[field] = message[key.toLowerCase()];
                }
            }
        }
    }

    /**
     * get bankImports from privat24
     */
    public getExtracts() {
        this.dataForPrivat.idMerchant = this.idMerchant;
        this.dataForPrivat.password = this.password;
        this.dataForPrivat.dataTo = this.dataPrivatTo.split('-').reverse().join('.');
        this.dataForPrivat.dataFrom = this.dataPrivatFrom.split('-').reverse().join('.');
        this._service.getPrivatExtracts(this.dataForPrivat)
            .subscribe(() => {
                this._service.UpdateDate(this.user.orgId)
                    .subscribe(response => {
                        this.lastPrivatUpdate = response;
                    });

            });
    }

    //filter bank Imports
    public searchBankImport() {
        this._bankSearchModel.card = this.card;
        if (!this._bankSearchModel.state) {
            this._bankSearchModel.state == null;
        }
        this._service.getRawExtracts(this._bankSearchModel)
            .subscribe(response => {
                this._dataForFinOp = response;
            })
    }

    //initialize data for new finOp
    public createFinOp(bankImport: ImportDetailPrivatViewModel) {
        this._newFinOp.description = bankImport.description;
        this._newFinOp.bankImportId = bankImport.id;
        this._newFinOp.amount = +bankImport.cardAmount.split(' ')[0];
        this._newFinOp.absoluteAmount = Math.abs(this._newFinOp.amount);
        if (this._newFinOp.amount > 0) {
            this._newFinOp.cardToId = Number(this.currentOrgAccount.id);
        }
        if (this._newFinOp.amount < 0) {
            this._newFinOp.cardFromId = Number(this.currentOrgAccount.id);
        }
        this._newFinOp.orgId = this.user.orgId;
        this._newFinOp.targetId = this.targets[0].targetId;
        this.index = this._dataForFinOp.findIndex(element => element.id == bankImport.id);
        this.currentOrgAccountNumber = this.currentOrgAccount.orgAccountName + ': ' + this.currentOrgAccount.orgAccountNumber;
    }

    //save new initialize finOp
    public saveFinOp() {
        this._finOpService.createFinOp(this._newFinOp, this.spinner)
            .subscribe(response => {
                this.showToast();
                setTimeout(() => {
                    this.closeFinOpModal();
                    this._dataForFinOp[this.index].isLooked = true;
                }, 2500);
            });
    }



    /**
    * Closes bankImports modal window
    */
    public closeModal(): void {
        this.newBankImportModalWindow.hide();
    }

    /**
     * Open bankImports modal window
     */
    public onActionClick(): void {
        this.dataForPrivat.card = this.card;

        this.newBankImportModalWindow.show();
    }

    public onIncomeClick(): void {
        if (this.currentOrgAccount.targetId != undefined) {
            for (let bankDetail of this._dataForFinOp) {
                if (bankDetail.isLooked == false) {
                    if (Number(bankDetail.cardAmount.split(' ')[0]) > 0) {
                        this.createFinOp(bankDetail);
                        this.saveFinOp();
                    }
                }
            }
            this.getAllExtracts()
        }
        this.closeWarningModal();
    }

    public warningWindowShow(): void {
        this.finOpWarningWindow.show();
    }


    public UpdateDate(): void {
        this._service.UpdateDate
    }
    public onPrivatClick(): void {
        this._service.getUserExtracts(this.currentOrgAccount.id).subscribe(() => {
            this.showSpinner = false;
            this._service.UpdateDate(this.user.orgId)
                .subscribe(response => {
                    this.lastPrivatUpdate = response;
                });
            this.getAllExtracts();
        });
        this.showSpinner = true;
        this.newBankImportModalWindow.hide();
    }
    /**
     * close finOp modal window
     */
    public closeFinOpModal(): void {
        this.finOpModalWindow.hide();
        this._newFinOp = new FinOpFromBankViewModel();
    }

    public closeWarningModal(): void {
        this.finOpWarningWindow.hide();
    }
    /**
     * open finOp modal window
     */
    public openFinOpModal(bankImport: ImportDetailPrivatViewModel): void {
        this.createFinOp(bankImport);
        this.finOpModalWindow.show();
    }

    /*
    Displays toast, that pops up when account is successfuly created
    */
    public showToast() {
        var x = document.getElementById("snackbar")
        x.className = "show";
        setTimeout(function () { x.className = x.className.replace("show", ""); }, 3000);
    }

    /*
    Navigates back to the previous page
    */
    private navigateBack(): void {
        this._location.back();
    }
    onChangeSelection(selected) {
        this._newFinOp.targetId = parseInt(selected);
    }
}