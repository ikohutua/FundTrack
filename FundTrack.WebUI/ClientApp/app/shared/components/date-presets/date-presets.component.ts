import { Component, ViewChild, OnInit, EventEmitter, Output, AfterContentChecked, Input } from "@angular/core";
import { isBrowser } from 'angular2-universal';
import { DatePipe } from "@angular/common";
import { ModalComponent } from "../modal/modal-component";

interface IPresets {
    name: string;
    id: number;
}

@Component({
    selector: 'date-presets',
    templateUrl: './date-presets.component.html',
    styleUrls: ['./date-presets.component.css'],
    providers: [DatePipe]
})

export class DatePresetsComponent implements OnInit{

    private inputMaxDate: Date = new Date();
    private dateFromIn: Date = new Date();
    private dateToIn: Date = new Date();
    private dropboxValue: number = 0;
    @Output() dateFrom = new EventEmitter<Date>();
    @Output() dateTo = new EventEmitter<Date>();
    selectedPreset: number = 0;
    private presets: IPresets[] = [{ name: 'Цей місяць', id: 0 },
        { name: 'Минулий місяць', id: 1 },
        { name: 'Цей тиждень', id: 2 },
        { name: 'Минулий тиждень', id: 3 },
        { name: 'Користувацький', id: 4 }];

    @ViewChild("dateExceptionModal")
    private dateExceptionModal: ModalComponent;

    @ViewChild("dateCompareExceptionModal")
    private dateCompareExceptionModal: ModalComponent;


    ngOnInit(): void {
        this.dateFromIn.setMonth(this.dateFromIn.getMonth() - 1);
    }

    /**
     * @constructor
     * @param _service
     */
    constructor(private datePipe: DatePipe) { }


    public setBeginDate(beginDate: Date): void {
        this.dateFromIn = new Date(this.datePipe.transform(beginDate, 'yyyy-MM-dd'));
        if (this.isDateValid()) {
            this.dateFrom.emit(this.dateFromIn);
            this.selectedPreset = 4;
        }
    }

    public setEndDate(endDate: Date): void {
        this.dateToIn = new Date(this.datePipe.transform(endDate, 'yyyy-MM-dd'));
        if (this.isDateValid()) {
            this.dateTo.emit(this.dateToIn);
            this.selectedPreset = 4;
        }
    }

    onDropBoxChange(value) {
        this.selectedPreset = value;
        this.dateFromIn = new Date();
        this.dateToIn = new Date();
        if (value == 0) {//this month    
            this.dateFromIn.setMonth(new Date().getMonth());
            this.dateFromIn.setDate(1);
        }
        if (value == 1) {//last month
            this.dateFromIn.setMonth(this.dateFromIn.getMonth() - 1);
            this.dateFromIn.setDate(1);
            this.dateToIn.setDate(1);
        }
        if (value == 2) {//this week ???
            this.dateFromIn.setDate((new Date().getDate()+1) - new Date().getDay());
        }
        if (value == 3) {//last week 
            this.dateFromIn.setDate(new Date().getDate() - new Date().getDay() - 6);
            this.dateToIn.setDate(new Date().getDate() - new Date().getDay());
        }
        if (value == 4) {//default 
            this.dateFromIn.setMonth(this.dateFromIn.getMonth() - 1);
        }
        if (this.isDateValid()) {
            this.dateFrom.emit(this.dateFromIn);
            this.dateTo.emit(this.dateToIn);
        }
    }
   
    isDateValid(): boolean {
        if (!this.dateFromIn || !this.dateToIn) {
            this.openModal(this.dateExceptionModal);
            return false;
        }
        if (this.datePipe.transform(this.dateFromIn, 'yyyy-MM-dd')
            > this.datePipe.transform(this.dateToIn, 'yyyy-MM-dd')) {
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