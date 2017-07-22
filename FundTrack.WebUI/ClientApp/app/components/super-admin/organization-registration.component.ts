import { Component, Injectable, Input, Output, ElementRef, ViewChild, NgZone, OnInit} from '@angular/core';
import { IOrganizationRegistrationViewModel } from '../../view-models/abstract/organization-registration-view-model.interface';
import { OrganizationRegistrationService } from '../../services/concrete/organization-registration.service';
import { OrganizationRegistrationViewModel } from '../../view-models/concrete/organization-registration-view-model';
import { MapComponent } from '../../shared/components/map/map.component';
import { Observable } from "rxjs/Observable";
import { ModalComponent } from '../../shared/components/modal/modal-component';

@Component({
    selector: 'register-organization', 
    template: require('./organization-registration.component.html'),
    styleUrls: ['./organization-registration.component.css'], 
    providers:[MapComponent]
})

/**
 *Class to register new organization
*/
export class OrganizationRegistrationComponent implements OnInit {
    @ViewChild(ModalComponent)
    /**
    Modal component that contains address input controls
    **/
    public modal: ModalComponent;
    userError: string;
    nameError: string;
    address: string;
    addresses: string[];
    zoom: number = 30;
    organization: OrganizationRegistrationViewModel = new OrganizationRegistrationViewModel();
    constructor(private _registerService: OrganizationRegistrationService, private _map: MapComponent) {
        
    }

    ngOnInit() {
        this._map.zoom = this.zoom;
    }

    /**
     * Registers new organization
     */
    registerOrganization() {
        this.userError = " ";
        this.nameError = " ";
        this.organization.country = "Україна";
        console.log(this.organization);
        this._registerService.registerOrganization(this.organization)
            .subscribe(org => { this.userError = org.userError; this.nameError = org.nameError });
    }

    getAddresses() {
      
    }

    openModal() {
        this.modal.show();
    }
}