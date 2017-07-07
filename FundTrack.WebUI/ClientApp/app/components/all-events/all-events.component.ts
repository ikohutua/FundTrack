import { Component, OnInit, Input, HostListener } from '@angular/core';
import { MainPageViewModel } from "../../view-models/concrete/main-page-view-model";
import { IEventModel } from "../../view-models/abstract/event-model.interface";
import { EventModel } from "../../view-models/concrete/event-model";
import { OrganizationEventService } from "../../services/concrete/organization-events.service";
import { EventInitViewModel } from '../../view-models/abstract/event-initpaginationdata-view-model';
import { Subscription } from "rxjs/Subscription";
import { ActivatedRoute } from "@angular/router";

@Component({
    templateUrl: './all-events.component.html',
    styleUrls: ['./all-events.component.css'],
    providers: [OrganizationEventService]
})

export class AllEventsComponent implements OnInit {
    private _model: EventModel[] = new Array<EventModel>();
    private _errorMessage: string;
    private _subscription: Subscription;
    private _allEventsByScroll: string = 'api/Event/AllEventsByScroll';
    private _allEventsOfOrganization: string = 'api/Event/AllEventsOfOrganization';
    private _getEventsPaginationData: string = 'api/Event/GetEventsPaginationData';
    private _organizationId: number = 0;
    private _countOfEvents: number;
    private _currentPage: number = 1;
    private _itemsPerPage: number = 6;

    constructor(private _service: OrganizationEventService, private _router: ActivatedRoute) { }

    private getEventsList(id?: number): void {
        if (id) {
            this._organizationId = id;
            this._service.getCollectionById(id, this._allEventsOfOrganization)
                .subscribe(model => this._model = model,
                error => this._errorMessage = <any>error);
        }
        else {
            this._service.getItemsOnScroll(this._allEventsByScroll, this._itemsPerPage, this._currentPage)
                .subscribe(model => { this._model = this._model.concat(model) },
                error => this._errorMessage = <any>error);
        }
    }

    ngOnInit(): void {
        this._service.getInitData(this._getEventsPaginationData).subscribe((data: EventInitViewModel) => {
            this._countOfEvents = data.totalEventsCount;
            this._itemsPerPage = data.eventsPerPage;
        });

        this._subscription = this._router.params.subscribe(params => {
            let id = +params['id'];
            this.getEventsList(id);
        });
    }

    ngDestroy(): void {
        this._subscription.unsubscribe();
    }

    @HostListener('window:scroll', ['$event'])
    onScroll($event: Event): void {
        if ((window.innerHeight + window.scrollY) >= document.body.offsetHeight) {
            if (this._currentPage * this._itemsPerPage < this._countOfEvents) {
                this._currentPage = this._currentPage + 1;
                this.getEventsList(this._organizationId);
            }
        }
    }
}