import { Component, Output, EventEmitter, OnInit } from "@angular/core";
import { Router } from "@angular/router";
import { isBrowser } from "angular2-universal";
import * as key from '../../../shared/key.storage';
import { AuthorizeUserModel } from "../../../view-models/concrete/authorized-user-info-view.model";

@Component({
    selector: 'sidebar',
    templateUrl: './sidebar.component.html',
    styleUrls: ['./sidebar.component.css'],
})
export class SidebarComponent implements OnInit {
    private user: AuthorizeUserModel;
    //flag that verifies if user if logged in
    private userRole: string = null;
    //property for side bar visible mode
    private sideBarIsClosed: boolean = true;
    @Output() onOpen: EventEmitter<boolean> = new EventEmitter();

    constructor(private _router: Router) { }

    ngOnInit(): void
    {
        if (isBrowser)
        {
            if (localStorage.getItem(key.keyToken)) {
                this.user = JSON.parse(localStorage.getItem(key.keyModel)) as AuthorizeUserModel;
            }
        }
    }
   

   

    /**
     * Hide or show sidebar
     */
    private showSideBar(): void {
        if (this.sideBarIsClosed) {
            this.sideBarIsClosed = false;
            this.onOpen.emit(true);
        } else {
            this.sideBarIsClosed = true;
            this.onOpen.emit(false);
        }
    }

    /**
     * Redirect to all events in organization management page
     * @param id for organization
     */
    public redirectToAllEvents(idOrganization: number): void {
        this._router.navigate(['organization/events/' + idOrganization.toString()]);
    }

    /**
     * Redirect to 'add new event page' in organization management page
     * @param idOrganization
     */
    public redirectToAddEventPage(idOrganization: number): void {
        this._router.navigate(['organization/event/add/' + idOrganization.toString()]);
    }

    /**
     * Redirect to 'all requests page' in organization management page
     * @param idOrganization
     */
    public redirectToAllRequests(idOrganization: number): void {
        this._router.navigate(['organization/requests/' + idOrganization.toString()]);
    }

    /**
     * Redirect to 'manage requests page' in organization management page
     * @param idOrganization
     */
    public redirectToManageRequestPage(idOrganization: number): void {
        this._router.navigate(['organization/request/manage/' + idOrganization.toString() + "/0"]);
    }
}