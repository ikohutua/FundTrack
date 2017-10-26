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
            'red', 'green', 'blue'
        ]
    };
    // public dataSet: any[] = [];
    public dataSet: any[] = [
        {
            "name": "Germany",
            "series": [
                {
                    "name": "2010",
                    "value": 62000
                },
                {
                    "name": "2011",
                    "value": 89400
                },
                {
                    "name": "2012",
                    "value": 34500
                }
            ]
        },

        {
            "name": "USA",
            "series": [
                {
                    "name": "2010",
                    "value": 78506
                },
                {
                    "name": "2011",
                    "value": 85300
                },
                {
                    "name": "2012",
                    "value": 71100
                }
            ]
        },

        {
            "name": "Ukraina",
            "series": [
                {
                    "name": "2010",
                    "value": 54200
                },
                {
                    "name": "2011",
                    "value": 32100
                },
                {
                    "name": "2012",
                    "value": 75990
                }
            ]
        }
    ];

    //---------Pie chart---------


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

    showChart() {
        this.isChartVisible = !this.isChartVisible;
        // this.prepareUsersDonationsReportForCharts(this.pagedReportItems);
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

    public prepareUsersDonationsReportForCharts(list: Array<UsersDonationsReportDataViewModel>) {
        this.dataSet = [];
        list.forEach(d => {
            this.dataSet.push({
                name: d.userLogin,
                value: d.moneyAmount
            });
        });
    }

    showErrorMessage(message: string) {
        alert(commonMessages.anErrorOccurred + "\n" + message);
    }
}