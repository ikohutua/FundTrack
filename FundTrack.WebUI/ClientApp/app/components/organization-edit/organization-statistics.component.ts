﻿import { OnInit, Component } from "@angular/core";
import { OrganizationGetGeneralInfoService } from "../../services/concrete/organization-management/organization-get-general-info.service";
import { EditOrganizationService } from "../../services/concrete/organization-management/edit-organization.service";
import { BaseTargetReportViewModel, SubTargetReportViewModel, AbctractTargetViewModel } from "../../view-models/concrete/edit-organization/target-report-view.model";
import { FinOpListViewModel } from "../../view-models/concrete/finance/finop-list-viewmodel";
import { OrganizationStatisticsService } from "../../services/concrete/organization-management/organization-statistics.service";
import { DatePipe } from '@angular/common';
import { isBrowser } from "angular2-universal";
import * as key from '../../shared/key.storage';
import * as constant from '../../shared/default-configuration.storage';
import { AuthorizeUserModel } from "../../view-models/concrete/authorized-user-info-view.model";

@Component({
    selector: 'statistics',
    templateUrl: './organization-statistics.component.html',
    styleUrls: ['./organization-statistics.component.css'],
    providers: [DatePipe, OrganizationGetGeneralInfoService, EditOrganizationService, OrganizationStatisticsService]
})
export class OrganizationStatisticsComponent implements OnInit {

    allTargets: Array<BaseTargetReportViewModel> = new Array<BaseTargetReportViewModel>();

    //Vertical bar chart & pie chart
    showXAxis = true;
    showYAxis = true;
    gradient = false;
    showLegend = false;
    showXAxisLabel = false;
    xAxisLabel = 'Targets';
    showYAxisLabel = false;
    yAxisLabel = 'Money';
    barPadding = 20;
    colorScheme = {
        domain: [
            '#2597FB', '#65EBFD', '#99FDD0',
            '#FCEE4B', '#FDD6E3', '#FCB1A8',
            '#EF6F7B', '#CB96E8', '#EFDEE0',
            '#FEFCFA']
    };
    public dataSet: any[] = [];

    dateFrom: Date = new Date();
    dateTo: Date = new Date();
    inputMaxDate: Date = new Date();
    unassingnedFinOps: FinOpListViewModel[] = Array<FinOpListViewModel>();
    user: AuthorizeUserModel = new AuthorizeUserModel();
    reportType: number = constant.incomeId; // set spending type as selected
    selectedCategory: string = "Базові";
    isDataExists: boolean = false;
    private showSpinner: boolean = false;
    private readonly baseTargetName: string = "Базові";

    constructor(private organizationStatisticsService: OrganizationStatisticsService,
        private datePipe: DatePipe) {
    }

    ngOnInit(): void {
        this.showSpinner = true;
        if (isBrowser) {
            if (localStorage.getItem(key.keyToken)) {
                this.user = JSON.parse(localStorage.getItem(key.keyModel)) as AuthorizeUserModel;
            }
        };
        this.dateFrom.setMonth(this.dateFrom.getMonth() - 1); 
        this.prepareTargetsForCharts(this.allTargets);
        this.getBaseTargets();
    }

    public prepareTargetsForCharts(list: Array<AbctractTargetViewModel>) {
        this.dataSet = [];
        list.forEach(t => {
            this.dataSet.push({
                name: t.targetName,
                value: t.sum
            });
        });
    }

    public setBeginDate(beginDate: Date): void {
        this.dateFrom = beginDate;
        this.getBaseTargets();
    }

    public setEndDate(endDate: Date): void {
        this.dateTo = endDate;
        this.getBaseTargets();
    }

    private onClickBaseTarget(target: BaseTargetReportViewModel): void {
        target.isOpen = !target.isOpen;
        if (target.isOpen) {
            if (target.id == undefined) {
                this.selectedCategory = this.baseTargetName;
                this.prepareTargetsForCharts(this.allTargets);
                this.organizationStatisticsService.getFinOpsByTargetId(this.reportType, target.id, this.transformDate(this.dateFrom), this.transformDate(this.dateTo))
                    .subscribe(response => this.unassingnedFinOps = response);
            } else {
                this.selectedCategory = target.targetName;
                this.organizationStatisticsService.getSubTargets(this.user.orgId, this.reportType, target.id, this.transformDate(this.dateFrom), this.transformDate(this.dateTo))
                    .subscribe(response => {
                        target.subTargetsArray = response; 
                        this.prepareTargetsForCharts(response);
                    });
            }
        }
        else {
            this.selectedCategory = this.baseTargetName;
            this.prepareTargetsForCharts(this.allTargets);
        }
    }

    private onClickSubTarget(subTarget: SubTargetReportViewModel): void {
        subTarget.isOpen = !subTarget.isOpen;
        if (subTarget.isOpen) {
            this.organizationStatisticsService.getFinOpsByTargetId(this.reportType, subTarget.id, this.transformDate(this.dateFrom), this.transformDate(this.dateTo))
                .subscribe(response => subTarget.finOpsArray = response);
        }
    }

    private transformDate(date : Date) : string {
        return this.datePipe.transform(date, "yyyy-MM-dd");
    }

    private onChangeFinOpType($event): void {
        this.reportType = $event;
        this.getBaseTargets();
    }

    private getBaseTargets(): void {
        this.showSpinner = true;
        this.organizationStatisticsService.getReportForFinopsByTargets(this.user.orgId, this.reportType, this.transformDate(this.dateFrom), this.transformDate(this.dateTo))
            .subscribe(response => {
                this.allTargets = response;
                this.prepareTargetsForCharts(response);
                this.selectedCategory = this.baseTargetName;
                this.checkData();
                this.showSpinner = false;
            });
    }

    private checkData(): void {
        if (this.allTargets == undefined) {
            this.isDataExists = false;
            return;
        }
        if (this.allTargets.length == 0) {
            this.isDataExists = false;
            return;
        }
        this.isDataExists = true;
    }
}
