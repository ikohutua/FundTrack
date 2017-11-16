import { Component, Injectable, Input, Output, ElementRef, ViewChild, NgZone, OnInit } from '@angular/core';
import { IOrganizationRegistrationViewModel } from '../../view-models/abstract/organization-registration-view-model.interface';
import { OrganizationRegistrationService } from '../../services/concrete/organization-registration.service';
import { OrganizationRegistrationViewModel } from '../../view-models/concrete/organization-registration-view-model';
import { MapComponent } from '../../shared/components/map/map.component';
import { Observable } from "rxjs/Observable";
import { ModalComponent } from '../../shared/components/modal/modal-component';
import * as message from '../../shared/common-message.storage';
import * as defaultConfig from '../../shared/default-configuration.storage';
import { Image } from "../../view-models/concrete/image.model";
import { ImputImageService } from "../../shared/input-image-service";


@Component({
    selector: 'register-organization',
    template: require('./organization-registration.component.html'),
    styleUrls: ['./organization-registration.component.css'],
    providers: [MapComponent]
})

/**
 *Class to register new organization
*/
export class OrganizationRegistrationComponent implements OnInit {
    @ViewChild('success')
    /**
    Modal component that contains 
    **/
    public modal: ModalComponent;
    userError: string;
    nameError: string;
    address: string;
    addresses: string[];
    organization: OrganizationRegistrationViewModel = new OrganizationRegistrationViewModel();
    orgName: string;

    currentFile: File;
    defaultLogoUrl: string;
    newLogo: Image;
    isNewLogoAvailable: boolean = false;
    errorMessage: string;
    isError: boolean = false;


    constructor(private _registerService: OrganizationRegistrationService, private _map: MapComponent) {

    }

    ngOnInit() {
        this.defaultLogoUrl = defaultConfig.defaultOrganizationLogoUrl;
    }

    /**
     * Registers new organization
     */
    registerOrganization() {
        this.organization.country = "Україна";
        if (this.isNewLogoAvailable) {
            this.organization.logoBase64Code = this.newLogo.base64Data;
            this.organization.logoImageExtension = this.newLogo.imageExtension;
        }
        this._registerService.registerOrganization(this.organization)
            .subscribe(org => {
                this.userError = org.userError;
                this.nameError = org.nameError;
                if (this.userError == null && this.nameError == null) {
                    this.orgName = org.name;
                    this.modal.show();
                }
            });
    }
    getAddresses() {

    }

    openModal() {
        this.modal.show();
    }

    handleInputChange(startFile: any) {

        if (!startFile.target.files.length) {
            return;
        }
        this.isError = false;
        let imgInpServ: ImputImageService = new ImputImageService();

        imgInpServ.UploadImageFromFile(startFile)
            .then((res) => {
                this.newLogo = res;
                this.isNewLogoAvailable = true;
            })
            .catch((err) => {
                this.imageErrorMessage(err.message);
            });
    }

    imageErrorMessage(message: string) {
        this.errorMessage = message;
        this.isError = true;
    }

    clearImage() {
        this.isNewLogoAvailable = false;
    }
}