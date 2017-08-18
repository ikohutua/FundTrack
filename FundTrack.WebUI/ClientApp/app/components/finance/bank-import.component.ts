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
import { TargetViewModel } from "../../view-models/concrete/finance/target-view.model";
import { OrgAccountSelectViewModel } from "../../view-models/concrete/finance/org-accounts-select-view.model";
import * as key from '../../shared/key.storage'
import { AuthorizeUserModel } from "../../view-models/concrete/authorized-user-info-view.model";

@Component({
    selector: 'bank-import',
    template: require('./bank-import.component.html'),
    styles: [require('./bank-import.component.css')],
    providers: [BankImportService,FinOpService]
})

export class BankImportComponent implements OnInit {
    public bankImportForm: FormGroup;
    public card: string = "5168742201982208";

    private dataForPrivat: DataRequestPrivatViewModel = new DataRequestPrivatViewModel();

    @ViewChild("newBankImport")
    public newBankImportModalWindow: ModalComponent;

    @ViewChild("finOp")
    public finOpModalWindow: ModalComponent;

    private _newFinOp: FinOpViewModel = new FinOpViewModel();

    private importData: ImportPrivatViewModel = new ImportPrivatViewModel();
    private _dataForFinOp: ImportDetailPrivatViewModel[] = new Array<ImportDetailPrivatViewModel>();
    private _bankSearchModel: BankImportSearchViewModel = new BankImportSearchViewModel();
    @Input() dataPrivatFrom: string;
    @Input() dataPrivatTo: string;

    private targets: TargetViewModel[] = new Array<TargetViewModel>();
    private currentOrgAccount: OrgAccountSelectViewModel = new OrgAccountSelectViewModel();
    private currentOrgAccountNumber: string;

    private user: AuthorizeUserModel = new AuthorizeUserModel();
    private index: number;

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
            if (localStorage.getItem(key.keyToken)) {
                this.user = JSON.parse(localStorage.getItem(key.keyModel)) as AuthorizeUserModel;
                this._service.getAllExtracts(this.card)
                    .subscribe(response => this._dataForFinOp = response);
                this._finOpService.getTargets()
                    .subscribe(response => this.targets = response);
                this._finOpService.getOrgAccountForFinOp(this.user.orgId, this.card)
                    .subscribe(response => this.currentOrgAccount = response);
            }
        }
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

    public getExtracts() {
        let datesFrom = this.dataPrivatFrom.split('-');
        let datesTo = this.dataPrivatTo.split('-');
        this.dataForPrivat.dataFrom = datesFrom.reverse().join('.');
        this.dataForPrivat.dataTo = datesTo.reverse().join('.');
        this._service.getUserExtracts(this.dataForPrivat)
            .subscribe(response => {
                this.importData = response;
                if (!this.importData.error) {
                    this._service.registerBankExtracts(this.importData.importsDetail)
                        .subscribe(response => {
                        });
                }
            });
    }

    public searchBankImport() {
        //let dataFrom = this.dataForPrivat.dataFrom.split('.');
        //let dataTo = this.dataForPrivat.dataTo.split('.');
        //this._bankSearchModel.dataFrom = new Date(+dataFrom[2], ((+dataFrom[1]) - 1), +dataFrom[0], 3, 0, 0);
        //this._bankSearchModel.dataTo = new Date(+dataTo[2], ((+dataTo[1]) - 1), (+dataTo[0]) + 1, 2, 59, 59);
        this._bankSearchModel.card = this.card;
        if (!this._bankSearchModel.state) {
            this._bankSearchModel.state == null;
        }
        this._service.getRawExtracts(this._bankSearchModel)
            .subscribe(response => {
                this._dataForFinOp = response;
            })
    }

    public createFinOp(bankImport: ImportDetailPrivatViewModel) {
        this._newFinOp.description = bankImport.description;
        this._newFinOp.bankImportId = bankImport.id;
        this._newFinOp.amount = +bankImport.cardAmount.split(' ')[0];
        this._newFinOp.accToName = this.currentOrgAccount.orgAccountName;
        this._newFinOp.orgId = this.user.orgId;
        this.index = this._dataForFinOp.findIndex(element => element.id == bankImport.id);
        this.currentOrgAccountNumber = this.currentOrgAccount.orgAccountName + ' ( ' + this.currentOrgAccount.orgAccountNumber + ' )';
        this.openFinOpModal();
    }

    public saveFinOp() {
        this._finOpService.createFinOp(this._newFinOp)
            .subscribe(response => console.log(response));
        this.closeFinOpModal();
        this._dataForFinOp[this.index].isLooked = true;
    }

    /**
    * Closes modal window
    */
    public closeModal(): void {
        this.newBankImportModalWindow.hide();
    }

    /**
     * Open modal window
     */
    public onActionClick(): void {
        this.newBankImportModalWindow.show();
    }

    public closeFinOpModal(): void {
        this.finOpModalWindow.hide();
        this._newFinOp = new FinOpViewModel();
    }

    public openFinOpModal(): void {
        this.finOpModalWindow.show();
    }
}