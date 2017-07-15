import { Component, Input, EventEmitter, Output, OnInit } from "@angular/core"
import { RequestManagementViewModel } from "../../view-models/abstract/organization-management-view-models/request-management-view-model";
import { IEventManagementViewModel } from "../../view-models/abstract/organization-management-view-models/event-management-view-model.interface";
import { EventManagementViewModel } from "../../view-models/concrete/event-management-view-model";

@Component({
    selector: 'delete-event',
    template: require('./organization-management-event-delete.component.html'),
    styles: [require('./organization-management-event-delete.component.css')],
})

export class OrganizationManagementEventDeleteComponent {

    @Input() itemToToDelete: IEventManagementViewModel = new EventManagementViewModel();

    @Output() onSuccesfullDelete = new EventEmitter<boolean>();

    public deleteConfirmation() {
        this.onSuccesfullDelete.emit();
    }
}