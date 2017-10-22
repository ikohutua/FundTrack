import { OnInit, Component } from "@angular/core";
import { OrganizationGetGeneralInfoService } from "../../services/concrete/organization-management/organization-get-general-info.service";
import { EditOrganizationService } from "../../services/concrete/organization-management/edit-organization.service";
import { BaseTargetReportViewModel, SubTargetReportViewModel } from "../../view-models/concrete/edit-organization/target-report-view.model";
import { FinOpListViewModel } from "../../view-models/concrete/finance/finop-list-viewmodel";
import { OrganizationStatisticsService } from "../../services/concrete/organization-management/organization-statistics.service";
import { DatePipe } from '@angular/common';

@Component({
    selector: 'statistics',
    templateUrl: './organization-statistics.component.html',
    styleUrls: ['./organization-statistics.component.css'],
    providers: [DatePipe, OrganizationGetGeneralInfoService, EditOrganizationService, OrganizationStatisticsService]
})
export class OrganizationStatisticsComponent implements OnInit {

    allTargets: Array<BaseTargetReportViewModel> = new Array<BaseTargetReportViewModel>();
    testTargets: Array<BaseTargetReportViewModel> = [
        { id: -1, targetName: "Медицина", sum: 10600, subTargetsArray: new Array<SubTargetReportViewModel>(), isOpen: false },
        { id: -1, targetName: "Продукти", sum: 6350, subTargetsArray: new Array<SubTargetReportViewModel>(), isOpen: false},
        { id: -1, targetName: "Одяг", sum: 9700, subTargetsArray: new Array<SubTargetReportViewModel>(), isOpen: false},
        { id: -1, targetName: "Електроніка", sum: 25000, subTargetsArray: new Array<SubTargetReportViewModel>(), isOpen: false},
        { id: -1, targetName: "Test", sum: 400, subTargetsArray: new Array<SubTargetReportViewModel>(), isOpen: false}
    ];

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

    constructor(private organizationStatisticsService: OrganizationStatisticsService,
                private datePipe: DatePipe) {
    }

    ngOnInit(): void {
        this.dateFrom.setMonth(this.dateFrom.getMonth() - 24); 
        //this.allTargets = this.testTargets;
        this.prepareTargetsForCharts(this.allTargets);
        this.organizationStatisticsService.getReportForFinopsByTargets(1, this.datePipe.transform(this.dateFrom, 'yyyy-MM-dd'), this.datePipe.transform(this.dateTo, 'yyyy-MM-dd'))
            .subscribe(response => this.allTargets = response);
    }

    public prepareTargetsForCharts(list: Array<BaseTargetReportViewModel>) {

        list.forEach(t => {
            this.dataSet.push({
                name: t.targetName,
                value: t.sum
            });
        });
    }

    onSelect(event) {
        console.log(event);
    }

    public setBeginDate(beginDate: Date): void {
        this.dateFrom = beginDate;
        console.log(beginDate);
    }

    public setEndDate(endDate: Date): void {
        this.dateTo = endDate;
        console.log(endDate);
    }

    private onClickBaseTarget(target : BaseTargetReportViewModel): void {
        target.isOpen = !target.isOpen;
        if (target.isOpen) {
            this.organizationStatisticsService.getSubTargets(1,
                    target.id,
                    this.datePipe.transform(this.dateFrom, 'yyyy-MM-dd'),
                    this.datePipe.transform(this.dateTo, 'yyyy-MM-dd'))
                .subscribe(response => target.subTargetsArray = response);
        }
    }

    private onClickSubTarget(subTarget : SubTargetReportViewModel): void {
        subTarget.isOpen = !subTarget.isOpen;
        if (subTarget.isOpen) {
            this.organizationStatisticsService.getFinOpsByTargetId(subTarget.id,
                    this.datePipe.transform(this.dateFrom, 'yyyy-MM-dd'),
                    this.datePipe.transform(this.dateTo, 'yyyy-MM-dd'))
                .subscribe(response => subTarget.finOpsArray = response);
        }
    }
}
