export class DatePeriod {
    dateFrom: string;
    dateTo: string;
    constructor (firstDate: string, endDate: string) {
        this.dateFrom = firstDate;
        this.dateTo = endDate;
    }
}