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
import { FinOpListViewModel } from "../../view-models/concrete/finance/finop-list-viewmodel";
import { isBrowser } from "angular2-universal";
import { FinOpService } from "../../services/concrete/finance/finOp.service";
import { TargetViewModel } from "../../view-models/concrete/finance/donate/target.view-model";
import { OrgAccountSelectViewModel } from "../../view-models/concrete/finance/org-accounts-select-view.model";
import * as key from '../../shared/key.storage'
import * as message from '../../shared/common-message.storage'
import * as constant from '../../shared/default-configuration.storage';
import { AuthorizeUserModel } from "../../view-models/concrete/authorized-user-info-view.model";
import { SpinnerComponent } from "../../shared/components/spinner/spinner.component";
import { OrgAccountService } from "../../services/concrete/finance/orgaccount.service";
import { OrgAccountViewModel } from "../../view-models/concrete/finance/orgaccount-viewmodel";
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

    @ViewChild("suggestedImports")
    public suggestedImportsWindow: ModalComponent;

    @ViewChild(SpinnerComponent) spinner: SpinnerComponent;
    @ViewChild("warning")
    public finOpWarningWindow: ModalComponent;


    //model which contain data to create new finOp
    private _newFinOp: FinOpFromBankViewModel = new FinOpFromBankViewModel();
    private transferFinOp: FinOpFromBankViewModel = new FinOpFromBankViewModel();
    private commisionFinOp: FinOpFromBankViewModel = new FinOpFromBankViewModel();
    //data which was received from privat24
    private importData: ImportPrivatViewModel = new ImportPrivatViewModel();
    //array which contain register bank imports in db
    private _dataForFinOp: ImportDetailPrivatViewModel[] = new Array<ImportDetailPrivatViewModel>();
    //model for filtering bank imports
    private _bankSearchModel: BankImportSearchViewModel = new BankImportSearchViewModel();
    //strings for contain date
    private suggestedBankImports: ImportDetailPrivatViewModel[] = new Array<ImportDetailPrivatViewModel>();

    private selectedBankImport: ImportDetailPrivatViewModel = new ImportDetailPrivatViewModel();
    @Input() dataPrivatFrom: string;
    @Input() dataPrivatTo: string;

    //array which contains targets to create new finOp
    private targets: TargetViewModel[] = new Array<TargetViewModel>();
    //model which contain select orgaccount(name,number,id)
    private currentOrgAccount: OrgAccountSelectViewModel = new OrgAccountSelectViewModel();
    private currentOrgAccountNumber: string;

    private cashAccounts: OrgAccountViewModel[] = new Array<OrgAccountViewModel>();
    private cashAccountsTo: OrgAccountViewModel[] = new Array<OrgAccountViewModel>();
    private bankAccounts: OrgAccountViewModel[] = new Array<OrgAccountViewModel>();
    private bankAccountsTo: OrgAccountViewModel[] = new Array<OrgAccountViewModel>();
    private updateFinOperation: FinOpListViewModel = new FinOpListViewModel();
    private updateFinOperationResponse: FinOpListViewModel = new FinOpListViewModel();

    //current user in system
    private user: AuthorizeUserModel = new AuthorizeUserModel();
    //index selected bank import in table
    private index: number;
    //count bank imports in selected orgAccounts
    private count: number;
    private bankAccId: number;
    private isOrgAccountHaveTarget: boolean;
    private isWindthraw: boolean = false;
    private isDeposite: boolean = false;
    private isBankTransfer: boolean = false;
    private showSpinner: boolean = false;
    private work: boolean = false;
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
        this._service.getAllExtracts(this.card)
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
        this.showSpinner = true;
        this._service.getPrivatExtracts(this.dataForPrivat)
            .subscribe(() => {
                this._service.UpdateDate(this.user.orgId)
                    .subscribe(response => {
                        this.lastPrivatUpdate = response;
                    });
                this.getAllExtracts();
                this.showSpinner = false;
            });
        this.newBankImportModalWindow.hide();
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
    public initializeFinOp(bankImport: ImportDetailPrivatViewModel) {
        this._newFinOp = new FinOpFromBankViewModel();
        this._newFinOp.description = bankImport.description;
        this._newFinOp.bankImportId = bankImport.id;
        this._newFinOp.amount = +bankImport.cardAmount.split(' ')[0];
        this._newFinOp.absoluteAmount = Math.abs(this._newFinOp.amount);
        if (this._newFinOp.amount > 0) {
            this._newFinOp.cardToId = Number(this.currentOrgAccount.id);
            this._newFinOp.finOpType = constant.incomeId;
        }
        if (this._newFinOp.amount < 0) {
            this._newFinOp.cardFromId = Number(this.currentOrgAccount.id);
            this._newFinOp.finOpType = constant.spendingId;
        }
        this._newFinOp.orgId = this.user.orgId;
        this._newFinOp.targetId = this.targets[0].targetId;
        this.index = this._dataForFinOp.findIndex(element => element.id == bankImport.id);
        this.currentOrgAccountNumber = this.currentOrgAccount.orgAccountName + ': ' + this.currentOrgAccount.orgAccountNumber;
    }

    public createFinOp(finOp: FinOpFromBankViewModel) {
        this.defineOperation(finOp);
        finOp.orgId = this.user.orgId;
        finOp.userId = this.user.id;
        finOp.amount = Math.abs(finOp.amount);
        this.saveFinOp(finOp);
    }

    private defineOperation(finOp: FinOpFromBankViewModel) {
        if (this.isWindthraw || this.isBankTransfer) {
            finOp.cardFromId = this.currentOrgAccount.id;
            finOp.finOpType = constant.transferId;
            return
        }

        if (this.isDeposite) {
            finOp.cardToId = this.currentOrgAccount.id;
            finOp.finOpType = constant.transferId;
            return;
        }

        if (this._newFinOp.amount > 0) {
            finOp.cardToId = this.currentOrgAccount.id;
            finOp.finOpType = constant.incomeId;
            return;
        }

        if (this._newFinOp.amount < 0) {
            finOp.cardFromId = this.currentOrgAccount.id;
            finOp.finOpType = constant.spendingId;
            return;
        }

    }

    private getSuggestedBankImports() {
        this._service.getAllSuggestedBankImports(this._newFinOp.amount, this._newFinOp.finOpDate).subscribe(bankImports => {
            this.suggestedBankImports = bankImports;
        });
    }

    private saveCommision() {
        this.commisionFinOp.bankImportId = this._newFinOp.bankImportId;
        this.commisionFinOp.cardFromId = this.currentOrgAccount.id;
        this.commisionFinOp.amount = Math.abs(this._newFinOp.absoluteAmount - +this.selectedBankImport.amount.split(' ')[0]);
        this.commisionFinOp.description = message.commisionMessage;
        this.commisionFinOp.finOpType = constant.spendingId;
        this.commisionFinOp.targetId = null;
        this.commisionFinOp.userId = this.user.id;
        this.commisionFinOp.orgId = this.user.orgId;
        this._finOpService.createFinOp(this.commisionFinOp, this.spinner).subscribe(responce => {
            this.commisionFinOp = responce;
        });
    }

    private getBankAccountOrganizationId() {
        this.bankAccId = this.bankAccounts.find(b => b.cardNumber == this.selectedBankImport.card).bankAccId;
        this.bankAccId = this.bankAccounts.find(b => b.bankAccId == this.bankAccId).id;
    }

    private transferOperation() {
        this.saveCommision();
        this.getBankAccountOrganizationId();
        this.closeSuggestionsModal();
        if (this._newFinOp.amount < 0) {
            this._newFinOp.cardToId = this.bankAccId;
            this._newFinOp.bankImportId = this.selectedBankImport.id;
            this.createFinOp(this._newFinOp);
        }
        else {
            this._newFinOp.amount = +this.selectedBankImport.amount.split(' ')[0];
            this._newFinOp.cardToId = this.bankAccId;
            this._newFinOp.description = this.selectedBankImport.description;
            this._newFinOp.finOpDate = this.selectedBankImport.trandate;
            this._newFinOp.bankImportId = this.selectedBankImport.id;
            this.createFinOp(this._newFinOp);
        }
    }

    private radioButtonOnChange(bankImport: ImportDetailPrivatViewModel) {
        this.selectedBankImport = bankImport;
    }

    //save new initialize finOp
    public saveFinOp(finOp: FinOpFromBankViewModel) {
        this._finOpService.createFinOp(finOp, this.spinner)
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
    private closeModal(): void {
        this.newBankImportModalWindow.hide();
    }

    /**
     * Open bankImports modal window
     */
    private onActionClick(): void {
        this.dataForPrivat.card = this.card;
        this.newBankImportModalWindow.show();
    }

    private onIncomeClick(): void {
        if (this.currentOrgAccount.targetId != undefined) {
            this.showSpinner = true;
            this._finOpService.processMultipleFinOps(this.currentOrgAccount.id).subscribe(() => {
                debugger;
                this.getAllExtracts();
                this.showSpinner = false;
            });
            this.closeWarningModal();
        }
    }

    private warningWindowShow(): void {
        this.finOpWarningWindow.show();
    }

    private onPrivatClick(): void {
        this.work = true;
        this._service.getUserExtracts(this.currentOrgAccount.id).subscribe(() => {
            this._service.UpdateDate(this.user.orgId)
                .subscribe(response => {
                    this.lastPrivatUpdate = response;
                });
            this.getAllExtracts();
            this.work = false;
        });
    }
    /**
 * open finOp modal window
 */
    private openFinOpModal(bankImport: ImportDetailPrivatViewModel): void {
        this.initializeFinOp(bankImport);
        this.getSuggestedBankImports();
        this.finOpModalWindow.show();
    }

    /**
     * close finOp modal window
     */
    public closeFinOpModal(): void {
        this.finOpModalWindow.hide();
        this._newFinOp = new FinOpFromBankViewModel();
        this.updateFinOperation = new FinOpListViewModel();
        this.isWindthraw = false;
        this.isDeposite = false;
        this.isBankTransfer = false;
    }

    private openSuggestionsModal() {
        this.isBankTransfer = true;
        this.selectedBankImport = this.suggestedBankImports[0];
        this.suggestedImportsWindow.show();
    }

    private closeWarningModal(): void {
        this.finOpWarningWindow.hide();
    }
    /**
     * open finOp modal window
     */
    private closeSuggestionsModal() {
        this.isBankTransfer = false;
        this.suggestedImportsWindow.hide();
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