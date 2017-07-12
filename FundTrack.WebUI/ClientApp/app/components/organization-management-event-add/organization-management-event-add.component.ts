import { Component } from "@angular/core";
import { IEventManagementViewModel } from "../../view-models/abstract/organization-management-view-models/event-management-view-model.interface";
import { ActivatedRoute, Router } from "@angular/router";
import { OrganizationManagementEventsService } from "../../services/concrete/organization-management/organization-management-events.service";
import { Subscription } from "rxjs/Subscription";
import { EventManagementViewModel } from "../../view-models/concrete/event-management-view-model";
import { ImageModel } from "../../view-models/concrete/image-url-view-model";
import { AmazonUploadComponent } from "../../shared/components/amazonUploader/amazon-upload.component";

@Component({
    selector: 'org-management-add',
    templateUrl: './organization-management-event-add.component.html',
    styleUrls: ['./organization-management-event-add.component.css']
})

export class OrganizationManagementEventAddComponent {
    private _idForCurrentOrganization: number;
    private _event: IEventManagementViewModel;
    private _subscription: Subscription;
    private _errorMessage: string;
    private _image: ImageModel;
    private _uploader: AmazonUploadComponent = new AmazonUploadComponent();

    /**
     * @constructor
     * @param _route
     * @param _service
     */
    public constructor(private _route: ActivatedRoute, private _router: Router, private _service: OrganizationManagementEventsService) { }

    ngOnInit(): void {
        this._subscription = this._route.parent.params.subscribe(
            params => {
                this._idForCurrentOrganization = +params["id"];
                this._event = new EventManagementViewModel();
            });
    }

    /**
     * Adds new event
     */
    private addNewEvent() {
        debugger;
        this._event.organizationId = this._idForCurrentOrganization;
        this._event.createDate = new Date(Date.now());
        this._image;
        this._service.addNewEvent(this._event).subscribe(
            ev => { this._event = ev },
            error => { this._errorMessage = <any>error });
    }

    /**
     * Saves passed file in Amazon Web Storage
     * @param fileInput: file to be saved in AWS
     */
    private saveFileInAws(fileInput: any): void {
        var that = this;
        this._image = new ImageModel();
        this._image.imageUrl = '';
        var maxFileSize = 4000000;
        let file = fileInput.target.files[0];
        let uploadedFileName = file.name;
        if (file.size != null && file.size < maxFileSize) {
            this._uploader.UploadImageToAmazon(file, uploadedFileName).then(function (data) {
                that._image.imageUrl = data.Location;
            })
        }
        else {
            alert('Розмр файлу не може перевищувати ' + Math.ceil(maxFileSize / 1000000) + 'МБ');
        }
    }
}