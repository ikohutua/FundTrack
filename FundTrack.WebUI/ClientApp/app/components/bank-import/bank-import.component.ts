import { Component, Input, ViewChild, Output } from "@angular/core";
import { PrivatSessionViewModel } from "../../view-models/concrete/privat-session-view.model";
import { DataRequestPrivatViewModel } from "../../view-models/concrete/data-request-privat-view.model";
import { ModalComponent } from "../../shared/components/modal/modal-component";
import { BankImportService } from "../../services/concrete/bank-import.service";
import { ImportDetailPrivatViewModel, ImportPrivatViewModel } from "../../view-models/concrete/import-privat-view.model";
import { BankImportSearchViewModel } from "../../view-models/concrete/finance/bank-import-search-view.model";
import { FormGroup, FormBuilder, Validators } from "@angular/forms";
import { ValidatorsService } from "../../services/concrete/validators/validator.service";

@Component({
    template: require('./bank-import.component.html'),
    styles: [require('./bank-import.component.css')],
    providers: [BankImportService]
})

export class BankImportComponent {
    public bankImportForm: FormGroup;

    private dataForPrivat: DataRequestPrivatViewModel = new DataRequestPrivatViewModel();

    @ViewChild(ModalComponent)

    public modalWindow: ModalComponent;

    private importData: ImportPrivatViewModel = new ImportPrivatViewModel();
    private _dataForFinOp: ImportDetailPrivatViewModel[] = new Array<ImportDetailPrivatViewModel>();
    private _bankSearchModel: BankImportSearchViewModel = new BankImportSearchViewModel();
    @Input() dataPrivatFrom: string;
    @Input() dataPrivatTo: string;

    public constructor(private _service: BankImportService,
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

    /*
Errors to be displayed on the UI
*/
    private formErrors = {
        cardNumber: "",
        idMerchant: "",
        password: "",
        dataTo: "",
        dataFrom:""
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
                console.log(this.importData);
                console.log("bla");
                    if(!this.importData.error) {
                    this._service.registerBankExtracts(this.importData.importsDetail)
                        .subscribe(response => {
                            let dataFrom = this.dataForPrivat.dataFrom.split('.');
                            let dataTo = this.dataForPrivat.dataTo.split('.');
                            this._bankSearchModel.dataFrom = new Date(+dataFrom[2], ((+dataFrom[1]) - 1), +dataFrom[0], 3, 0, 0);
                            this._bankSearchModel.dataTo = new Date(+dataTo[2], ((+dataTo[1]) - 1), (+dataTo[0]) + 1, 2, 59, 59);
                            this._bankSearchModel.card = this.dataForPrivat.card;
                            this._service.getRawExtracts(this._bankSearchModel)
                                .subscribe(response => {
                                    console.log(response);
                                    this._dataForFinOp = response;
                                })
                        });
                }
            });
    }

    /**
    * Closes modal window
    */
    public closeModal(): void {
        this.modalWindow.hide();
    }

    /**
     * Open modal window
     */
    public onActionClick(): void {
        this.modalWindow.show();
    }
}