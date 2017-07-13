import { Component, Output, EventEmitter } from "@angular/core";
import { Router } from "@angular/router";

@Component({
    selector: 'sidebar',
    templateUrl: './sidebar.component.html',
    styleUrls: ['./sidebar.component.css'],
})
export class SidebarComponent {

    constructor(private _router: Router) { }

    @Output() onOpen: EventEmitter<boolean> = new EventEmitter();

    //property for side bar visible mode
    private sideBarIsClosed: boolean = true;

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
}