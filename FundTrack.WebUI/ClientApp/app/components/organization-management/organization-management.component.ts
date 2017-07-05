import { Component } from "@angular/core";

@Component({
    selector: 'org-management',
    templateUrl: './organization-management.component.html',
    styleUrls: ['./organization-management.component.css']
})
export class OrganizationManagementComponent {
    //property for side bar visible mode
    private sideBarIsClosed: boolean = true;

    //hide or show side bar
    private showSideBar(): void {
        if (this.sideBarIsClosed) {
            this.sideBarIsClosed = false;
        } else {
            this.sideBarIsClosed = true;
        }
    }
}