import { OnInit, Component } from "@angular/core";
import { OrganizationGetGeneralInfoService } from "../../services/concrete/organization-management/organization-get-general-info.service";
import { EditOrganizationService } from "../../services/concrete/organization-management/edit-organization.service";
import { TargetReportViewModel } from "../../view-models/concrete/edit-organization/target-report-view.model";
import { FinOpListViewModel } from "../../view-models/concrete/finance/finop-list-viewmodel";

@Component({
    selector: 'statistics',
    templateUrl: './organization-statistics.component.html',
    styleUrls: ['./organization-statistics.component.css'],
    providers: [OrganizationGetGeneralInfoService, EditOrganizationService]
})
export class OrganizationStatisticsComponent implements OnInit {

    allTargets: Array<TargetReportViewModel> = new Array<TargetReportViewModel>();
    testTargets: Array<TargetReportViewModel> = [
        { targetName: "Медицина", sum: 10600, list: new Array<FinOpListViewModel>() },
        { targetName: "Продукти", sum: 6350, list: new Array<FinOpListViewModel>() },
        { targetName: "Одяг", sum: 9700, list: new Array<FinOpListViewModel>() },
        { targetName: "Електроніка", sum:25000, list: new Array<FinOpListViewModel>() },
        { targetName: "Test", sum: 400, list: new Array<FinOpListViewModel>() }
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

    constructor() {
    }

    ngOnInit(): void {
        this.allTargets = this.testTargets;
        this.PrepareTargetsForCharts(this.allTargets);
    }

    public PrepareTargetsForCharts(list: Array<TargetReportViewModel>) {

        list.forEach(t => {
            this.dataSet.push({
                name: t.targetName,
                value: t.sum
            });
        })
    }

    onSelect(event) {
        console.log(event);
    }
}
