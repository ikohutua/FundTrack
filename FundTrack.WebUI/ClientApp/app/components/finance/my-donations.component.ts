import { Component, OnInit } from "@angular/core";
import { UserDonationViewModel } from "../../view-models/concrete/finance/donate/user-donation-view-model";
import { UserDonationFilteringViewModel } from "../../view-models/concrete/finance/donate/user-donation-filtering-view-model";
import { DonateService } from "../../services/concrete/finance/donate-money.service";
import { AuthorizeUserModel } from "../../view-models/concrete/authorized-user-info-view.model";
import { DatePipe } from '@angular/common';
import { isBrowser } from "angular2-universal";
import * as key from "../../shared/key.storage";
import { Subject } from "rxjs/Subject";
import 'rxjs/add/operator/catch';
import * as moment from "moment/moment";
import { DatePeriod } from "../../shared/components/date-presets/date-period-class";

@Component({
    selector: 'my-donations',
    templateUrl: './my-donations.component.html',
    styleUrls: ['./my-donations.component.css'],
})

export class MyDonationsComponent implements OnInit {

    private readonly DATE_FORMAT = "YYYY-MM-DD";
    private user: AuthorizeUserModel = new AuthorizeUserModel();
    private myDonations: UserDonationViewModel[] = new Array<UserDonationViewModel>();
    private filteringModel: UserDonationFilteringViewModel = new UserDonationFilteringViewModel();
    private UserDonationsTableHeaders: string[] = ['№ п/п', 'Організація', 'Призначення', 'Сума, ₴', 'Дата', 'Опис'];
    private inputMaxDate: Date = new Date();
    private isDataExist: boolean;
    private isFilteredDataExist: boolean = true;
    private showSpinner: boolean = true;
    constructor(private donateService: DonateService,
        private datePipe: DatePipe) {
    }
    ngOnInit(): void {
        this.showSpinner = true;
        this.filteringModel.id = 0;
        this.filteringModel.dateFrom = moment().subtract(1, "month").format(this.DATE_FORMAT);
        this.filteringModel.dateTo = moment().format(this.DATE_FORMAT);

        if (isBrowser) {
            if (localStorage.getItem(key.keyToken)) {
                this.user = JSON.parse(localStorage.getItem(key.keyModel)) as AuthorizeUserModel;
            }
            this.donateService.getUserDonationsByDate(this.user.id, this.filteringModel.dateFrom, this.filteringModel.dateTo)
                .subscribe(donation => {
                if (donation.length != 0) {
                    this.isDataExist = true;
                    this.myDonations = donation;
                }
                else {
                    this.isDataExist = false;
                }
                this.showSpinner = false;
            });
        };
    }
    private donationWhenDateChanged() {
        this.donateService.getUserDonationsByDate(this.user.id, this.filteringModel.dateFrom,this.filteringModel.dateTo)
            .subscribe((outcomeData: UserDonationViewModel[]) => {
                if (outcomeData.length != 0) {
                    this.isFilteredDataExist = true;
                    this.myDonations = outcomeData;
                }
                else {
                    this.isFilteredDataExist = false;
                }
                this.showSpinner = false;
            })
    }

    onDatePeriodChange(value: DatePeriod) {
        this.filteringModel.dateFrom = value.dateFrom;
        this.filteringModel.dateTo = value.dateTo;
        this.donationWhenDateChanged();
    }
}
