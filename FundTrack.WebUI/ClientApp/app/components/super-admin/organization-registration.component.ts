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
    constructor(private _registerService: OrganizationRegistrationService, private _map: MapComponent) {
        
    }

    ngOnInit() {
       
    }

    /**
     * Registers new organization
     */
    registerOrganization() {
        //this.userError = " ";
        //this.nameError = " ";
        this.organization.country = "Україна";
        console.log(this.organization);
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
}