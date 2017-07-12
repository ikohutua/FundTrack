import { Component, OnInit, OnDestroy } from '@angular/core';
import { ShowRequestedItem } from "../../view-models/concrete/showrequesteditem-model.interface";
import { ShowRequestedItemService } from "../../services/concrete/showrequesteditem.service";
import { StorageService } from "../../shared/item-storage-service";
import { RequestedItemInitViewModel } from "../../view-models/abstract/requesteditem-initpaginationdata-view-model";

@Component({
    templateUrl: './all-requests.component.html',
    styleUrls: ['./all-requests.component.css'],
    providers: [ShowRequestedItemService]
})

export class AllRequestsComponent implements OnInit, OnDestroy {
    private _errorMessage: string;
    public totalItems;
    public itemsPerPage: number;
    public currentPage: number = 1;
    public offset: number = 0;

    private _model: ShowRequestedItem[] = new Array<ShowRequestedItem>();
    private _urlAllrequeatedItems: string = 'api/RequestedItems/AllEventsByScroll';

    constructor(private _service: ShowRequestedItemService, private _serviceStorageService: StorageService) { }

    private getRequestedItemsList(id?: number): void {
        this._service.getCollection()
                     .subscribe(model => { this._model = model },
                     error => this._errorMessage = <any>error);
    }


    ngOnInit(): void {
        this._serviceStorageService.showDropDown = false;

        this._service.getRequestedItemInitData().subscribe((data: RequestedItemInitViewModel) => {
            this.totalItems = data.totalItemsCount;
            this.itemsPerPage = data.itemsPerPage;
            console.log(this.totalItems);
            console.log(this.itemsPerPage);

            this._service.getRequestedItemOnPage(this.itemsPerPage, this.currentPage).subscribe((requesteditem: ShowRequestedItem[]) => {
                    this._model = requesteditem;
                });
        });

        //this.getRequestedItemsList();
    }

    ngOnDestroy(): void {
        this._serviceStorageService.showDropDown = true;
    }

    public onPageChange(page): void {
        this._service.getRequestedItemOnPage(this.itemsPerPage, page).subscribe((requesteditem: ShowRequestedItem[]) => {
            this._model = requesteditem;
            this.offset = (page - 1) * this.itemsPerPage;
            });
    }

}
