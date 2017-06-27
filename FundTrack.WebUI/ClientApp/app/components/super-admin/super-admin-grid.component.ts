import { Component, OnChanges, Input, EventEmitter, Output } from "@angular/core";
import { SuperAdminItemsViewModel } from '../../view-models/abstract/super-admin.view-models/super-admin-users-view-model';
import { ModalComponent } from '../../shared/components/modal/modal-component';
import { ViewChild } from '@angular/core';
import { SuperAdminChangeStatusViewModel } from '../../view-models/abstract/super-admin.view-models/super-admin-change-status-view-model';

@Component({
    selector: 'super-admin-grid',
    templateUrl: './super-admin-grid.component.html'
})

/**
* Componet to dispay Grid for users or organizations
*/
export class SuperAdminGrid {
    @Input() arrayToDisplay: SuperAdminItemsViewModel[];
    @Input() headers: string[];
    @Output() banStatusChange = new EventEmitter()
    @ViewChild(ModalComponent)

    public modalWindow: ModalComponent

    public selectedItemBanDescription: string = '';   
    public selectedItemStatus: string = '';
    private selectedItemId: number;
    private customeFieldTouched = false;

    /**
     * Trigers when user click on change status button
     * @param item
     */
    public onActionClick(item: SuperAdminItemsViewModel) {    
        this.selectedItemBanDescription = item.bannDescription;
        this.selectedItemId = item.id;
        this.selectedItemStatus = item.isBanned == true ? 'розблокувати' : 'заблокувати';

        this.modalWindow.show();
    }

    /**
     * Trigers when user click on change status button on modal window
     * @param bannDescription
     */
    public statusChange(bannDescription: string) {
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
    public closeModal() {
        this.modalWindow.hide();
        this.customeFieldTouched = false;
    }
}