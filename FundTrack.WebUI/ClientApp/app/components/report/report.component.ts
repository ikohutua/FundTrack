import { Component, OnInit, ViewChild, OnDestroy } from "@angular/core";
import { StorageService } from "../../shared/item-storage-service";
import { DatePipe } from '@angular/common';
import { ModalComponent } from '../../shared/components/modal/modal-component';
import { IOrganizationForFiltering } from "../../view-models/abstract/organization-for-filtering.interface";
import { ShowRequestedItemService } from "../../services/concrete/showrequesteditem.service";
import { OrganizationManagementRequestService } from "../../services/concrete/organization-management/organization-management-request.service";
import { ReportFilterQueryViewModel } from "../../view-models/concrete/report-filter-query-view-model";
import { IncomeReportDataViewModel } from "../../view-models/concrete/income-report-data-view-model";
import { OutcomeReportDataViewModel } from "../../view-models/concrete/outcome-report-data-view-model";
import { ITotalSum } from "../../view-models/abstract/total-sum-money-amount-interface";

@Component({
    templateUrl: './report.component.html',
    styleUrls: ['./report.component.css'],
    host: { '(window:keydown)': 'hotkeys($event)' },
    providers: [DatePipe, ShowRequestedItemService, OrganizationManagementRequestService]
})
   
export class ReportComponent implements OnInit, OnDestroy {

    private errorMessage: string;
    private organizations: IOrganizationForFiltering[] = new Array<IOrganizationForFiltering>();
    private incomeReportData: IncomeReportDataViewModel[] = new Array<IncomeReportDataViewModel>();
    private outcomeReportData: OutcomeReportDataViewModel[] = new Array<OutcomeReportDataViewModel>();
    private reportModel: ReportFilterQueryViewModel = new ReportFilterQueryViewModel();
    private reportHeaders: string[];
    private accessToFill: number;
    private reportOutTotalSum: number;
    private reportInTotalSum: number;
    private reportImages: string[];
    private selectedImage: any;   
    private index: number = 0;
   
    @ViewChild("dateExceptionModal")
    public dateExceptionModal: ModalComponent;

    @ViewChild("exceptionModal")
    public exceptionModal: ModalComponent;
   
    constructor(private storageService: StorageService,
                private service: ShowRequestedItemService,
                private datePipe: DatePipe) { }

    ngOnInit(): void {
        this.storageService.showDropDown = false;
        this.reportModel.id = 0;
        this.reportModel.dateFrom = new Date();
        this.reportModel.dateFrom.setMonth(this.reportModel.dateFrom.getMonth() - 1);      
        this.reportModel.dateTo = new Date();
        this.reportModel.reportType = 0;
        this.getOrganizations();
    }

    ngOnDestroy(): void {
        this.storageService.showDropDown = true;
    }

    generateReport(): void {
        console.log("user dateFrom=" + this.datePipe.transform(this.reportModel.dateFrom, 'yyyy-MM-dd') + " and dateTo=" + this.datePipe.transform(this.reportModel.dateTo, 'yyyy-MM-dd'));
        console.log("user orgId=" + this.reportModel.id + " and reportType=" + this.reportModel.reportType);
        if (this.isDateValid()) {
            this.openModal(this.dateExceptionModal);
        }
        else {
            this.fillHeadersArray();
            this.getReportDataByType();
            this.accessToFill = this.reportModel.reportType;           
        }
    }
    
    getReportDataByType(): void {
        if (this.reportModel.reportType == 0) {         
            this.service.getOutcomeReportData(this.reportModel.id, this.datePipe.transform(this.reportModel.dateFrom, 'yyyy-MM-dd'), this.datePipe.transform(this.reportModel.dateTo, 'yyyy-MM-dd')).subscribe((outcomeData: OutcomeReportDataViewModel[]) => {
                this.outcomeReportData = outcomeData;
                this.reportOutTotalSum = this.getReportMoneySumByType(this.outcomeReportData);
                console.log("SUM IS=" + this.getReportMoneySumByType(this.outcomeReportData));
            },
                error => {
                    this.errorMessage = <any>error;
                    this.openModal(this.exceptionModal);
                })
        }
        if (this.reportModel.reportType == 1) {
            this.service.getIncomeReportData(this.reportModel.id, this.datePipe.transform(this.reportModel.dateFrom, 'yyyy-MM-dd'), this.datePipe.transform(this.reportModel.dateTo, 'yyyy-MM-dd')).subscribe((incomeData: IncomeReportDataViewModel[]) => {
                this.incomeReportData = incomeData;
                this.reportInTotalSum = this.getReportMoneySumByType(this.incomeReportData);
                console.log("SUM IS=" + this.getReportMoneySumByType(this.incomeReportData));
            },
                error => {
                    this.errorMessage = <any>error;
                    this.openModal(this.exceptionModal);
                })
        }
    }

    getReportMoneySumByType(array: Array<ITotalSum>): number {
        let sum = 0;
        for (let i = 0; i < array.length; i++) {
            sum += array[i].moneyAmount;
        }
        return sum;
    }

    isDateValid(): boolean {
        if (!this.reportModel.dateFrom || !this.reportModel.dateTo
            || this.datePipe.transform(this.reportModel.dateFrom, 'yyyy-MM-dd')
            >= this.datePipe.transform(this.reportModel.dateTo, 'yyyy-MM-dd'))
            return true;
        else
            return false;
    }

    openModal(modal: ModalComponent): void {
        modal.show();
    }

    closeModal(modal: ModalComponent): void {
        modal.hide();
    }
  
    public setBeginDate(beginDate: Date): void {
        this.reportModel.dateFrom = beginDate;       
    }

    public setEndDate(endDate: Date): void {
        this.reportModel.dateTo = endDate;
    }
 
    getOrganizations(): void {
        this.service.getOrgaizations().subscribe((organizations: IOrganizationForFiltering[]) => {
            this.organizations = organizations;
        },
            error => {
                this.errorMessage = <any>error;
                this.openModal(this.exceptionModal);
            })
    }

    //fill table header for chosen report type
    fillHeadersArray(): void {
        if (this.reportModel.reportType == 0) {
            this.reportHeaders = ['№ п/п', 'Призначення', 'Опис', 'Сума, ₴', 'Дата', 'Фото'];
        }
        if (this.reportModel.reportType == 1) {
            this.reportHeaders = ['№ п/п', 'Призначення', 'Від кого', 'Опис', 'Сума, ₴', 'Дата'];
        }
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
            if (images.length !=0) {
                this.reportImages = images;
            }
            else {
                //"image not found" hardcored default path 
                //TODO change path from hardcored to azure "image not found" path
                this.reportImages = ['https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcQn_abQ0ko6CuA9LMsgv-JWMVJhGQboWlZDlUoZHeZ33cFwr2Ds'];
            }
            
            this.selectedImage = this.reportImages[0];
            this.index = this.reportImages.indexOf(this.selectedImage);
        },
            error => {
                this.errorMessage = <any>error;
                this.openModal(this.exceptionModal);
            })
    }


}