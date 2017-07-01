import { Component, OnInit, EventEmitter, Output } from "@angular/core";
import { IOrganizationForFiltering } from "../../../view-models/abstract/organization-for-filtering.interface";
import { OrganizationDropdownService } from "../../../services/concrete/organization-dropdown.service";

@Component({
    selector: 'dropdown-org',
    templateUrl: './dropdown-filtering.component.html',
    styleUrls: ['./dropdown-filtering.component.css'],
    providers: [OrganizationDropdownService]
})

export class DropdownOrganizationsComponent implements OnInit {

    public filterBy: string;

    private _errorMessage: string;
    private _organizations: IOrganizationForFiltering[];
    private _selectedOrganizationName: string;

    /**
     * calls getOrganizationsList()
     */
    ngOnInit(): void {
        this.getOrganizationsList();
    }

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
            .subscribe(organizations => this._organizations = organizations,
            error => this._errorMessage = <any>error);
    }

    /**
     * gets a name of selected organization in dropdown list of organizations
     * @param IOrganizationForFiltering
     */
    private onSelect(organization: IOrganizationForFiltering): void {
        this._selectedOrganizationName = organization.name;
    }

    private onSelectAll(): void {
        this._selectedOrganizationName = null;
    }
}