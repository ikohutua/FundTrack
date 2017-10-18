import { OnInit, Component } from "@angular/core";
import { OrganizationGetGeneralInfoService } from "../../services/concrete/organization-management/organization-get-general-info.service";
import { EditOrganizationService } from "../../services/concrete/organization-management/edit-organization.service";

@Component({
    selector: 'statistics',
    templateUrl: './organization-statistics.component.html',
    styleUrls: ['./organization-statistics.component.css'],
    providers: [OrganizationGetGeneralInfoService, EditOrganizationService]
})
export class OrganizationStatisticsComponent implements OnInit {

    constructor() {

    }

    ngOnInit(): void {

    }

    // lineChart
    public lineChartData: Array<any> = [
        [65, 59, 80, 81, 56, 55, 40],
        [28, 48, 40, 19, 86, 27, 90],
        [50, 0, 20, 10, 50, 21, 30]
    ];
    public lineChartLabels: Array<any> = ['January', 'February', 'March', 'April', 'May', 'June', 'July'];
    public lineChartType: string = 'bar';
    public pieChartType: string = 'pie';

    // Pie
    public pieChartLabels: string[] = ['Download Sales', 'In-Store Sales', 'Mail Sales', 'Ololol'];
    public pieChartData: number[] = [300, 500, 100, 700];

    public randomizeType(): void {
        //this.lineChartType = this.lineChartType === 'line' ? 'bar' : 'line';
        this.pieChartType = this.pieChartType === 'doughnut' ? 'pie' : 'doughnut';
    }

    public chartClicked(e: any): void {
        console.log(e);
    }

    public chartHovered(e: any): void {
        console.log(e);
    }
}
