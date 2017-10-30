import * as _ from 'underscore';

export class Pager {
    public totalItems: number;
    public currentPage: number;
    public pageSize: number;
    public totalPages: number;
    public startPage: number;
    public endPage: number;
    public startIndex: number;
    public endIndex: number;
    public pages: number[];

    public getPager(totalItems: number, currentPage: number = 1, pageSize: number): Pager {
        this.totalItems = totalItems;
        this.currentPage = currentPage;
        this.pageSize = pageSize;
        // calculate total pages
        this.totalPages = Math.ceil(this.totalItems / this.pageSize);

        if (this.totalPages <= 10) {
            // less than 10 total pages so show all
            this.startPage = 1;
            this.endPage = this.totalPages;
        } else {
            // more than 10 total pages so calculate start and end pages
            if (this.currentPage <= 6) {
                this.startPage = 1;
                this.endPage = 10;
            } else if (this.currentPage + 4 >= this.totalPages) {
                this.startPage = this.totalPages - 9;
                this.endPage = this.totalPages;
            } else {
                this.startPage = this.currentPage - 5;
                this.endPage = this.currentPage + 4;
            }
        }

        // calculate start and end item indexes
        this.startIndex = (this.currentPage - 1) * this.pageSize;
        this.endIndex = Math.min(this.startIndex + this.pageSize - 1, this.totalItems - 1);

        // create an array of pages to ng-repeat in the pager control
        this.pages = _.range(this.startPage, this.endPage + 1);

        return this;
    };
}