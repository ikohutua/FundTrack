import { Component, OnInit } from '@angular/core';
import { OrganizationsDetailsService } from "../../services/concrete/organization-management/organizations-details.service";
import { OrganizationGeneralViewModel } from "../../view-models/concrete/organization-general-view.model";


@Component({
    selector: 'all-organizations',
    templateUrl: './all-organizations.component.html',
    styleUrls: ['./all-organizations.component.css'],
    providers: [OrganizationsDetailsService]
})
export class AllOrganizationsComponent implements OnInit {
   

    private allOrganizations: OrganizationGeneralViewModel[];

    constructor(private _service: OrganizationsDetailsService) {
    }

    ngOnInit(): void {
        this.getAllOrganizations();
    }
   
    getAllOrganizations() {        
        this._service.getAllOrganizations().subscribe((data) => {
            this.allOrganizations = data;
        });
    }
}