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
    private searchTerms = new Subject<string>();
    constructor(private donateService: DonateService,
        private datePipe: DatePipe) {

    }
    search(term: string): void {
        this.searchTerms.next(term);
    }
    ngOnInit(): void {
        this.filteringModel.id = 0;
        this.filteringModel.dateFrom = new Date();
        this.filteringModel.dateTo = new Date();

        if (isBrowser) {
            if (localStorage.getItem(key.keyToken)) {
                this.user = JSON.parse(localStorage.getItem(key.keyModel)) as AuthorizeUserModel;
            }
        };

        this.donateService.getUserDonations(this.user.id).subscribe(donation => {
            this.myDonations = donation;
            this.filteringModel.dateFrom = this.myDonations[0].date;
            //this.myDonations = this.searchTerms
            //    .debounceTime(300)
            //    .distinctUntilChanged()
            //    .switchMap()
            //    .catch(error => {
            //        console.log(error);
            //        return Observable.of<UserDonationViewModel[]>([]);
            //    });
        });

    }
    private donationWhenDateChanged() {
        this.donateService.getUserDonationsByDate(this.user.id, this.datePipe.transform(this.filteringModel.dateFrom, 'yyyy-MM-dd'), this.datePipe.transform(this.filteringModel.dateTo, 'yyyy-MM-dd'))
            .subscribe((outcomeData: UserDonationViewModel[]) => {
                this.myDonations = outcomeData;
            })
    }
    public setBeginDate(beginDate: Date): void {
        debugger;
        this.filteringModel.dateFrom = beginDate;
        this.donationWhenDateChanged();

    }
    public setEndDate(endDate: Date): void {
        this.filteringModel.dateTo = endDate;
        this.donationWhenDateChanged();
    }

}
