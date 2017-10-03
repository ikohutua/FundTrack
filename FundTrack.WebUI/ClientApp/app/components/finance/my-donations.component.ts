import { Component, OnInit } from "@angular/core";
import { UserDonationViewModel } from "../../view-models/concrete/finance/donate/user-donation-view-model";
import { DonateService } from "../../services/concrete/finance/donate-money.service";
import { AuthorizeUserModel } from "../../view-models/concrete/authorized-user-info-view.model";
import { isBrowser } from "angular2-universal";
import * as key from "../../shared/key.storage";

@Component({
    selector: 'my-donations',
    templateUrl: './my-donations.component.html',
    styleUrls: ['./my-donations.component.css'],
    providers:[DonateService]
})
export class MyDonationsComponent implements OnInit {
    private user: AuthorizeUserModel = new AuthorizeUserModel();
    private myDonations: UserDonationViewModel[];

    constructor(private donateService: DonateService) {

    }

    ngOnInit(): void {
        if (isBrowser) {
            if (localStorage.getItem(key.keyToken)) {
                this.user = JSON.parse(localStorage.getItem(key.keyModel)) as AuthorizeUserModel;
            }
        };
        debugger;
        this.donateService.getUserDonations(this.user.id).subscribe(donation => {
            this.myDonations = donation;
            alert(this.myDonations.values());
        });
        
    }

}
