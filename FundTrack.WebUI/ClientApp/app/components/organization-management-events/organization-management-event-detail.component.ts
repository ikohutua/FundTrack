import { Component } from "@angular/core";
import { ActivatedRoute, Router } from "@angular/router";
import { OrganizationManagementEventsService } from "../../services/concrete/organization-management/organization-management-events.service";
import { IEventManagementViewModel } from "../../view-models/abstract/organization-management-view-models/event-management-view-model.interface";
import { Subscription } from "rxjs/Subscription";

@Component({
    selector: 'org-management-event-detail',
    templateUrl: './organization-management-event-detail.component.html',
    styles: ['./organization-management-event-detail.component.css']
})
export class OrganizationManagementEventDetailComponent {
    private _idForCurrentEvent: number;
    private _event: IEventManagementViewModel;
    private _subscription: Subscription;
    private _images: string[];

    /**
     * @constructor
     * @param _route: ActivatedRoute
     * @param _router: Router
     * @param _service: OrganizationManagementEventsService
     */
    public constructor(private _route: ActivatedRoute, private _router: Router, private _service: OrganizationManagementEventsService) { }

    ngOnInit(): void {
        this._subscription = this._route.params.subscribe(params => {
            this._idForCurrentEvent = +params['id'];
            this.getInformationOfEvent(this._idForCurrentEvent);
        });
    }

    /**
    * Gets one event by identifier
    * @param id
    */
    private getInformationOfEvent(id: number): void {
        this._service.getOneEventById(id).subscribe(
            event => {
                this._event = event;
                this._images = [];
                this._event.images.forEach((image) => {
                    this._images.push(image.imageSrc);
                });
            });
    }

    /**
     * Redirect to all events list
     */
    private redirectToAllEvents(): void {
        this._router.navigate(['organization/events/' + this._event.organizationId]);
    }
}