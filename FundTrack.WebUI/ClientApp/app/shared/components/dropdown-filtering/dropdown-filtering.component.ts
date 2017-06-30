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

    private _errorMessage: string;
    public organizations: IOrganizationForFiltering[];
    public filterBy: string = '';
    public selectedOrganizationName: string = null;

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

    /**
     * gets a name of selected organization in dropdown list of organizations
     */
    private onSelect(org: IOrganizationForFiltering): void {
        this.selectedOrganizationName = org.name;
    }

    private onSelectAll(): void{
        this.selectedOrganizationName = null;
    }

}