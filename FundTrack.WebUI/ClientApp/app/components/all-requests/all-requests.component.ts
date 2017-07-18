import { Component, OnInit, OnDestroy } from '@angular/core';
import { ShowRequestedItem } from "../../view-models/concrete/showrequesteditem-model.interface";
import { ShowRequestedItemService } from "../../services/concrete/showrequesteditem.service";
import { StorageService } from "../../shared/item-storage-service";
import { RequestedItemInitViewModel } from "../../view-models/abstract/requesteditem-initpaginationdata-view-model";
import { FilterRequstedViewModel } from '../../view-models/concrete/filter-requests-view.model';

@Component({
    templateUrl: './all-requests.component.html',
    styleUrls: ['./all-requests.component.css'],
    providers: [ShowRequestedItemService]
})

export class AllRequestsComponent implements OnInit, OnDestroy {
    private _errorMessage: string;
    private _isFilter: boolean;
    public totalItems;
    public itemsPerPage: number;
    public currentPage: number = 1;
    public offset: number = 0;

    private _model: ShowRequestedItem[] = new Array<ShowRequestedItem>();
    private _urlAllrequeatedItems: string = 'api/RequestedItems/AllEventsByScroll';
    private _filters:FilterRequstedViewModel=new FilterRequstedViewModel();

    public organizationFilter: string;
    public categoryFilter: string;
    public typeFilter: string;
    public statusFilter: string;

    constructor(private _service: ShowRequestedItemService, private _serviceStorageService: StorageService) { }

    private getRequestedItemsList(id?: number): void {
        this._service.getCollection()
            .subscribe(model => { this._model = model },
            error => this._errorMessage = <any>error);
    }


    ngOnInit(): void {
        this._serviceStorageService.showDropDown = false;
        this._isFilter = false;
        this._service.getRequestedItemInitData().subscribe((data: RequestedItemInitViewModel) => {
            this.totalItems = data.totalItemsCount;
            this.itemsPerPage = data.itemsPerPage;
            let filters;
            this._service.getRequestedItemOnPage(this.itemsPerPage, this.currentPage, filters).subscribe((requesteditem: ShowRequestedItem[]) => {
                this._model = requesteditem;
            });
        });
    }

    ngOnDestroy(): void {
        this._serviceStorageService.showDropDown = true;
    }

    public onPageChange(page): void {
        let filters: any;
        if (this._isFilter) {
            filters = this.setupFilters();
        }
        this._service.getRequestedItemOnPage(this.itemsPerPage, page, filters).subscribe((requesteditem: ShowRequestedItem[]) => {
            this._model = requesteditem;
            this.offset = (page - 1) * this.itemsPerPage;
        });
    }

    public setFilters() {
        this._isFilter = true;
        this.organizationFilter = '';
        this.categoryFilter = '';
        this.typeFilter = '';
        this.statusFilter = 'Новий';
        this._service.getFilterRequestedItemInitData(this.setupFilters()).subscribe((data: RequestedItemInitViewModel) => {
            this.totalItems = data.totalItemsCount;
            this.itemsPerPage = data.itemsPerPage;
            this._service.getRequestedItemOnPage(this.itemsPerPage, this.currentPage, this.setupFilters()).subscribe((requesteditem: ShowRequestedItem[]) => {
                this._model = requesteditem;
                console.log(this._model);
            });
        });
    }

    private setupFilters():FilterRequstedViewModel{
        this._filters.organizationFilter=this.organizationFilter;
        this._filters.categoryFilter=this.categoryFilter;
        this._filters.typeFilter=this.typeFilter;
        this._filters.statusFilter=this.statusFilter;
        return this._filters;
    }
}
