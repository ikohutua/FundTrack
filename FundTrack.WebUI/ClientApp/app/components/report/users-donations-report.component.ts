import { Component, OnInit, OnChanges } from "@angular/core";
import { DatePipe } from "@angular/common";
import { ShowRequestedItemService } from "../../services/concrete/showrequesteditem.service";
import { OrganizationManagementRequestService } from "../../services/concrete/organization-management/organization-management-request.service";
import { ActivatedRoute } from "@angular/router";
import { ReportFilterQueryViewModel } from "../../view-models/concrete/report-filter-query-view-model";
import { UsersDonationsReportDataViewModel } from "../../view-models/concrete/users-donations-view.model";

import * as _ from 'underscore';
import * as commonMessages from '../../shared/common-message.storage';

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
    totalReportItemsCount: number;

    //----------------------------------------------
    // pager object
    pager: any = {};

    setPage(page: number) {
        if (page < 1) {
            return;
        }
        // get pager object from service
        this.pager = this.getPager(this.totalReportItemsCount, page);

        this._service.getUsersDonationsPaginatedReport(this.reportModel.id,
            this.prepareDate(this.reportModel.dateFrom),
            this.prepareDate(this.reportModel.dateTo),
            page, this.pager.pageSize)
            .subscribe(res => {
                this.pagedReportItems = res;
                this.isDataAvailable = res != undefined && res.length > 0 ? true : false;
            },
            error => {
                this.isDataAvailable = false;
                this.showErrorMessage(error);
            });
    }

    getPager(totalItems: number, currentPage: number = 1, pageSize: number = 10) {
        // calculate total pages
        let totalPages = Math.ceil(totalItems / pageSize);

        let startPage: number, endPage: number;
        if (totalPages <= 10) {
            // less than 10 total pages so show all
            startPage = 1;
            endPage = totalPages;
        } else {
            // more than 10 total pages so calculate start and end pages
            if (currentPage <= 6) {
                startPage = 1;
                endPage = 10;
            } else if (currentPage + 4 >= totalPages) {
                startPage = totalPages - 9;
                endPage = totalPages;
            } else {
                startPage = currentPage - 5;
                endPage = currentPage + 4;
            }
        }

        // calculate start and end item indexes
        let startIndex = (currentPage - 1) * pageSize;
        let endIndex = Math.min(startIndex + pageSize - 1, totalItems - 1);

        // create an array of pages to ng-repeat in the pager control
        let pages = _.range(startPage, endPage + 1);

        // return object with all pager properties required by the view
        return {
            totalItems: totalItems,
            currentPage: currentPage,
            pageSize: pageSize,
            totalPages: totalPages,
            startPage: startPage,
            endPage: endPage,
            startIndex: startIndex,
            endIndex: endIndex,
            pages: pages
        };
    }

    //----------------------------------------------

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

    generateReport() {
        if (this.isRequestDataValid()) {
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

    isRequestDataValid(): boolean {
        return this.reportModel.id > 0 && this.reportModel.dateTo <= this.inputMaxDate && this.reportModel.dateFrom <= this.reportModel.dateTo;
    }
    prepareDate(date: Date): string {
        return this.datePipe.transform(date, 'yyyy-MM-dd')
    }

    showErrorMessage(message: string) {
        alert(commonMessages.anErrorOccurred+"\n" + message);
    }

    setBeginDate(value: Date) {
        this.reportModel.dateFrom = value;
        this.generateReport();
    }
    setEndDate(value: Date) {
        this.reportModel.dateTo = value;
        this.generateReport();
    }
}