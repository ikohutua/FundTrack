import { Component, OnInit, EventEmitter, Output, AfterContentChecked } from "@angular/core";
import { IOrganizationForFiltering } from "../../../view-models/abstract/organization-for-filtering.interface";
import { OrganizationDropdownService } from "../../../services/concrete/organization-dropdown.service";
import { Router, ActivatedRoute } from "@angular/router";
import { isBrowser } from 'angular2-universal';

@Component({
    selector: 'dropdown-org',
    templateUrl: './dropdown-filtering.component.html',
    styleUrls: ['./dropdown-filtering.component.css'],
    providers: [OrganizationDropdownService]
})

export class DropdownOrganizationsComponent implements OnInit, AfterContentChecked {
    //for organization-list.pipe
    public filterBy: string;
    public activateRoute: string;
    private _errorMessage: string;
    private _organizations: IOrganizationForFiltering[];
    private _selectedOrganizationName: string;
    private _selectedOrganizationId: number;

    /**
     * calls getOrganizationsList()
     */
    ngOnInit(): void {
        this.getOrganizationsList();
    }

    ngAfterContentChecked(): void {
        if (!this._router.url.includes(this.activateRoute)) {
            this._selectedOrganizationName = "Список організацій";
        }
    }


    /**
     * @constructor
     * @param _service
     */
    constructor(private _service: OrganizationDropdownService,
        private _router: Router,
        private _activatedRoute: ActivatedRoute) { }

    /**
     * gets list of organizations from service
     */
    getOrganizationsList(): void {
        this._service.getCollection()
            .subscribe(organizations => this._organizations = organizations,
            error => this._errorMessage = <any>error);
    }

    /**
     * gets a name of selected organization in dropdown list 
     * @param IOrganizationForFiltering
     */
    public onSelect(org?: IOrganizationForFiltering): void {
        let paths: string[] = ['allevents', 'allrequests'];
        for (let i = 0; i < paths.length; ++i) {
            if (this._router.url.includes(paths[i])) {
                this.activateRoute = paths[i]+'/';
                if (org) {
                    this._selectedOrganizationName = org.name;
                    this._router.navigate(['/home/' + paths[i], org.id]);
                }
                else {
                    this._selectedOrganizationName = null;
                    this._router.navigate(['/home/' + paths[i]]);
                }
            }
        }
    }
}