import { Component, OnInit, OnChanges, Input, EventEmitter,Output } from "@angular/core";
import { Observable } from "rxjs/Observable";
import "rxjs/add/operator/map";
import "rxjs/add/operator/catch";
import "rxjs/add/observable/range";
import "rxjs/add/operator/filter"
import "rxjs/add/operator/toArray"

@Component({
    selector: 'pagination-pages',
    templateUrl: './pagination.component.html'
})

/**
* Generic class for pagination buttons
*/
export class PaginationComponent implements OnInit, OnChanges {
    @Input() offset: number;
    @Input() limit: number;
    @Input() size: number;
    @Input() range: number = 2;
    @Input() currentPage: number = 1;

    @Output() pageChange: EventEmitter<number> = new EventEmitter();

    // total number of items
    private totalPages: number;

    //pages to display
    private pages: Observable<number[]>;

    /**
     * Creates new instance of PaginationComponent
     */
    constructor() { }

    /**
     * Trigers when component is constructed
     */
    ngOnInit() {
        this.totalPages = this.getTotalPages(this.limit, this.size);
        this.getPages(this.offset, this.limit, this.size);
    }

    /**
     * Trigers when component is changed
     */
    ngOnChanges() {
        this.getPages(this.offset, this.limit, this.size);
    }

    /**
     * Gets current selected page
     * @param offset
     * @param limit
     */
    getCurrentPage(offset: number, limit: number): number {
        return Math.floor(offset / limit) + 1;
    }

    /**
     * Gets total pages to display
     * @param limit
     * @param size
     */
    getTotalPages(limit: number, size: number): number {
        return Math.ceil(Math.max(size, 1) / Math.max(limit, 1));
    }

    /**
     * Cheacks if the page number is valid number
     * @param page
     * @param totalPages
     */
    isValidPageNumber(page: number, totalPages: number): boolean {
        return page > 0 && page <= totalPages;
    }

    /**
     * Dinamicly calculating and displaing pages
     * @param offset
     * @param limit
     * @param size
     */
    getPages(offset: number, limit: number, size: number) {
        this.currentPage = this.getCurrentPage(offset, limit);
        this.totalPages = this.getTotalPages(this.limit, this.size);

        this.pages = Observable.range(-this.range, this.range * 2 + 1)
            .map(offset => this.currentPage + offset)
            .filter(page => this.isValidPageNumber(page, this.totalPages))
            .toArray();
    }

    /**
     * Trigers when the user selecs page 
     * @param page
     */
    selectPage(page: number) {
        if (this.isValidPageNumber(page, this.totalPages)) {
            this.pageChange.emit(page);
        }
    }
}