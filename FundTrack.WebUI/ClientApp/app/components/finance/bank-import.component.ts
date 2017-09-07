import { Component, Input, ViewChild, Output, OnInit } from "@angular/core";
import { PrivatSessionViewModel } from "../../view-models/concrete/privat-session-view.model";
import { DataRequestPrivatViewModel } from "../../view-models/concrete/data-request-privat-view.model";
import { ModalComponent } from "../../shared/components/modal/modal-component";
import { BankImportService } from "../../services/concrete/bank-import.service";
import { ImportDetailPrivatViewModel, ImportPrivatViewModel } from "../../view-models/concrete/import-privat-view.model";
import { BankImportSearchViewModel } from "../../view-models/concrete/finance/bank-import-search-view.model";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { ValidatorsService } from "../../services/concrete/validators/validator.service";
import { FinOpViewModel } from "../../view-models/concrete/finance/finOp-view.model";
import { isBrowser } from "angular2-universal";
import { FinOpService } from "../../services/concrete/finance/finOp.service";
import { TargetViewModel } from "../../view-models/concrete/finance/donate/target.view-model";
import { OrgAccountSelectViewModel } from "../../view-models/concrete/finance/org-accounts-select-view.model";
import * as key from '../../shared/key.storage'
import { AuthorizeUserModel } from "../../view-models/concrete/authorized-user-info-view.model";
import { SpinnerComponent } from "../../shared/components/spinner/spinner.component";

@Component({
    selector: 'bank-import',
    template: require('./bank-import.component.html'),
    styles: [require('./bank-import.component.css')],
    providers: [BankImportService, FinOpService]
})

export class BankImportComponent implements OnInit {
    //form for reactive validation
    public bankImportForm: FormGroup;
    //card which belong to select org account
    public card: string;
    //data which sent in request to privat24
    private dataForPrivat: DataRequestPrivatViewModel = new DataRequestPrivatViewModel();

    //modal window to get bankImports
    @ViewChild("newBankImport")
    public newBankImportModalWindow: ModalComponent;
    //modal window to create finOp
    @ViewChild("finOp")
    public finOpModalWindow: ModalComponent;
    @ViewChild(SpinnerComponent) spinner: SpinnerComponent;

    //model which contain data to create new finOp
    private _newFinOp: FinOpViewModel = new FinOpViewModel();
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

    //constructor
    public constructor(private _service: BankImportService,
        private _finOpService: FinOpService,
        private _fb: FormBuilder,
        private _validatorsService: ValidatorsService) {
        this.createForm();
    }

    private createForm(): void {
        this.bankImportForm = this._fb.group({
            cardNumber: [this.dataForPrivat.card, [Validators.required, Validators.maxLength(16), Validators.minLength(16), this._validatorsService.isInteger]],
            idMerchant: [this.dataForPrivat.idMerchant, [Validators.required, Validators.minLength(6), this._validatorsService.isInteger]],
            password: [this.dataForPrivat.password, Validators.required],
            dataTo: [this.dataForPrivat.dataTo, Validators.required],
            dataFrom: [this.dataForPrivat.dataFrom, Validators.required]
        });
        this.bankImportForm.valueChanges
            .subscribe(a => this.onValueChange(a));
        this.onValueChange();
    }

    ngOnInit() {
        if (isBrowser) {
            if (sessionStorage.getItem(key.keyCardNumber)) {
                this.card = sessionStorage.getItem(key.keyCardNumber);
                this._service.getCountExtractsOnCard(this.card)
                    .subscribe(response => {
                        this.count = response;
                        if (localStorage.getItem(key.keyToken)) {
                            this.user = JSON.parse(localStorage.getItem(key.keyModel)) as AuthorizeUserModel;
                            this._finOpService.getTargets()
                                .subscribe(response => this.targets = response);
                            this._finOpService.getOrgAccountForFinOp(this.user.orgId, this.card)
                                .subscribe(response => this.currentOrgAccount = response);
                            this.getAllExtracts();
                        }
                    });
            }
        }
    }

    private getAllExtracts() {
        this._service.getAllExtracts(this.card, this.spinner)
            .subscribe(response => this._dataForFinOp = response);
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
    private requiredMessage = "Поле є обов'язковим для заповнення";
    private numberMessage = "Поле повинно містити тільки цифри";
    private lengthMessage = "Недопустима кількість символів";
    private cardMessage = "Номер картки повинен складатися з 16 цифр";

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
        this.dataForPrivat.dataTo = this.dataPrivatTo.split('-').reverse().join('.');
        this.dataForPrivat.dataFrom = this.dataPrivatFrom.split('-').reverse().join('.');
        this._service.getUserExtracts(this.dataForPrivat)
            .subscribe(response => {
                this.importData = response;
                if (!this.importData.error) {
                    this._service.registerBankExtracts(this.importData.importsDetail)
                        .subscribe(response => {
                            this.showToast();
                            setTimeout(() => {
                                this.getAllExtracts();
                                this.closeModal();
                            }, 2500);
                        });
                }

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
        this._newFinOp.accToName = this.currentOrgAccount.orgAccountName;
        this._newFinOp.orgId = this.user.orgId;
        this.index = this._dataForFinOp.findIndex(element => element.id == bankImport.id);
        this.currentOrgAccountNumber = this.currentOrgAccount.orgAccountName + ': ' + this.currentOrgAccount.orgAccountNumber;
        this.openFinOpModal();
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

    /**
     * close finOp modal window
     */
    public closeFinOpModal(): void {
        this.finOpModalWindow.hide();
        this._newFinOp = new FinOpViewModel();
    }

    /**
     * open finOp modal window
     */
    public openFinOpModal(): void {
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
}