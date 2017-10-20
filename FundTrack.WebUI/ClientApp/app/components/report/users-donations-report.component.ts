import { Component, OnInit, OnChanges } from "@angular/core";
import { DatePipe } from "@angular/common";
import { ShowRequestedItemService } from "../../services/concrete/showrequesteditem.service";
import { OrganizationManagementRequestService } from "../../services/concrete/organization-management/organization-management-request.service";
import { ActivatedRoute } from "@angular/router";
import { ReportFilterQueryViewModel } from "../../view-models/concrete/report-filter-query-view-model";
import { UsersDonationsReportDataViewModel } from "../../view-models/concrete/users-donations-view.model";

@Component({
    templateUrl: './users-donations-report.component.html',
    styleUrls: ['./users-donations-report.component.css'],
    providers: [DatePipe, ShowRequestedItemService, OrganizationManagementRequestService]
})

export class UsersDonationsReportComponent implements OnInit {
    reportData: Array<UsersDonationsReportDataViewModel> = new Array<UsersDonationsReportDataViewModel>();
    reportModel: ReportFilterQueryViewModel = new ReportFilterQueryViewModel();
    inputMaxDate: Date = new Date();
    isDataAvailable: boolean;

    constructor(private _service: ShowRequestedItemService,
                private datePipe: DatePipe,
                private _route: ActivatedRoute) {
    }

    ngOnInit(): void {
        this.setIdFromUrl();
    }

    setIdFromUrl() {
        this._route.params.subscribe(params => {
            this.reportModel.id = params['id'];
            if (params['id'] != null) {
                this.reportModel.id = params['id'];
                this.generateReport();
            }
        }, error => {
            this.showErrorMessage(<any>error);
        });
    }

    generateReport() {
        if (!this.isRequestDataValid()) {
            return;
        }

        this._service.getUsersDonationsReport(this.reportModel.id,
                                              this.datePipe.transform(this.reportModel.dateFrom, 'yyyy-MM-dd'),
                                              this.datePipe.transform(this.reportModel.dateTo, 'yyyy-MM-dd'))
            .subscribe((result: Array<UsersDonationsReportDataViewModel>) =>
            {
                this.reportData = result
                this.isDataAvailable = true;
            })
    }

    isRequestDataValid(): boolean {
        return this.reportModel.id > 0 && this.reportModel.dateFrom < this.inputMaxDate && this.reportModel.dateTo < this.inputMaxDate;
    }

    showErrorMessage(message: string) {
        alert("Виникла помилка: " + message);
    }
}