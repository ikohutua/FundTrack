import { Component, Input, ViewChild } from "@angular/core";
import { PrivatSessionViewModel } from "../../view-models/concrete/privat-session-view.model";
import { DataRequestPrivatViewModel } from "../../view-models/concrete/data-request-privat-view.model";
import { ModalComponent } from "../../shared/components/modal/modal-component";
import { BankImportService } from "../../services/concrete/bank-import.service";
import { ImportPrivatViewModel } from "../../view-models/concrete/import-privat-view.model";

@Component({
    template: require('./bank-import.component.html'),
    styles: [require('./bank-import.component.css')],
    providers: [BankImportService]
})

export class BankImportComponent {
    @Input() dataForPrivat: DataRequestPrivatViewModel = new DataRequestPrivatViewModel();

    @ViewChild(ModalComponent)

    public modalWindow: ModalComponent;
    public importData: ImportPrivatViewModel = new ImportPrivatViewModel();

    public constructor(private _service: BankImportService) { }

    public getExtracts() {
        this._service.getUserExtracts(this.dataForPrivat)
            .subscribe(response => {
                this.importData = response;
                console.log(this.importData);
                this._service.registerBankExtracts(this.importData)
                    .subscribe(response => {
                        console.log("OK");
                        console.log(response);
                    });
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