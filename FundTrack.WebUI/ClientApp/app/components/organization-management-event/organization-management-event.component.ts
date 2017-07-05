import { Component, OnInit } from "@angular/core";
import { OrganizationManagementEventService } from "../../services/concrete/organization-management/organization-management-event.service";
import { IEventModel } from "../../view-models/abstract/event-model.interface";

@Component({
    selector: 'org-management-event',
    templateUrl: './organization-management-event.component.html',
    styleUrls: ['./organization-management-event.component.css'],
    providers: [OrganizationManagementEventService]
})
export class OrganizationManagementEventComponent implements OnInit {
    private _idForCurrentOrganization: number;
    private _allEvents: IEventModel[];

    constructor(private _service: OrganizationManagementEventService) { }

    ngOnInit(): void {
        //this._service.getCollection()
    }
}