import { Component, OnInit } from "@angular/core";
import { Subscription } from "rxjs/Subscription";
import { ActivatedRoute, Router } from "@angular/router";
import { OrganizationManagementEventsService } from "../../services/concrete/organization-management/organization-management-events.service";
import { IEventManagementViewModel } from "../../view-models/abstract/organization-management-view-models/event-management-view-model.interface";

@Component({
    selector: 'org-management-event',
    templateUrl: './organization-management-event.component.html',
    styleUrls: ['./organization-management-event.component.css'],
    providers: [OrganizationManagementEventsService],
})

export class OrganizationManagementEventComponent implements OnInit {
    private _idForCurrentOrganization: number;
    private _allEvents: IEventManagementViewModel[];
    private _subscription: Subscription;
    private _errorMessage: string;

    constructor(private _route: ActivatedRoute, private _router: Router, private _service: OrganizationManagementEventsService) { }

    ngOnInit(): void {
        this._subscription = this._route.params.subscribe(        
            params => {
                this._idForCurrentOrganization = +params['id'];
                console.log(this._idForCurrentOrganization);
                this.getAllEvents(1);
            }
        );
    }

    private getAllEvents(id: number) {
        this._service.GetAllEventsByOrganizationId(id)
            .subscribe(events => this._allEvents = events,
            error => this._errorMessage = <any>error);
    }

    ngDestroy(): void {
        this._subscription.unsubscribe();
    }
}