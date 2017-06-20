import { Component, OnInit } from "@angular/core";
import { IOrganizationForLayout } from "../../../view-models/abstract/organization-for-layout.interface";
import { OrganizationDropdownService } from "../../../services/concrete/organization-dropdown.service";
import { Service } from "../../../services/abstract/service.abstract-class";

@Component({
    selector: 'dropdown-org',
    templateUrl: './dropdown-filtering.component.html',
    styleUrls: ['./dropdown-filtering.component.css'],
    providers: [OrganizationDropdownService]
})

export class DropdownOrganizationsComponent implements OnInit {

    private _errorMessage: string;
    public organizations: IOrganizationForLayout[];
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
        this._service.getCollection()
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