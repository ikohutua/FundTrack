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
import { TargetViewModel } from "../../view-models/concrete/finance/donate/target.view-model";
import { DataSetViewModel } from "../../view-models/concrete/data-set-view.model";

@Component({
    templateUrl: './common-donations-report.component.html',
    styleUrls: ['./common-donations-report.component.css'],
    providers: [DatePipe, ShowRequestedItemService, OrganizationManagementRequestService]
})

export class CommonDonationsReportComponent implements OnInit {
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

    isDesc: boolean = true;
    previousSortedColIndex: number = 0;
    currentSortedColIndex: number = -1;

    isChartVisible: boolean;
    targetsOfOrganization: Array<TargetViewModel>;
    selectedTargetId: number = -1;

    //---------Pie chart---------
    showXAxis = true;
    showYAxis = true;
    gradient = false;
    showLegend = false;
    showXAxisLabel = true;
    xAxisLabel = 'Поточний місяць';
    showYAxisLabel = true;
    yAxisLabel = 'Money';
    barPadding = 20;
    autoScale = true;
    colorScheme = {
        domain: [
            'green', 'blue'
        ]
    };

    // public dataSet: any[] = [];
    public dataSet: any[]=[];

    constructor(private _service: ShowRequestedItemService,
        private datePipe: DatePipe,
        private _route: ActivatedRoute) {
    }

    ngOnInit(): void {
        this.setReportModelIdFromUrl();
        this.reportModel.dateFrom = new Date();
        this.reportModel.dateFrom.setMonth(this.reportModel.dateFrom.getMonth() - 1);
        this.reportModel.dateTo = new Date();
        this.reportModel.filterValue = "";
        this.generateSimpleReport();
        this.getOrganizationTargets();
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
        this.isChartVisible = false;
        this.generateSimpleReport();
    }

    setEndDate(value: Date) {
        this.reportModel.dateTo = value;
        this.isChartVisible = false;
        this.generateSimpleReport();
    }

    displayChart() {
        this.isChartVisible = !this.isChartVisible;
        if (this.isChartVisible) {
            this.UpdateDonationValueReportForChart();
        }
    }

    showChart() {
        this.isChartVisible = true;
    }


    getOrganizationTargets() {
        this._service.getAllTargetsOfOrganization(this.reportModel.id)
            .subscribe(res => {
                this.targetsOfOrganization = res;
                console.log(res);

            }, error => {
                this.showErrorMessage(error);
            });
    }
    showChartWithCommonData() {
        this.selectedTargetId = -1;
        this.UpdateDonationValueReportForChart();
        this.showChart();
    }

    showChartByTarget(targetId: number) {
        this.selectedTargetId = targetId;
        console.log(targetId);
        this.UpdateDonationValueReportForChart();
        this.showChart();
    }

    UpdateDonationValueReportForChart() {
        this._service.DonationsValueReportPerDay(this.reportModel, this.selectedTargetId)
            .subscribe(res => {
                console.log(res);
                this.setNewDataForLineChart(res);
            }, error => {
                this.showErrorMessage(error);
            });
    }


    setNewDataForLineChart(list: DataSetViewModel[]) {
        debugger;
        this.dataSet = [];

        let name = this.selectedTargetId == -1
            ? "По всіх призначеннях"
            : _.find(this.targetsOfOrganization, t => t.targetId == this.selectedTargetId).name;
        console.log(name);

        let series: DataSetViewModel[]=[];

        list.forEach(item => {
            series.push({
                name: item.name,
                value: item.value
            })
        });
        console.log(series);

        this.dataSet = [{
            "name": name,
            "series": series
        }];
    }

    sortTable(value: number) {
        this.currentSortedColIndex = value;
        let colName = "";
        switch (value) {
            case 0: colName = "sequenceNumber"; break;
            case 1: colName = "userLogin"; break;
            case 2: colName = "userFulName"; break;
            case 3: colName = "moneyAmount"; break;
            case 4: colName = "target"; break;
            default:
        }

        this.isDesc = value === this.previousSortedColIndex
            ? this.isDesc = !this.isDesc
            : this.isDesc = true;

        this.previousSortedColIndex = value;

        this.pagedReportItems = this.isDesc
            ? _.sortBy(this.pagedReportItems, colName)
            : this.pagedReportItems.reverse();
    }

    onChangePageSize(value: number) {
        this.pageSize = value;
        this.generateSimpleReport();
    }

    generateSimpleReport() {
        if (!this.isRequestDataValid()) {
            alert(commonMessages.invalidDate);
            return;
        }

        this._service.getCountOfCommonUsersDonationsReportItems(this.reportModel)
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

        this._service.getCommonUsersDonationsPaginatedReport(this.reportModel)
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

    isRequestDataValid(): boolean {

        this.reportModel.dateFrom = new Date(this.datePipe.transform(this.reportModel.dateFrom));
        this.reportModel.dateTo = new Date(this.datePipe.transform(this.reportModel.dateTo));

        return this.reportModel.id > 0 && (this.reportModel.dateTo <= this.inputMaxDate && this.reportModel.dateTo >= this.reportModel.dateFrom) && this.reportModel.dateFrom <= this.reportModel.dateTo;
    }
    prepareDate(date: Date): string {
        return this.datePipe.transform(date, 'yyyy-MM-dd')
    }

    showErrorMessage(message: string) {
        alert(commonMessages.anErrorOccurred + "\n" + message);
    }
}