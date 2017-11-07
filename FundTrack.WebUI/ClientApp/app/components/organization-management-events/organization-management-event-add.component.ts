import { Component } from "@angular/core";
import { IEventManagementViewModel } from "../../view-models/abstract/organization-management-view-models/event-management-view-model.interface";
import { ActivatedRoute, Router } from "@angular/router";
import { OrganizationManagementEventsService } from "../../services/concrete/organization-management/organization-management-events.service";
import { Subscription } from "rxjs/Subscription";
import { EventManagementViewModel } from "../../view-models/concrete/event-management-view-model";
import { ImageModel } from "../../view-models/concrete/image-url-view-model";
import { IImageModel } from "../../view-models/abstract/organization-management-view-models/image-url-view-model.interface";

@Component({
    selector: 'org-management-add',
    templateUrl: './organization-management-event-add.component.html',
    styleUrls: ['./organization-management-event-add.component.css']
})

export class OrganizationManagementEventAddComponent {
    private _idForCurrentOrganization: number;
    private _event: IEventManagementViewModel = new EventManagementViewModel();
    private _subscription: Subscription;
    private _errorMessage: string;
    private _currentImage: IImageModel;

    /**
     * @constructor
     * @param _route: ActivatedRoute
     * @param _router: Router
     * @param _service: OrganizationManagementEventsService
     */
    public constructor(private _route: ActivatedRoute, private _router: Router, private _service: OrganizationManagementEventsService) { }

    ngOnInit(): void {
        this._subscription = this._route.params.subscribe(
            params => {
                this._idForCurrentOrganization = +params["id"];
                this._event = new EventManagementViewModel();
            });
    }

    /**
     * Adds new event
     */
    private addNewEvent(): void {
        this._event.organizationId = this._idForCurrentOrganization;
        this._event.createDate = new Date(Date.now());
        this._service.addNewEvent(this._event).subscribe(
            () => { this._router.navigate(['organization/events/' + this._event.organizationId]); },
            error => { this._errorMessage = <any>error });
    }

    /**
     * Gets extension of specified file
     * @param fileName: name of the file extension of which is needed to be retrieved
     */
    private getFileExtension(fileName: string): string {
        return fileName.substring(fileName.lastIndexOf('.') + 1, fileName.length) || fileName;
    }

    /**
     * Deletes image from local list
     * @param imageUrl
     */
    private deleteImageFromList(imageUrl: string): void {
        this._event.images.splice(this._event.images.findIndex(i => i.imageUrl == imageUrl), 1)
    }

    /**
     * Redirect to all events list
     */
    private redirectToAllEvents(): void {
        this._router.navigate(['organization/events/' + this._idForCurrentOrganization]);
    }

    ngDestroy(): void {
        this._subscription.unsubscribe();
    }
}