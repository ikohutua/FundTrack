import { Component, OnInit, OnDestroy } from '@angular/core';
import { ShowRequestedItem } from "../../view-models/concrete/showrequesteditem-model.interface";
import { ShowRequestedItemService } from "../../services/concrete/showrequesteditem.service";
import { StorageService } from "../../shared/item-storage-service";
import { RequestedItemInitViewModel } from "../../view-models/abstract/requesteditem-initpaginationdata-view-model";
import { IOrganizationForFiltering } from "../../view-models/abstract/organization-for-filtering.interface";
import { ISearchingDataForRequestedItem } from "../../view-models/abstract/searching-data-for-requesteditems-model";
import { Router } from "@angular/router";
import { GoodsCategoryViewModel } from "../../view-models/concrete/goods-category-view.model";
import { GoodsTypeShortViewModel } from "../../view-models/concrete/goods-type-view.model";
import { GoodsStatusViewModel } from "../../view-models/concrete/goods-status-model";
import { FilterRequstedViewModel } from '../../view-models/concrete/filter-requests-view.model';
import { OrganizationManagementRequestService } from "../../services/concrete/organization-management/organization-management-request.service";
import { GoodsTypeViewModel } from "../../view-models/concrete/goodsType-view.model";

@Component({
    templateUrl: './all-requests.component.html',
    styleUrls: ['./all-requests.component.css'],
    providers: [ShowRequestedItemService, OrganizationManagementRequestService]
})

export class AllRequestsComponent implements OnInit, OnDestroy {
    private _errorMessage: string;
    private _isFilter: boolean;
    public totalItems;
    public itemsPerPage: number;
    public currentPage: number = 1;
    public offset: number = 0;

    private _organization: string = '';
    private _type: string = '';
    private _category: string = '';
    private _status: string = '';

    private _model: ShowRequestedItem[] = new Array<ShowRequestedItem>();
    private _organizations: IOrganizationForFiltering[] = new Array<IOrganizationForFiltering>();
    private _statuses: GoodsStatusViewModel[] = new Array<GoodsStatusViewModel>();
    private _filters: FilterRequstedViewModel = new FilterRequstedViewModel();
    private _goodsTypes: GoodsTypeViewModel[] = new Array<GoodsTypeViewModel>();
    private _selecteType: GoodsTypeViewModel = new GoodsTypeViewModel;

    private _searchingDataForRequestedItem: ISearchingDataForRequestedItem;

    constructor(private _service: ShowRequestedItemService,
        private _storageService: StorageService,
        private _router: Router,
        private _organizationManagementRequest: OrganizationManagementRequestService) { }

    ngOnInit(): void {
        this._storageService.showDropDown = false;
        this.getDataForSearching();
        this.loadAllRequestedItem();
    }

    loadAllRequestedItem() {
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
        this._storageService.showDropDown = true;
    }

    private selectType(): void {
        this._category = '';
        this._selecteType = this._goodsTypes.find(i => i.name == this._type);
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

    getDataForSearching(): void {
        this._service.getOrgaizations().subscribe((organizations: IOrganizationForFiltering[]) => {
            this._organizations = organizations;
        });

        this._service.getStatuses().subscribe((statuses: GoodsStatusViewModel[]) => {
            this._statuses = statuses;
        });

        this._organizationManagementRequest.getAllGoodsTypes().subscribe((goodsTypes: GoodsTypeViewModel[]) => {
            this._goodsTypes = goodsTypes;
        },
            error => {
                this._errorMessage = <any>error;
            })
    }

    public filteredRequestedItems() {
        this._isFilter = true;
        this._service.getFilterRequestedItemInitData(this.setupFilters()).subscribe((data: RequestedItemInitViewModel) => {
            this.totalItems = data.totalItemsCount;
            this._service.getRequestedItemOnPage(this.itemsPerPage, this.currentPage, this.setupFilters()).subscribe((requesteditem: ShowRequestedItem[]) => {
                this._model = requesteditem;
                console.log(this._model);
            });
        });
    }

    cancelFilter() {
        this._organization = '';
        this._type = '';
        this._category = '';
        this._status = '';
        this.loadAllRequestedItem();
    }
    private setupFilters(): FilterRequstedViewModel {
        this._filters.organizationFilter = this._organization;
        this._filters.categoryFilter = this._category;
        this._filters.typeFilter = this._type;
        this._filters.statusFilter = this._status;
        return this._filters;
    }

    onClick(item) {
        this._router.navigate(['home/requestdetail/' + item.id]);
    }

    public itemsPerPageChange(amount: number): void {
        this.offset = 0;
        this.itemsPerPage = amount;
        this.filteredRequestedItems();
    }
}
