import { Component, OnInit, OnChanges } from "@angular/core";
import { DatePipe } from "@angular/common";
import { ShowRequestedItemService } from "../../services/concrete/showrequesteditem.service";
import { OrganizationManagementRequestService } from "../../services/concrete/organization-management/organization-management-request.service";
import { ActivatedRoute } from "@angular/router";
import { ReportFilterQueryViewModel } from "../../view-models/concrete/report-filter-query-view-model";
import { UsersDonationsReportDataViewModel } from "../../view-models/concrete/users-donations-view.model";

import * as _ from 'underscore';
import * as commonMessages from '../../shared/common-message.storage';
import { Pager } from "../../services/concrete/pager.service";
import * as moment from "moment/moment";
import { DatePeriod } from "../../shared/components/date-presets/date-period-class";


@Component({
    templateUrl: './users-donations-report.component.html',
    styleUrls: ['./users-donations-report.component.css'],
    providers: [DatePipe, ShowRequestedItemService, OrganizationManagementRequestService]
})

export class UsersDonationsReportComponent implements OnInit {

    private readonly DATE_FORMAT = "YYYY-MM-DD";
    pagedReportItems: Array<UsersDonationsReportDataViewModel>;
    reportModel: ReportFilterQueryViewModel = new ReportFilterQueryViewModel();
    inputMaxDate: Date = new Date();
    inputMinDate: Date = new Date("2000-01-01");
    isDataAvailable: boolean;

    isGettingDataStarted: boolean = true;
    totalReportItemsCount: number;
    pager: Pager = new Pager();
    pageSize: number = 10;
    listOfPagesSizes: number[] = [10, 20, 30];

    isFilterTurnedOn: boolean;

    constructor(private _service: ShowRequestedItemService,
        private datePipe: DatePipe,
        private _route: ActivatedRoute) {
    }

    ngOnInit(): void {
        this.setReportModelIdFromUrl();
        this.reportModel.dateFrom = moment().subtract(1, "month").format(this.DATE_FORMAT);
        this.reportModel.dateTo = moment().format(this.DATE_FORMAT);
        this.reportModel.filterValue = "";
        this.generateSimpleReport();
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

    onDatePeriodChange(value: DatePeriod) {
        this.reportModel.dateFrom = value.dateFrom;
        this.reportModel.dateTo = value.dateTo;
        this.generateSimpleReport();
    }

    filter() {
        this.isFilterTurnedOn = this.reportModel.filterValue.length > 0;
        this.generateSimpleReport();
    }

    clearFilter() {
        this.reportModel.filterValue = "";
        this.filter();
    }

    onChangePageSize(value: number) {
        this.pageSize = value;
        this.generateSimpleReport();
    }

    generateSimpleReport() {
       
        this.isGettingDataStarted = true;
        this._service.getCountOfUsersDonationsReportItems(this.reportModel)
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

        this.reportModel.currentPage = page;
        this.isGettingDataStarted = true;
        this.isDataAvailable = false;

        // get pager object to help implement pagination
        this.pager = new Pager().getPager(this.totalReportItemsCount, this.reportModel.currentPage, this.pageSize);
        this.reportModel.pageSize = this.pager.pageSize;

        this._service.getUsersDonationsPaginatedReport(this.reportModel)
            .subscribe(res => {
                this.calculateSequenceNumber(res);
                this.pagedReportItems = res;
                this.isDataAvailable = (res != undefined && res.length > 0);
                this.isGettingDataStarted = false;
            },
            error => {
                this.showErrorMessage(error);
                this.isGettingDataStarted = false;
            });

    }

    calculateSequenceNumber(list: Array<UsersDonationsReportDataViewModel>) {
        let i = 0;
        list.forEach((item) => { item.sequenceNumber = (i++ + 1) + (this.pager.currentPage - 1) * this.pageSize; });
    }

   
    prepareDate(date: Date): string {
        return this.datePipe.transform(date, 'yyyy-MM-dd')
    }

    showErrorMessage(message: string) {
        alert(commonMessages.anErrorOccurred + "\n" + message);
    }
}