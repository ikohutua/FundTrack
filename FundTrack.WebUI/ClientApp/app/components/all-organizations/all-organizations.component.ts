import { Component, OnInit } from '@angular/core';
import { OrganizationsDetailsService } from "../../services/concrete/organization-management/organizations-details.service";
import { OrganizationGeneralViewModel } from "../../view-models/concrete/organization-general-view.model";
import { Router } from "@angular/router";


@Component({
    selector: 'all-organizations',
    templateUrl: './all-organizations.component.html',
    styleUrls: ['./all-organizations.component.css'],
    providers: [OrganizationsDetailsService]
})
export class AllOrganizationsComponent implements OnInit {


    private allOrganizations: OrganizationGeneralViewModel[];
    private filterBy: string;

    constructor(private _service: OrganizationsDetailsService, private router: Router) {
    }

    ngOnInit(): void {
        this.getAllOrganizations();
    }

    getAllOrganizations() {
        this._service.getAllOrganizations().subscribe((data) => {
            this.allOrganizations = data;
        });
    }

    public redirectToOrganizationDetailPage(id: number): void {
        this.router.navigate(['/organization/organizationDetail/' + id]);
    }
}