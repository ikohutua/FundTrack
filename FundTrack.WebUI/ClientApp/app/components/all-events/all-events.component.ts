import { Component, OnInit, Input, HostListener, OnDestroy, AfterContentChecked } from '@angular/core';
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

export class AllEventsComponent implements OnInit, OnDestroy, AfterContentChecked {
    private _model: EventModel[] = new Array<EventModel>();
    private _errorMessage: string;
    private _subscription: Subscription;
    private _urlAllEventsByScroll: string = 'api/Event/AllEventsByScroll';
    private _urlAllEventsOfOrganization: string = 'api/Event/AllEventsOfOrganization';
    private _urlGetEventsPaginationData: string = 'api/Event/GetEventsPaginationData';
    private _organizationId: number;
    private _countOfEvents: number;
    private _currentPage: number = 1;
    private _itemsPerPage: number = 6;
    public showUsersSpinner: boolean = false;

    constructor(private _service: OrganizationEventService, private _router: ActivatedRoute) {
        this.showUsersSpinner = true;
    }

    private getEventsList(id?: number): void {
        if (id) {
            this._organizationId = id;
            this._service.getCollectionById(id, this._urlAllEventsOfOrganization)
                .subscribe(model => {
                    this._model = model,
                        this.showUsersSpinner = false
                },
                error => this._errorMessage = <any>error);
        }
        else {
            this._service.getItemsOnScroll(this._urlAllEventsByScroll, this._itemsPerPage, this._currentPage)
                .subscribe(model => {
                    this._model = this._model.concat(model),
                        this.showUsersSpinner = false
                },
                error => this._errorMessage = <any>error);
        }
    }

    ngOnInit(): void {
        this.showUsersSpinner = true;
        this._service.getInitData(this._urlGetEventsPaginationData).subscribe((data: EventInitViewModel) => {
            this._countOfEvents = data.totalEventsCount;
            this._itemsPerPage = data.eventsPerPage;
        });

        this._subscription = this._router.params.subscribe(params => {
            let id = +params['id'];
            this.getEventsList(id);
        });

    }

    ngAfterContentChecked() {
        if (sessionStorage.getItem("id")) {
            let id = +sessionStorage.getItem("id");
            this.getEventsList(id);
            sessionStorage.clear();
        }
    }

    ngOnDestroy(): void {
        //this._subscription.unsubscribe();
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