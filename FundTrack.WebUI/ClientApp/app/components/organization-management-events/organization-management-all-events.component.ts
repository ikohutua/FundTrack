import { Component, OnInit } from "@angular/core";
import { Subscription } from "rxjs/Subscription";
import { ActivatedRoute, Router, Params } from "@angular/router";
import { OrganizationManagementEventsService } from "../../services/concrete/organization-management/organization-management-events.service";
import { IEventManagementViewModel } from "../../view-models/abstract/organization-management-view-models/event-management-view-model.interface";
import { EventManagementViewModel } from "../../view-models/concrete/event-management-view-model";

@Component({
    selector: 'org-management-event',
    templateUrl: './organization-management-all-events.component.html',
    styleUrls: ['./organization-management-all-events.component.css'],
    providers: [OrganizationManagementEventsService]
})

export class OrganizationManagementAllEventsComponent implements OnInit {
    private _idForCurrentOrganization: number;
    private _currentEventItem: EventManagementViewModel;
    private _allEvents: IEventManagementViewModel[];
    private _subscription: Subscription;
    private _errorMessage: string;
    private _totalItems: number;
    private _offset: number = 0;
    private _itemPerPage: number = 8;
    private _currentPage: number = 1;
    private _showUsersSpinner: boolean = false;

    /**
     * @constructor
     * @param _route: ActivatedRoute
     * @param _router: Router
     * @param _service: OrganizationManagementEventsService
     */
    constructor(private _route: ActivatedRoute, private _router: Router, private _service: OrganizationManagementEventsService) { }


    ngOnInit(): void {
        this._showUsersSpinner = true;
        this._subscription = this._route.params.subscribe(
            params => {
                this._idForCurrentOrganization = +params["id"];
                this.getEventsInitData(this._idForCurrentOrganization);
            });
    }

    /**
     * Trigers when user changes page
     * @param page
     */
    public onPageChange(page): void {
        this._showUsersSpinner = true;
        this._service.getEventsByOrganizationIdForPage(this._idForCurrentOrganization, page, this._itemPerPage).subscribe(
            events => {
                this._allEvents = events;
                this._offset = (page - 1) * this._itemPerPage;
                this._showUsersSpinner = false;
            }
        );
    }

    /**
     * Gets events initial data
     * @param idOrganization
     */
    private getEventsInitData(idOrganization: number): void {
        this._service.getEventsInitData(idOrganization).subscribe(response => {
            this._totalItems = response.totalEventsCount;
            this._itemPerPage = response.eventsPerPage;
            this.getEventsPerPageByOrganizationId(this._idForCurrentOrganization, this._currentPage, this._itemPerPage);
        });
    }

    /**
     * Gets events per page
     * @param idOrganization
     * @param currentPage
     * @param pageSize
     */
    private getEventsPerPageByOrganizationId(idOrganization: number, currentPage: number, pageSize: number): void {
        this._service.getEventsByOrganizationIdForPage(idOrganization, currentPage, pageSize)
            .subscribe(events => {
                this._allEvents = events;
                this._showUsersSpinner = false;
            });
    }

    /**
     * Redirect to event deteil page
     * @param identifier for concrete event
     */
    private redirectToDetailEditPage(id: number): void {
        this._router.navigate(['organization/event/edit/' + id.toString()]);
    }

    /**
     * Deletes concrete event by identifier
     */
    private deleteEvent(): void {
        this._service.deleteEvent(this._currentEventItem.id)
            .subscribe(data => this._allEvents.splice(this._allEvents.findIndex(e => e.id == this._currentEventItem.id), 1));
    }

    /**
     * Sets current event
     * @param event
     */
    private setCurrentEventItem(event: EventManagementViewModel): void {
        this._currentEventItem = event;
    }

    /**
     * Trigers when user changes items to display on page
     * @param amount
     */
    public itemsPerPageChange(amount: number): void {
        this._showUsersSpinner = true;
        this._service.getEventsByOrganizationIdForPage(this._idForCurrentOrganization, 1, amount).subscribe(
            events => {
                this._offset = 0;
                this._allEvents = events;
                this._itemPerPage = amount;
                this._showUsersSpinner = false;
            });
    }

    /**
     * Redirect to event deteil page
     * @param id 
     */
    private redirectToDeteilPage(id: number) {
        this._router.navigate(['organization/event/detail/' + id]);
    }
    
    ngDestroy(): void {
        this._subscription.unsubscribe();
    }
}