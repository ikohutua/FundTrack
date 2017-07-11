import { Component, Input, EventEmitter, Output, OnInit } from "@angular/core"
import { RequestManagementViewModel } from "../../view-models/abstract/organization-management-view-models/request-management-view-model";

@Component({
    selector: 'delete-request',
    template: require('./organization-delete-request.component.html'),
    styles: [require('./organization-delete-request.component.css')],
   })


export class OrganizationDeleteRequestComponent implements OnInit {
    ngOnInit(): void {
        console.log(this.itemToToDelete);
    }
    @Input() itemToToDelete: RequestManagementViewModel = new RequestManagementViewModel();
    @Output() onSuccesfullDelete = new EventEmitter<boolean>();

    public deleteConfirmation() {
        this.onSuccesfullDelete.emit();
    }
}