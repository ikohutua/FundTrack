import { Component, OnInit, Input } from '@angular/core';
import { IEventDetailModel } from "../../view-models/abstract/eventdetail-model.interface";
import { EventDetailModel } from "../../view-models/concrete/eventdetail-model";
import { EventDetailService } from "../../services/concrete/eventdetail.service";
import { Subscription } from "rxjs/Subscription";
import { ActivatedRoute } from "@angular/router";

@Component({
    templateUrl: './event-detail.component.html',
    styleUrls: ['./event-detail.component.css'],
    providers: [EventDetailService]
})

export class EventDetailComponent implements OnInit {
    private _eventDetail: EventDetailModel;
    private _errorMessage: string;
    private _subscribe: Subscription;
    private _getEventById: string = 'api/EventDetail/EventDetailById';

    constructor(private _service: EventDetailService, private _router: ActivatedRoute) { }

    private getEventDetail(id: number): void {
        this._service.getById(id, this._getEventById)
            .subscribe(model => this._eventDetail = model,
            error => this._errorMessage = <any>error);
    }

    ngOnInit(): void {
        this._subscribe = this._router.params.subscribe(params => {
            let id = +params['id'];
            this.getEventDetail(id);
        });
    }

    ngDestroy(): void {
        this._subscribe.unsubscribe();
    }
}