import { Component, OnInit } from "@angular/core";
import { ActivatedRoute } from "@angular/router";
import { Subscription } from 'rxjs/Subscription';
import { OrganizationGeneralViewModel } from '../../view-models/concrete/organization-general-view.model';
import { OrganizationGetGeneralInfoService } from '../../services/concrete/organization-management/organization-get-general-info.service';


@Component({
    selector: 'org-management',
    templateUrl: './organization-management.component.html',
    styleUrls: ['./organization-management.component.css'],
    providers: [OrganizationGetGeneralInfoService]
})
export class OrganizationManagementComponent implements OnInit {
    private _organizationId: number;
    private _organization: OrganizationGeneralViewModel;
    private _subscription: Subscription;
    constructor(private _router: ActivatedRoute, private _getGeneralInfoService: OrganizationGetGeneralInfoService) {}

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

    ngOnInit() {
        this._subscription = this._router.params.subscribe(params => {
            this._organizationId = +params['id'];
            this.getInformationOfOrganization(this._organizationId);
        });
    }

    /**
     * gets information about organization by its id
     * @param id
     */
    private getInformationOfOrganization(id: number): void {
        this._getGeneralInfoService.getById(id, 'api/OrganizationProfile/GetInformationById')
            .subscribe(model => this._organization = model)
    }
    
    ngDestroy(): void {
        this._subscription.unsubscribe();
    }
}