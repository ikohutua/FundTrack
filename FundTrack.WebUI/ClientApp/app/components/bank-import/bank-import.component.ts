import { Component, Input, ViewChild, Output } from "@angular/core";
import { PrivatSessionViewModel } from "../../view-models/concrete/privat-session-view.model";
import { DataRequestPrivatViewModel } from "../../view-models/concrete/data-request-privat-view.model";
import { ModalComponent } from "../../shared/components/modal/modal-component";
import { BankImportService } from "../../services/concrete/bank-import.service";
import { ImportDetailPrivatViewModel, ImportPrivatViewModel } from "../../view-models/concrete/import-privat-view.model";
import { BankImportSearchViewModel } from "../../view-models/concrete/finance/bank-import-search-view.model";

@Component({
    template: require('./bank-import.component.html'),
    styles: [require('./bank-import.component.css')],
    providers: [BankImportService]
})

export class BankImportComponent {
    @Input() dataForPrivat: DataRequestPrivatViewModel = new DataRequestPrivatViewModel();

    @ViewChild(ModalComponent)

    public modalWindow: ModalComponent;

    private importData: ImportPrivatViewModel = new ImportPrivatViewModel();
    private _dataForFinOp: ImportDetailPrivatViewModel[] = new Array<ImportDetailPrivatViewModel>();
    private _bankSearchModel: BankImportSearchViewModel = new BankImportSearchViewModel();
    @Input() dataPrivatFrom: string;
    @Input() dataPrivatTo: string;

    public constructor(private _service: BankImportService) { }

    public getExtracts() {
        let datesFrom = this.dataPrivatFrom.split('-');
        let datesTo = this.dataPrivatTo.split('-');
        this.dataForPrivat.dataFrom = datesFrom.reverse().join('.');
        this.dataForPrivat.dataTo = datesTo.reverse().join('.');
        this._service.getUserExtracts(this.dataForPrivat)
            .subscribe(response => {
                this.importData = response;
                console.log(this.importData);
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