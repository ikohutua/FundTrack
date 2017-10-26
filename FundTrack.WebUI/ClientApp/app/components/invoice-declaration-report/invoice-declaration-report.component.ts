import { Component, OnInit, ViewChild, OnDestroy } from "@angular/core";
import { StorageService } from "../../shared/item-storage-service";
import { DatePipe } from '@angular/common';
import { ModalComponent } from '../../shared/components/modal/modal-component';
import { ShowRequestedItemService } from "../../services/concrete/showrequesteditem.service";
import { OrganizationManagementRequestService } from "../../services/concrete/organization-management/organization-management-request.service";
import { InvoiceDeclarationRequestViewModel } from "../../view-models/concrete/invoice-declaration-request-view-model";
import { ActivatedRoute } from "@angular/router";
import { InvoiceDeclarationResponseViewModel } from "../../view-models/concrete/invoice-declaration-response-view-model";

@Component({
    templateUrl: './invoice-declaration-report.component.html',
    styleUrls: ['./invoice-declaration-report.component.css'],
    host: { '(window:keydown)': 'hotkeys($event)' },
    providers: [DatePipe, ShowRequestedItemService, OrganizationManagementRequestService]
})
   
export class InvoiceDeclarationReportComponent implements OnInit, OnDestroy {

    private errorMessage: string;
    private declarationRequestModel: InvoiceDeclarationRequestViewModel = new InvoiceDeclarationRequestViewModel();
    private declarationResponseModel: InvoiceDeclarationResponseViewModel[] = new Array<InvoiceDeclarationResponseViewModel>();
    private inputMaxDate: Date = new Date();
    private routeOrgIndex: number = 1;
    private ifDataExists: boolean = false;

    @ViewChild("exceptionModal")
    private exceptionModal: ModalComponent;

    @ViewChild("emptyResultsModal")
    private emptyResultsModal: ModalComponent;

    onDateFromChange(dateFrom: Date) {
        this.declarationRequestModel.dateFrom = dateFrom;
        this.generateReport();
    }

    onDateToChange(dateTo: Date) {
        this.declarationRequestModel.dateTo = dateTo;
        this.generateReport();
    }
     
    constructor(private storageService: StorageService,
                private service: ShowRequestedItemService,
                private datePipe: DatePipe,
                private route: ActivatedRoute) { }

    ngOnInit(): void {
        this.storageService.showDropDown = false;
        this.declarationRequestModel.orgid = 0;
        this.declarationRequestModel.dateFrom = new Date();
        this.declarationRequestModel.dateFrom.setMonth(this.declarationRequestModel.dateFrom.getMonth() - 1);
        this.declarationRequestModel.dateTo = new Date();
        this.GetAllBalancesByAccountId();
        this.getIdFromUrl();   
    }

    ngOnDestroy(): void {
        this.storageService.showDropDown = true;
    }

    getIdFromUrl() {
        this.route.params.subscribe(params => {
            this.routeOrgIndex = params['id'];
        }, error => {
            this.errorMessage = <any>error;
            this.openModal(this.exceptionModal);
            });
    }

    GetAllBalancesByAccountId(): void {

    }

    generateReport(): void {
        this.declarationRequestModel.orgid = this.routeOrgIndex;
            this.getInvoiceDeclarationReport();         
    }

    getInvoiceDeclarationReport(): void {
        this.service.getInvoiceDeclarationData(this.declarationRequestModel.orgid,
                                                this.datePipe.transform(this.declarationRequestModel.dateFrom, 'yyyy-MM-dd'),
                                                this.datePipe.transform(this.declarationRequestModel.dateTo, 'yyyy-MM-dd'))
                                                    .subscribe((outcomeData: InvoiceDeclarationResponseViewModel[]) => {
               if (outcomeData.length != 0) {
                    this.declarationResponseModel = outcomeData;
                    this.getTotalSum();
                    this.ifDataExists = true;
               }
                else {  
                   this.openModal(this.emptyResultsModal);
                   this.ifDataExists = false;
                }
            },
                error => {
                    this.errorMessage = <any>error;
                    this.openModal(this.exceptionModal);
                })
        
   
    }

    getTotalSum(): void {  
        
        this.declarationResponseModel.forEach(m => {
            if (m.beginIncomeMonthSum == null || m.beginIncomeMonthSum == 0) {
                m.totalSum = null;
                m.beginIncomeMonthSum = null;
            }
            else {
                m.totalSum = m.beginIncomeMonthSum + m.totalIncomeSum + m.transferIncome - m.flowOutcome - m.transferOutcome;
            }
        });
    }

    openModal(modal: ModalComponent): void {
        modal.show();
    }

    closeModal(modal: ModalComponent): void {
        modal.hide();
    }
  
   


}