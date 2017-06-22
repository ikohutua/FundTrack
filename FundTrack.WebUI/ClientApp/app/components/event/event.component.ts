import { Component, OnInit } from '@angular/core';
import { MainPageViewModel } from "../../view-models/concrete/main-page-view-model";
import { IEventModel } from "../../view-models/abstract/event-model.interface";
import { EventModel } from "../../view-models/concrete/event-model";
import { OrganizationEventService } from "../../services/concrete/organization-events.service";

@Component({
    selector: 'event',
    templateUrl: './event.component.html',
    styleUrls: ['./event.component.css'],
    providers: [OrganizationEventService]
})
export class EventComponent implements OnInit {
    private model: EventModel[];
    private _errorMessage: string;

    constructor(private _service: OrganizationEventService) { }

    getEventsList(): void {
        this._service.getCollection()
            .subscribe(model => this.model = model,
            error => this._errorMessage = <any>error);
    }

    ngOnInit(): void {
        this.getEventsList();
    }
}
