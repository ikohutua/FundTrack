import { Component, OnChanges, Input, EventEmitter, Output } from "@angular/core";
import { SuperAdminItemsViewModel } from '../../view-models/abstract/super-admin.view-models/super-admin-users-view-model';
import { ModalComponent } from '../../shared/components/modal/modal-component';
import { SuperAdminChangeStatusViewModel } from '../../view-models/abstract/super-admin.view-models/super-admin-change-status-view-model';
import { ViewChild } from '@angular/core';

@Component({
    selector: 'super-admin-grid',
    templateUrl: './super-admin-grid.component.html',
    styles: [require('./super-admin-grid.component.css')]
})

/**
* Componet to dispay Grid for users or organizations
*/
export class SuperAdminGrid {
    @Input() arrayToDisplay: SuperAdminItemsViewModel[];
    @Input() headers: string[];
    @Output() banStatusChange = new EventEmitter();
    @ViewChild(ModalComponent)
    public modalWindow: ModalComponent

    private _banTitle: string = 'Забанити';
    private _unBanTitle: string = 'Відбанити'

    public selectedItemId: number;
    public customeFieldTouched = false;
    public selectedItemBanDescription: string = '';
    public selectedItemStatus: string = '';

    /**
     * Trigers when user click on change status button
     * @param item
     */
    public onActionClick(item: SuperAdminItemsViewModel) {    
        this.selectedItemBanDescription = item.bannDescription;
        this.selectedItemId = item.id;
        this.selectedItemStatus = item.isBanned == true ? this._unBanTitle : this._banTitle;

        this.modalWindow.show();
    }

    /**
     * Trigers when user click on change status button on modal window
     * @param bannDescription
     */
    public statusChange(bannDescription: string) : void {
        let model = new SuperAdminChangeStatusViewModel();

        model.id = this.selectedItemId;
        model.banDescription = bannDescription;
        
        this.banStatusChange.emit(model);

        this.modalWindow.hide();
        this.customeFieldTouched = false;
    }   

    /**
     * Closes modal window
     */
    public closeModal() : void {
        this.modalWindow.hide();
        this.customeFieldTouched = false;
    }
}