import { Component, OnInit } from "@angular/core";
import { IEventManagementViewModel } from "../../view-models/abstract/organization-management-view-models/event-management-view-model.interface";
import { OrganizationManagementEventsService } from "../../services/concrete/organization-management/organization-management-events.service";
import { Subscription } from "rxjs/Subscription";
import { ActivatedRoute, Router } from "@angular/router";
import { ImageModel } from "../../view-models/concrete/image-url-view-model";
import { IImageModel } from "../../view-models/abstract/organization-management-view-models/image-url-view-model.interface";

@Component({
    selector: 'org-management-event',
    templateUrl: './organization-manadement-event-edit.component.html',
    styleUrls: ['./organization-manadement-event-edit.component.css']
})
export class OrganizationManadementEventEditComponent implements OnInit {
    private _idForCurrentEvent: number;
    private _event: IEventManagementViewModel;
    private _subscription: Subscription;
    private _errorMessage: string;

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
    public getInformationOfEvent(id: number): void {
        this._service.getOneEventById(id).subscribe(event => this._event = event);
    }

    /**
     * Updates event
     */
    private updateEvent(): void {
        this._service.updateEvent(this._event)
            .subscribe(() => this._router.navigate(['organization/events/' + this._event.organizationId]));
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
     * Deletes concrete image
     * @param currentImage
     */
    private deleteCurrentImage(currentImage: IImageModel): void {
        if (currentImage.id > 0) {
            this._service.deleteCurrentImage(currentImage.id)
                .subscribe(data => this.deleteImageFromList(currentImage.imageUrl),
                error => this._errorMessage = <any>error);
        }
        else {
            this.deleteImageFromList(currentImage.imageUrl);
        }
    }

    /**
     * Redirect to all events list
     */
    private redirectToAllEvents(): void {
        this._router.navigate(['organization/events/' + this._event.organizationId]);
    }

    ngDestroy(): void {
        this._subscription.unsubscribe();
    }
}