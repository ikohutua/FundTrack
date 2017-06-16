import { Component, OnInit } from "@angular/core";
import { OrganizationDropdownService } from "../../../services/organization-dropdown.service";
import { IOrganizationsForLayout } from "../../../viewModels/Abstract/IOrganizationsForLayout";

@Component({
    selector: 'dropdown-org',
    templateUrl: './dropdown-filtering.component.html',
    styleUrls: ['./dropdown-filtering.component.css'],
    providers: [OrganizationDropdownService]
})

export class DropdownOrganizationsComponent implements OnInit {

    private _errorMessage: string;
    public organizations: IOrganizationsForLayout[];
    public filterBy: string = '';

    /**
     * @constructor
     * @param _service
     */
    constructor(private _service: OrganizationDropdownService) { }

    /**
     * gets list of organizations from service
     */
    getOrganizationsList(): void {
        this._service.getOrganizations()
            .subscribe(organizations => this.organizations = organizations,
            error => this._errorMessage = <any>error);
    }

    /**
     * calls getOrganizationsList()
     */
    ngOnInit(): void {
        this.getOrganizationsList();
    }
}