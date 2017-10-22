import { Component, OnInit, OnChanges } from "@angular/core";
import { DatePipe } from "@angular/common";
import { ShowRequestedItemService } from "../../services/concrete/showrequesteditem.service";
import { OrganizationManagementRequestService } from "../../services/concrete/organization-management/organization-management-request.service";
import { ActivatedRoute } from "@angular/router";
import { ReportFilterQueryViewModel } from "../../view-models/concrete/report-filter-query-view-model";
import { UsersDonationsReportDataViewModel } from "../../view-models/concrete/users-donations-view.model";

import * as _ from 'underscore';
import * as commonMessages from '../../shared/common-message.storage';

export class Pager {
    public totalItems: number;
    public currentPage: number;
    public pageSize: number;
    public totalPages: number;
    public startPage: number;
    public endPage: number;
    public startIndex: number;
    public endIndex: number;
    public pages: number[];

    public getPager(totalItems: number, currentPage: number = 1, pageSize: number = 10): Pager {
        this.totalItems = totalItems;
        this.currentPage = currentPage;
        this.pageSize = pageSize;
        // calculate total pages
        this.totalPages = Math.ceil(this.totalItems / this.pageSize);

        if (this.totalPages <= 10) {
            // less than 10 total pages so show all
            this.startPage = 1;
            this.endPage = this.totalPages;
        } else {
            // more than 10 total pages so calculate start and end pages
            if (this.currentPage <= 6) {
                this.startPage = 1;
                this.endPage = 10;
            } else if (this.currentPage + 4 >= this.totalPages) {
                this.startPage = this.totalPages - 9;
                this.endPage = this.totalPages;
            } else {
                this.startPage = this.currentPage - 5;
                this.endPage = this.currentPage + 4;
            }
        }

        // calculate start and end item indexes
        this.startIndex = (this.currentPage - 1) * this.pageSize;
        this.endIndex = Math.min(this.startIndex + this.pageSize - 1, this.totalItems - 1);

        // create an array of pages to ng-repeat in the pager control
        this.pages = _.range(this.startPage, this.endPage + 1);

        return this;
    };
}

@Component({
    templateUrl: './users-donations-report.component.html',
    styleUrls: ['./users-donations-report.component.css'],
    providers: [DatePipe, ShowRequestedItemService, OrganizationManagementRequestService]
})

export class UsersDonationsReportComponent implements OnInit {
    reportData: Array<UsersDonationsReportDataViewModel> = new Array<UsersDonationsReportDataViewModel>();
    pagedReportItems: Array<UsersDonationsReportDataViewModel>;
    reportModel: ReportFilterQueryViewModel = new ReportFilterQueryViewModel();
    inputMaxDate: Date = new Date();
    inputMinDate: Date = new Date("2000-01-01");
    isDataAvailable: boolean;
    isGettingDataStarted: boolean = true;
    totalReportItemsCount: number;
    pager: Pager = new Pager();

    constructor(private _service: ShowRequestedItemService,
        private datePipe: DatePipe,
        private _route: ActivatedRoute) {
    }

    ngOnInit(): void {
        this.setReportModelIdFromUrl();
        this.reportModel.dateFrom = new Date();
        this.reportModel.dateFrom.setMonth(this.reportModel.dateFrom.getMonth() - 1);
        this.reportModel.dateTo = new Date();
        this.generateReport();
    }

    setReportModelIdFromUrl() {
        this._route.params.subscribe(params => {
            this.reportModel.id = params['id'];
            if (params['id'] != null) {
                this.reportModel.id = params['id'];
            }
        }, error => {
            this.showErrorMessage(<any>error);
        });
    }

    setBeginDate(value: Date) {
        this.reportModel.dateFrom = value;
        this.generateReport();
    }
    setEndDate(value: Date) {
        this.reportModel.dateTo = value;
        this.generateReport();
    }

    generateReport() {
        if (!this.isRequestDataValid()) {
            alert(commonMessages.invalidDate);
            return;
        }

        this._service.getCountOfUsersDonationsReportItems(this.reportModel.id,
            this.prepareDate(this.reportModel.dateFrom),
            this.prepareDate(this.reportModel.dateTo))
            .subscribe(res => {
                this.totalReportItemsCount = res;
                this.setPage(1);
            }, error => {
                this.showErrorMessage(error);
            });
    }

    setPage(page: number) {
        if (page < 1) {
            return;
        }
        this.isGettingDataStarted = true;
        this.isDataAvailable = false;

        // get pager object to help implement pagination
        this.pager = new Pager().getPager(this.totalReportItemsCount, page);

        this._service.getUsersDonationsPaginatedReport(this.reportModel.id,
            this.prepareDate(this.reportModel.dateFrom),
            this.prepareDate(this.reportModel.dateTo),
            page, this.pager.pageSize)
            .subscribe(res => {
                this.pagedReportItems = res;
                this.isDataAvailable = (res != undefined && res.length > 0);
                this.isGettingDataStarted = false;
            },
            error => {
                this.showErrorMessage(error);
                this.isGettingDataStarted = false;
            });
    }


    isRequestDataValid(): boolean {
        console.log(this.reportModel.dateFrom);
        this.reportModel.dateFrom = new Date(this.datePipe.transform(this.reportModel.dateFrom));
        this.reportModel.dateTo = new Date( this.datePipe.transform(this.reportModel.dateTo));

        console.log(this.reportModel.dateFrom);
        console.log(this.reportModel.dateTo);

        let a = this.reportModel.id > 0 && (this.reportModel.dateTo <= this.inputMaxDate && this.reportModel.dateTo >= this.reportModel.dateFrom) && this.reportModel.dateFrom <= this.reportModel.dateTo;

        console.log(a);
        console.log(this.reportModel.id > 0);
        console.log(this.reportModel.dateTo.getDate() <= this.inputMaxDate.getDate());
        console.log(this.reportModel.dateTo.getDate() >= this.reportModel.dateFrom.getDate());
        console.log(this.reportModel.dateFrom.getDate() <= this.reportModel.dateTo.getDate());
        return a;
    }
    prepareDate(date: Date): string {
        return this.datePipe.transform(date, 'yyyy-MM-dd')
    }

    showErrorMessage(message: string) {
        alert(commonMessages.anErrorOccurred + "\n" + message);
    }
}