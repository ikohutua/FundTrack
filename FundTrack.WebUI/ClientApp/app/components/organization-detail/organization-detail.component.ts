import { Component, OnInit, OnDestroy } from "@angular/core";
import { OrganizationsDetailsService } from "../../services/concrete/organization-management/organizations-details.service";
import { ActivatedRoute, Router } from "@angular/router";
import { OrganizationDetailViewModel } from "../../view-models/concrete/organization-detail-view.model";
import { OrganizationGeneralViewModel } from "../../view-models/concrete/organization-general-view.model";


@Component({
    selector: 'organization-detail',
    templateUrl: './organization-detail.component.html',
    styleUrls: ['./organization-detail.component.css'],
    providers: [OrganizationsDetailsService]
})
export class OrganizationDetailComponent implements OnInit, OnDestroy {

    subscriber: any;
    organizationDetail: OrganizationDetailViewModel;
    showUsersSpinner: boolean = false;
    orgId: number;

    // Urls to other components
    allOrganizationComponentUrl: string = '/organization/allOrganizations';
    requestsComponentUrl: string = '/home/allrequests/';
    eventsComponentUrl: string = '/home/allevents/';
    donateComponentUrl: string = '/finance/donate/';
    reportsComponentUrl: string = '/home/report/';

    constructor(private route: ActivatedRoute, private service: OrganizationsDetailsService, private router: Router) {
        this.showUsersSpinner = true;
    }

    ngOnInit(): void {
        this.subscriber = this.route.params.subscribe(params => {
            if (isNaN(+params["id"]) || +params["id"] < 0) {
                this.redirectToOtherPage(this.allOrganizationComponentUrl);
            }
            this.orgId = +params["id"];
            this.getOrganizationDetail(this.orgId);
        });
    }

    getOrganizationDetail(orgId: number) {
        this.service.getOrganizationDetail(orgId).subscribe((data) => {
            this.organizationDetail = data;
            this.showUsersSpinner = false;
        },
            (error) => {
                this.redirectToOtherPage(this.allOrganizationComponentUrl);
            });
    }

    redirectToOtherPage(url: string) {
        this.router.navigate([url]);
    }

    ngOnDestroy(): void {
        this.subscriber.unsubscribe;
    }
}