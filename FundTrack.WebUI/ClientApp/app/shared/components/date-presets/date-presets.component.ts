import { Component, ViewChild, OnInit, EventEmitter, Output } from "@angular/core";
import { ModalComponent } from "../modal/modal-component";
import * as moment from "moment/moment";
import { DatePeriod } from "./date-period-class";

interface IPresets {
    name: string;
    id: number;
}

@Component({
    selector: "date-presets",
    templateUrl: "./date-presets.component.html",
    styleUrls: ["./date-presets.component.css"],
})



export class DatePresetsComponent implements OnInit {

    private readonly DATE_FORMAT = "YYYY-MM-DD";
    private inputMaxDate = moment().format(this.DATE_FORMAT);
    private dateFromIn = moment().format(this.DATE_FORMAT);
    private dateToIn = moment().format(this.DATE_FORMAT);
    private dropboxValue: number = 0;
    @Output() datePeriod = new EventEmitter<DatePeriod>();

    selectedPreset: number = 0;
    private presets: IPresets[] = [{ name: "Цей місяць", id: 0 },
    { name: "Минулий місяць", id: 1 },
    { name: "Цей тиждень", id: 2 },
    { name: "Минулий тиждень", id: 3 },
    { name: "Користувацький", id: 4 }];

    @ViewChild("dateExceptionModal")
    private dateExceptionModal: ModalComponent;

    @ViewChild("dateCompareExceptionModal")
    private dateCompareExceptionModal: ModalComponent;


    ngOnInit(): void {
        this.dateFromIn = moment().subtract(1, "month").format(this.DATE_FORMAT);
    }


    public setBeginDate(beginDate: Date): void {
        this.dateFromIn = moment(beginDate).format(this.DATE_FORMAT);
        if (this.isDateValid()) {
            //this.dateFrom.emit(this.dateFromIn);
            this.datePeriod.emit(new DatePeriod(this.dateFromIn, this.dateToIn));
            this.selectedPreset = 4;
        }
    }

    public setEndDate(endDate: Date): void {
        this.dateToIn = moment(endDate).format(this.DATE_FORMAT);
        if (this.isDateValid()) {
            //this.dateTo.emit(this.dateToIn);
            this.datePeriod.emit(new DatePeriod(this.dateFromIn, this.dateToIn));
            this.selectedPreset = 4;
        }
    }

    onDropBoxChange(value) {
        this.selectedPreset = value;
        this.dateFromIn = moment().format(this.DATE_FORMAT);
        this.dateToIn = moment().format(this.DATE_FORMAT);
        switch (value) {
            case "0"://this month
                this.dateFromIn = moment().startOf("month").format(this.DATE_FORMAT);
                break;
            case "1"://last month
                this.dateFromIn = moment().subtract(1, "months").startOf("month").format(this.DATE_FORMAT);
                this.dateToIn = moment().subtract(1, "months").endOf("month").format(this.DATE_FORMAT);
                break;
            case "2"://this week 
                this.dateFromIn = moment().startOf("isoWeek").format(this.DATE_FORMAT);
                break;
            case "3"://last week
                this.dateFromIn = moment().subtract(1, "weeks").startOf("isoWeek").format(this.DATE_FORMAT);
                this.dateToIn = moment().subtract(1, "weeks").endOf("isoWeek").format(this.DATE_FORMAT);
                break;
            default:
                this.dateFromIn = moment().subtract(1, "month").format(this.DATE_FORMAT);
        }
        if (this.isDateValid()) {
            this.datePeriod.emit(new DatePeriod(this.dateFromIn, this.dateToIn));
  
        }
    }

    isDateValid(): boolean {
        if (!this.dateFromIn || !this.dateToIn) {
            this.openModal(this.dateExceptionModal);
            return false;
        }
        if (this.dateFromIn > this.dateToIn) {
            this.openModal(this.dateCompareExceptionModal);
            return false;
        }
        return true;
    }

    openModal(modal: ModalComponent): void {
        modal.show();
    }

    closeModal(modal: ModalComponent): void {
        modal.hide();
    }

}