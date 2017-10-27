import { Component, OnInit, ViewChild, OnDestroy } from "@angular/core";
import { StorageService } from "../../shared/item-storage-service";
import { ModalComponent } from "../../shared/components/modal/modal-component";
import { IOrganizationForFiltering } from "../../view-models/abstract/organization-for-filtering.interface";
import { ShowRequestedItemService } from "../../services/concrete/showrequesteditem.service";
import { OrganizationManagementRequestService } from "../../services/concrete/organization-management/organization-management-request.service";
import { ReportFilterQueryViewModel } from "../../view-models/concrete/report-filter-query-view-model";
import { IncomeReportDataViewModel } from "../../view-models/concrete/income-report-data-view-model";
import { OutcomeReportDataViewModel } from "../../view-models/concrete/outcome-report-data-view-model";
import { ITotalSum } from "../../view-models/abstract/total-sum-money-amount-interface";
import { ActivatedRoute } from "@angular/router";
import * as moment from "moment/moment";

@Component({
    templateUrl: "./report.component.html",
    styleUrls: ["./report.component.css"],
    host: { '(window:keydown)': "hotkeys($event)" },
    providers: [ShowRequestedItemService, OrganizationManagementRequestService]
})
   
export class ReportComponent implements OnInit, OnDestroy {

    private readonly DATE_FORMAT = "YYYY-MM-DD"; 
    private errorMessage: string= "";
    private organizations: IOrganizationForFiltering[] = new Array<IOrganizationForFiltering>();
    private incomeReportData: IncomeReportDataViewModel[] = new Array<IncomeReportDataViewModel>();
    private outcomeReportData: OutcomeReportDataViewModel[] = new Array<OutcomeReportDataViewModel>();
    private reportModel: ReportFilterQueryViewModel = new ReportFilterQueryViewModel();
    private accessToFill: number;
    private reportOutTotalSum: number = 0;
    private reportInTotalSum: number = 0;
    private reportImages: string[];
    private selectedImage: any;   
    private index: number = 0;
    private routeOrgIndex: number = 1;
    private ifDataExists: boolean = false;
    private inputMaxDate = moment().format(this.DATE_FORMAT);

    @ViewChild("exceptionModal")
    private exceptionModal: ModalComponent;

    @ViewChild("emptyResultsModal")
    private emptyResultsModal: ModalComponent;


    onDateFromChange(dateFrom: string,orgId) {
        this.reportModel.dateFrom = dateFrom;
        this.generateReport(orgId);
    }

    onDateToChange(dateTo: string,orgId) {
        this.reportModel.dateTo = dateTo;
        this.generateReport(orgId);
    }

    constructor(private storageService: StorageService,
                private service: ShowRequestedItemService,
                private route: ActivatedRoute) { }

    ngOnInit(): void {
        this.storageService.showDropDown = false;
        this.reportModel.id = 0;
        this.reportModel.dateFrom = moment().subtract(1, "month").format(this.DATE_FORMAT);
        this.reportModel.dateTo = moment().format(this.DATE_FORMAT);
        this.getOrganizations();
        this.getIdFromUrl();   
    }

    ngOnDestroy(): void {
        this.storageService.showDropDown = true;
    }

    getIdFromUrl() {
        this.route.params.subscribe(params => {
            this.routeOrgIndex = params["id"];
            if (this.routeOrgIndex != null) {
                this.generateReport(this.routeOrgIndex);
            }
        }, error => {
            this.errorMessage = error;
            this.openModal(this.exceptionModal);
        });
    }

    generateReport(orgId): void {
        this.reportModel.id = orgId;       
            this.getReportData();            
    }

    getReportData(): void {
        
            this.service.getOutcomeReportData(this.reportModel.id, this.reportModel.dateFrom, this.reportModel.dateTo)
                .subscribe((outcomeData: OutcomeReportDataViewModel[]) => {
                        if (outcomeData.length != 0) {
                            this.ifDataExists = true;
                            this.outcomeReportData = outcomeData;
                            this.reportOutTotalSum = this.getReportMoneySumByType(this.outcomeReportData);
                        } else {
                            this.ifDataExists = false;
                            this.openModal(this.emptyResultsModal);
                        }
                    },
                    error => {
                        this.errorMessage = error;
                        this.openModal(this.exceptionModal);
                    });
        
       
            this.service
                .getIncomeReportData(this.reportModel.id, this.reportModel.dateFrom, this.reportModel.dateTo)
                .subscribe((incomeData: IncomeReportDataViewModel[]) => {
                        if (incomeData.length != 0) {
                            this.ifDataExists = true;
                            this.incomeReportData = incomeData;
                            this.reportInTotalSum = this.getReportMoneySumByType(this.incomeReportData);
                        } else {
                            this.openModal(this.emptyResultsModal);
                            this.ifDataExists = false;
                        }
                    },
                    error => {
                        this.errorMessage = error;
                        this.openModal(this.exceptionModal);
                    });
        
    }

    getReportMoneySumByType(array: Array<ITotalSum>): number {
        let sum = 0;
        for (let i = 0; i < array.length; i++) {
            sum += array[i].moneyAmount;
        }
        return sum;
    }

    openModal(modal: ModalComponent): void {
        modal.show();
    }

    closeModal(modal: ModalComponent): void {
        modal.hide();
    }
 
    getOrganizations(): void {
        this.service.getOrgaizations().subscribe((organizations: IOrganizationForFiltering[]) => {
                this.organizations = organizations;
            },
            error => {
                this.errorMessage = error;
                this.openModal(this.exceptionModal);
            });
    }

    public navigate(forward) {
        this.index = this.reportImages.indexOf(this.selectedImage) + (forward ? 1 : -1);
        if (this.index >= 0 && this.index < this.reportImages.length) {
            this.selectedImage = this.reportImages[this.index];       
        }
    }

    public hotkeys(event) {
        if (this.selectedImage) {
            if (event.keyCode == 37) {
                this.navigate(false);
            } else if (event.keyCode == 39) {
                this.navigate(true);
            }
        }
    }

    public getImagesById(finOpId) {
        this.service.getFinOpImages(finOpId).subscribe((images: string[]) => {
                if (images.length != 0) {
                    this.reportImages = images;
                } else {
                    //"image not found" hardcored default path 
                    //TODO change path from hardcored to azure "image not found" path
                    this.reportImages = [
                        "https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQn_abQ0ko6CuA9LMsgv-JWMVJhGQboWlZDlUoZHeZ33cFwr2Ds"
                    ];
                }

                this.selectedImage = this.reportImages[0];
                this.index = this.reportImages.indexOf(this.selectedImage);
            },
            error => {
                this.errorMessage = error;
                this.openModal(this.exceptionModal);
            });
    }


}