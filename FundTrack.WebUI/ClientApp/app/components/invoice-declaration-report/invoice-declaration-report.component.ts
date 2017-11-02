import { Component, OnInit, ViewChild, OnDestroy } from "@angular/core";
import { StorageService } from "../../shared/item-storage-service";
import { ModalComponent } from '../../shared/components/modal/modal-component';
import { ShowRequestedItemService } from "../../services/concrete/showrequesteditem.service";
import { OrganizationManagementRequestService } from "../../services/concrete/organization-management/organization-management-request.service";
import { InvoiceDeclarationRequestViewModel } from "../../view-models/concrete/invoice-declaration-request-view-model";
import { ActivatedRoute } from "@angular/router";
import { InvoiceDeclarationResponseViewModel } from "../../view-models/concrete/invoice-declaration-response-view-model";
import * as moment from "moment/moment";
import { DatePeriod } from "../../shared/components/date-presets/date-period-class";

@Component({
    templateUrl: './invoice-declaration-report.component.html',
    styleUrls: ['./invoice-declaration-report.component.css'],
    host: { '(window:keydown)': 'hotkeys($event)' },
    providers: [ShowRequestedItemService, OrganizationManagementRequestService]
})

export class InvoiceDeclarationReportComponent implements OnInit, OnDestroy {

    private readonly DATE_FORMAT = "YYYY-MM-DD";
    private errorMessage: string = "";
    private declarationRequestModel: InvoiceDeclarationRequestViewModel = new InvoiceDeclarationRequestViewModel();
    private declarationResponseModel: InvoiceDeclarationResponseViewModel[] = new Array<InvoiceDeclarationResponseViewModel>();
    private inputMaxDate: Date = new Date();
    private routeOrgIndex: number = 1;
    private ifDataExists: boolean = false;

    @ViewChild("exceptionModal")
    private exceptionModal: ModalComponent;

    @ViewChild("emptyResultsModal")
    private emptyResultsModal: ModalComponent;

    onDatePeriodChange(value: DatePeriod) {
        this.declarationRequestModel.dateFrom = value.dateFrom;
        this.declarationRequestModel.dateTo= value.dateTo;
        this.generateReport();
    }
    constructor(private storageService: StorageService,
        private service: ShowRequestedItemService,
        private route: ActivatedRoute) { }

    ngOnInit(): void {
        this.storageService.showDropDown = false;
        this.declarationRequestModel.orgid = 0;
        this.declarationRequestModel.dateFrom = moment().subtract(1, "month").format(this.DATE_FORMAT);
        this.declarationRequestModel.dateTo = moment().format(this.DATE_FORMAT);
        this.getIdFromUrl();
    }

    ngOnDestroy(): void {
        this.storageService.showDropDown = true;
    }

    getIdFromUrl() {
        this.route.params.subscribe(params => {
            this.routeOrgIndex = params["id"];
        }, error => {
            this.errorMessage = error;
            this.openModal(this.exceptionModal);
        });
    }


    generateReport(): void {
        this.declarationRequestModel.orgid = this.routeOrgIndex;
        this.getInvoiceDeclarationReport();
    }

    getInvoiceDeclarationReport(): void {
        this.service.getInvoiceDeclarationData(this.declarationRequestModel.orgid,
            this.declarationRequestModel.dateFrom,
            this.declarationRequestModel.dateTo)
            .subscribe((outcomeData: InvoiceDeclarationResponseViewModel[]) => {
                this.declarationResponseModel = outcomeData;
                this.checkIncomeSum();
                this.ifDataExists = true;
            },
            error => {
                this.errorMessage = error;
                this.openModal(this.exceptionModal);
            });
    }

    checkIncomeSum(): void {
        this.declarationResponseModel.forEach(m => {
            if (m.beginIncomeMonthSum == null || m.beginIncomeMonthSum === 0) {
                m.totalSum = null;
                m.beginIncomeMonthSum = null;
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