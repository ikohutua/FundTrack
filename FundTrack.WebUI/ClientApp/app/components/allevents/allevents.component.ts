import { Component, OnInit, Input } from '@angular/core';
import { MainPageViewModel } from "../../view-models/concrete/main-page-view-model";
import { IEventModel } from "../../view-models/abstract/event-model.interface";
import { EventModel } from "../../view-models/concrete/event-model";
import { OrganizationEventService } from "../../services/concrete/organization-events.service";
import { Subscription } from "rxjs/Subscription";
import { ActivatedRoute } from "@angular/router";

@Component({
    templateUrl: './allevents.component.html',
    styleUrls: ['./allevents.component.css'],
    providers: [OrganizationEventService]
})

export class AllEventsComponent implements OnInit {
    private _model: EventModel[];
    private _errorMessage: string;
    private _subscribe: Subscription;

    constructor(private _service: OrganizationEventService, private _router: ActivatedRoute) { }

    private getEventsList(id?: number): void {
        if (id) {
            this._service.getCollection(id, 'api/Event/AllEventsOfOrganization')
                .subscribe(model => this._model = model,
                error => this._errorMessage = <any>error);
        }
        else {
            this._service.getCollection()
                .subscribe(model => this._model = model,
                error => this._errorMessage = <any>error);
        }
    }

    ngOnInit(): void {
        this._subscribe = this._router.params.subscribe(params => {
            let id = +params['id'];
            this.getEventsList(id);
        });
    }

    ngDestroy(): void {
        this._subscribe.unsubscribe();
    }
}