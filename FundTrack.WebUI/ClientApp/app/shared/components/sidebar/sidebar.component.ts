import { Component, Output, EventEmitter, OnInit, AfterContentChecked, DoCheck, OnChanges } from "@angular/core";
import { Router } from "@angular/router";
import { isBrowser } from "angular2-universal";
import * as key from '../../../shared/key.storage';
import { AuthorizeUserModel } from "../../../view-models/concrete/authorized-user-info-view.model";
import { StorageService } from "../../item-storage-service";
import { UserResponseService } from "../../../services/concrete/organization-management/user-responses.service";


@Component({
    selector: 'sidebar',
    templateUrl: './sidebar.component.html',
    styleUrls: ['./sidebar.component.css'],
    providers: [UserResponseService]     
})



export class SidebarComponent implements OnInit {

    private allOrganizationsComponentUrl: string = 'organization/allOrganizations';


    private user: AuthorizeUserModel;
    //flag that verifies if user is logged in
    private userRole: string = null;
    //property for side bar visible mode
    private sideBarIsClosed: boolean = true;
    private newUserResponse: number;
    @Output() onOpen: EventEmitter<boolean> = new EventEmitter();

    constructor(private _router: Router,
        private _userResponseService: UserResponseService,
        private _storage: StorageService) { }

    ngOnInit(): void {
        if (isBrowser) {
            if (localStorage.getItem(key.keyToken)) {
                this.user = JSON.parse(localStorage.getItem(key.keyModel)) as AuthorizeUserModel;
               
                this._userResponseService.getUserResponseWithNewStatus(this.user.orgId)
                    .subscribe(count => {
                        this.newUserResponse = count;
                        sessionStorage.setItem("NewResponse", count.toString());
                    });
            }
        }
        this._storage.getNavChangeEmitter().subscribe(count => this.newUserResponse = count);
        this._storage.getAuthorizeUserEmitter().subscribe(user => this.user = user);
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
    public redirectToAllEvents(): void {
        this._router.navigate(['organization/events/' + this.user.orgId]);
    }

    /**
     * Redirect to 'add new event page' in organization management page
     * @param idOrganization
     */
    public redirectToAddEventPage(): void {
        this._router.navigate(['organization/event/add/' + this.user.orgId]);
    }

    /**
     * Redirect to 'all requests page' in organization management page
     * @param idOrganization
     */
    public redirectToAllRequests(idOrganization: number): void {
        this._router.navigate(['organization/requests/' + idOrganization]);
    }

    /**
    *Redirect to 'all response page' in organization managment page
    * @param idOrganization
    */
    public redirectToAllResponsesPage(): void {
        this._router.navigate(['organization/request/response/' + this.user.orgId.toString()]);
    }

    public redirectToPage(url : string): void {
        this._router.navigate([url]);
    }

    /**
     * Redirect to 'all requests page' in organization management page
     * @param idOrganization
     */
    public redirectToShowAllRequests(): void {
        this.showSideBar();
        this._router.navigate(['home/allrequests']);
    }

    /**
     * Redirect to 'manage requests page' in organization management page
     * @param idOrganization
     */
    public redirectToManageRequestPage(idOrganization: number): void {
        this._router.navigate(['organization/request/manage/' + idOrganization.toString()]);
    }

   
    public redirectToReportsPage(): void {
        this._router.navigate(['home/report']);
    }

    public redirectToEditOrganizationPage(): void {
        this.showSideBar();
        this._router.navigate(['organization/edit/' + this.user.orgId.toString()]);
    }

    public redirectToHomePage(): void {
        this._router.navigate(['home/allevents']);
    }

    public redirectToTargetManagementPage(): void {
        this._router.navigate(['organization/targets/' + this.user.orgId.toString()]);
    }

    public redirectToStatisticsPage(): void {
        this._router.navigate(['organization/statistics/' + this.user.orgId.toString()]);
    }

    public redirectToUsersDonationsReportsPage() {
        this._router.navigate(['organization/usersdonationsreports/' + this.user.orgId.toString()]);
    }

    public redirectToCommonDonationsReportsPage() {
        this._router.navigate(['organization/commondonationsreports/' + this.user.orgId.toString()]);
    }
}