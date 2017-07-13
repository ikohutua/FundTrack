import { Component, OnInit } from "@angular/core";
import { Subscription } from "rxjs/Subscription";
import { ActivatedRoute, Router, Params } from "@angular/router";
import { OrganizationManagementEventsService } from "../../services/concrete/organization-management/organization-management-events.service";
import { IEventManagementViewModel } from "../../view-models/abstract/organization-management-view-models/event-management-view-model.interface";

@Component({
    selector: 'org-management-event',
    templateUrl: './organization-management-event.component.html',
    styleUrls: ['./organization-management-event.component.css'],
    providers: [OrganizationManagementEventsService]
})

export class OrganizationManagementEventsComponent implements OnInit {
    private _idForCurrentOrganization: number;
    private _allEvents: IEventManagementViewModel[];
    private _subscription: Subscription;
    private _errorMessage: string;

    constructor(private _route: ActivatedRoute, private _router: Router, private _service: OrganizationManagementEventsService) { }

    ngOnInit(): void {
        this._subscription = this._route.params.subscribe(
            params => {
                debugger;
                this._idForCurrentOrganization = +params["id"];
                this.getAllEvents(this._idForCurrentOrganization);
            });
    }

    private getAllEvents(id: number): void {
        this._service.getAllEventsByOrganizationId(id)
            .subscribe(events => this._allEvents = events,
            error => this._errorMessage = <any>error);
        debugger;
    }

    private redirectToDetailEditPage(id: number): void {
        this._router.navigate(['organization/event/edit/' + id.toString()]);
    }

    private deleteEvent(id: number): void {
        if (confirm("Ви впевнені, що хочете видалити подію?")) {
            this._service.deleteEvent(id).subscribe(data => this._allEvents.splice(this._allEvents.findIndex(e => e.id == id), 1));
        }
    }

    ngDestroy(): void {
        this._subscription.unsubscribe();
    }
}