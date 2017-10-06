import { Component, OnInit } from "@angular/core";
import { UserDonationViewModel } from "../../view-models/concrete/finance/donate/user-donation-view-model";
import { UserDonationFilteringViewModel } from "../../view-models/concrete/finance/donate/user-donation-filtering-view-model";
import { DonateService } from "../../services/concrete/finance/donate-money.service";
import { AuthorizeUserModel } from "../../view-models/concrete/authorized-user-info-view.model";
import { DatePipe } from '@angular/common';
import { isBrowser } from "angular2-universal";
import * as key from "../../shared/key.storage";
import { Subject } from "rxjs/Subject";
// Observable operators
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/debounceTime';
import 'rxjs/add/operator/distinctUntilChanged';
import { Observable } from "rxjs/Observable";

@Component({
    selector: 'my-donations',
    templateUrl: './my-donations.component.html',
    styleUrls: ['./my-donations.component.css'],
    providers: [DonateService, DatePipe]
})
export class MyDonationsComponent implements OnInit {

    private user: AuthorizeUserModel = new AuthorizeUserModel();
    private myDonations: UserDonationViewModel[] = new Array<UserDonationViewModel>();
    private filteringModel: UserDonationFilteringViewModel = new UserDonationFilteringViewModel();
    private UserDonationsTableHeaders: string[] = ['№ п/п', 'Організація', 'Призначення', 'Сума, ₴', 'Дата', 'Опис'];
    private inputMaxDate: Date = new Date();
    private isDataExist: boolean;
    private isFilteredDataExist: boolean = true;
    private showSpinner: boolean = false;
    constructor(private donateService: DonateService,
        private datePipe: DatePipe) {
    }
    ngOnInit(): void {
        this.showSpinner = true;
        this.filteringModel.id = 0;
        this.filteringModel.dateFrom = new Date();
        this.filteringModel.dateTo = new Date();

        if (isBrowser) {
            if (localStorage.getItem(key.keyToken)) {
                this.user = JSON.parse(localStorage.getItem(key.keyModel)) as AuthorizeUserModel;
            }
        };

        this.donateService.getUserDonations(this.user.id).subscribe(donation => {
            if (donation.length != 0) {
                this.isDataExist = true;
                this.myDonations = donation;
                this.filteringModel.dateFrom = this.myDonations[0].date;
            }
            else {
                this.isDataExist = false;
            }
            this.showSpinner = false;
        });
    }
    private donationWhenDateChanged() {
        this.donateService.getUserDonationsByDate(this.user.id, this.datePipe.transform(this.filteringModel.dateFrom, 'yyyy-MM-dd'), this.datePipe.transform(this.filteringModel.dateTo, 'yyyy-MM-dd'))
            .subscribe((outcomeData: UserDonationViewModel[]) => {
                if (outcomeData.length != 0) {
                    this.isFilteredDataExist = true;
                    this.myDonations = outcomeData;
                }
                else {
                    this.isFilteredDataExist = false;
                }
            })
    }
    public setBeginDate(beginDate: Date): void {
        this.filteringModel.dateFrom = beginDate;
        this.donationWhenDateChanged();

    }
    public setEndDate(endDate: Date): void {
        this.filteringModel.dateTo = endDate;
        this.donationWhenDateChanged();
    }
}
