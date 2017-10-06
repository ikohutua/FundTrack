import { Component, OnInit, Input, OnChanges, SimpleChanges, Output, EventEmitter } from "@angular/core";
import { DatePipe } from '@angular/common';
import { FixingBalanceService } from "../../services/concrete/fixing-balance.service";
import { FixingBalanceFilteringViewModel } from "../../view-models/concrete/fixing-balance-filtering-view.model";
import { BalanceViewModel } from "../../view-models/concrete/finance/balance-view.model";

@Component({
    selector: 'fixing-balance',
    templateUrl: './fixing-balance.component.html',
    styleUrls: ['./fixing-balance.component.css'],
    providers: [FixingBalanceService]
})
export class FixingBalanceComponent implements OnChanges, OnInit {

    @Output() closeModalEvent = new EventEmitter<boolean>();

    @Input('accountId') accountId: number;

    seccessFixingBalance: string = "Поточний баланс успішно зафіксовано";
    isDatePickerEnabled: boolean = true;
    isSubmitEnabled: boolean = true;
    minValueDatePicker: Date;
    serverDate: string;
    currentDate: string;
    lastFixing: BalanceViewModel;
    message: string = "";
    toasterClass: string = "";


    constructor(private fixingBalanceService: FixingBalanceService) {
    }

    ngOnInit(): void {

    }

    ngOnChanges(changes: SimpleChanges): void {
        if (changes['accountId'].currentValue && changes['accountId'].currentValue != -1) {
            this.getFilterFromServer();
        }
    }

    getFilterFromServer() {
        this.fixingBalanceService.getFilterByAccId(this.accountId)
            .subscribe(data => {
                this.resetAllData();
                this.changeDataForFilter(data);
            });
    }

    changeDataForFilter(filter: FixingBalanceFilteringViewModel) {
        this.serverDate = filter.serverDate;
        this.currentDate = filter.serverDate;
        this.lastFixing = filter.lastFixing;
        this.minValueDatePicker = new Date(filter.firstDayForFixingBalance);
        // variables for compare date
        let serverDate = new Date(this.serverDate);
        let minValueDate = new Date(this.minValueDatePicker);
        if (+serverDate == +minValueDate) {
            this.isDatePickerEnabled = false;
        }
        if (!filter.firstDayForFixingBalance || +serverDate < +minValueDate || !filter.hasFinOpsAfterLastFixing) {
            this.isDatePickerEnabled = false;
            this.isSubmitEnabled = false;
        }

    }

    resetAllData() {
        this.isSubmitEnabled = true;
        this.isDatePickerEnabled = true;
        this.minValueDatePicker = new Date();
        this.serverDate = "";
    }

    fixBalance() {
        let balance = new BalanceViewModel();
        balance.balanceDate = this.currentDate;
        balance.orgAccountId = this.accountId;
        this.fixingBalanceService.fixBalance(balance)
            .subscribe(data => {
                this.message = this.seccessFixingBalance;
                this.showToast();
                this.getFilterFromServer();
            },
            error => {
                this.message = error["_body"];
                this.showToast();
                this.isSubmitEnabled = false;
                this.isDatePickerEnabled = false;
            });
    }

    setCurrentDate(date: Date) {
        this.currentDate = date.toString();
    }

    public showToast() {
        this.toasterClass = "show";
        setTimeout(() => {
            this.toasterClass = "";
            this.closeModalEvent.emit(true);
        }, 3000);
    }
}
