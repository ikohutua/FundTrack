import { Component, OnInit, Input, ViewChild} from '@angular/core';
import { Subscription } from "rxjs/Subscription";
import { ActivatedRoute, Router } from "@angular/router";
import { RequestManagementViewModel } from '../../view-models/abstract/organization-management-view-models/request-management-view-model';
import { OrganizationManagementRequestService } from "../../services/concrete/organization-management/organization-management-request.service";
import { GoodsTypeViewModel } from "../../view-models/concrete/goodsType-view.model";
import { GoodsCategoryViewModel } from "../../view-models/concrete/goodsCategory-view.model";
import { RequestedImageViewModel } from "../../view-models/abstract/organization-management-view-models/requested-item-view.model";
import { SpinnerComponent } from "../../shared/components/spinner/spinner.component";

@Component({
    selector: 'manage-request',
    template: require('./organization-manage-request.component.html'),
    styles: [require('./organization-manage-request.component.css')],
    providers: [OrganizationManagementRequestService]
})

export class OrganizationManageRequestComponent implements OnInit {

    private _requestedItem: RequestManagementViewModel = new RequestManagementViewModel();
    private _errorMessage: string;
    private _goodsTypes: GoodsTypeViewModel[];
    private _selecteType: GoodsTypeViewModel;
    private _subscription: Subscription;
    private _sub: Subscription;
    private _requestedItemId: number;
    private _currentOrgId: number;
    private _currentImageUrl: string[] = [];
    private _requiredFieldMessage: string = "Обовязкове поле для заповнення";   
    private addingImage: string = "http://www.freeiconspng.com/uploads/add-icon--line-iconset--iconsmind-29.png";

    ngOnInit(): void {
        this._route.params.subscribe(params => {
            this._currentOrgId = +params["idOrganization"];
            this._requestedItemId = +params["idRequest"];
        });
        this._requestedItem.images = [];
        this.fillGoodtypes();
        
        if (this._requestedItemId) {
            this.getByIdRequestedItem(this._requestedItemId);          
        }       
    }

    /**
     * Initialize new instance of OrganizationCreateRequestComponent
     * @param _service
     */
    constructor(private _service: OrganizationManagementRequestService,
                private _route: ActivatedRoute,
                private _router: Router) { }

    /**
     * Fills goods type dropdown
     */
    private fillGoodtypes() {
        this._service.getAllGoodsTypes()
            .subscribe(r => {
                this._goodsTypes = r;
            },
            error => {
                this._errorMessage = <any>error;
            })
    }

    /**
     * Checks if type selected
     */
    private isTypeSelected() {
        if (this._selecteType != null && this._selecteType.id != undefined) {
            return true;
        }
        return false;
    }

    /**
     * Checks if category selected
     */
    private isCategorySelected() {
        if (this._requestedItem.goodsCategoryId != undefined &&
            !Number.isNaN(Number(this._requestedItem.goodsCategoryId))) {
            return true;
        }
        return false;
    }

    /**
     * Add new requested item
     */
    private addRequestedItem() {
        this._requestedItem.goodsTypeId = this._selecteType.id;
        this._requestedItem.organizationId = 1;
        this._service.addRequestedItem(this._requestedItem)
            .subscribe((c) => {
                if (c.errorMessage != "") {
                    this._router.navigate(["/organization/requests/1"])
                }
                else {
                    this._errorMessage = c.errorMessage;
                }               
            },
                error => this._errorMessage = <any>error);
    }

    /**
     * Edit requested item
     * @param itemId
     */
    private getByIdRequestedItem(itemId: number) {
        this._service.getRequestedItemById(itemId)
            .subscribe(c => {
                if (c.errorMessage != "") {
                    this._requestedItem = c;
                    this.setGoodsType(this._requestedItem.goodsTypeId);
                }
                else {
                    this._errorMessage = c.errorMessage;
                }
            },
            error => this._errorMessage = <any>error);
    }

    /**
     * delete current image from database or from list
     * @param currentImage
     */
    private deleteCurrentImage(currentImage: RequestedImageViewModel) {
        if (currentImage.id > 0) {
            this._service.deleteCurrentImage(currentImage.id)
                .subscribe(data => this.deleteImageFromList(currentImage.imageUrl),
                error => this._errorMessage = <any>error);
        }
        else {
            this.deleteImageFromList(currentImage.imageUrl);
        }
    }

    /**
     * Delete image from list
     * @param imageUrl
     */
    private deleteImageFromList(imageUrl: string) {
        this._requestedItem.images.splice(this._requestedItem.images.findIndex(i => i.imageUrl == imageUrl), 1)         
    }

    /**
     * Edit requested item
     * @param item
     */
    private editRequestetItem(item: RequestManagementViewModel) {
        this._service.editRequestedItem(item)
            .subscribe(r => {
                    this._requestedItem = r,
                    this._router.navigate(['/organization/requests/1']),
                    error => this._errorMessage = <any>error
            });
    }

    /**
     * Manage requested items wich method will be called
     */
    private manageRequestedItems() {
        if (this._requestedItemId > 0) {
            this.editRequestetItem(this._requestedItem);
        }
        else {
            this.addRequestedItem();
        }
    }

    /**
     * Navigate to all requested items page
     */
    private backToAllItems(): void {
        this._router.navigate(["/organization/requests/1"]);
    }

    /**
     * Set goodType by id for dropdown
     * @param goodTypeId
     */
    private setGoodsType(goodTypeId: number) {
        this._selecteType = this._goodsTypes.find(c => c.id == goodTypeId);
    }

    /**
     * Gets extension of specified file
     * @param fileName: name of the file extension of which is needed to be retrieved
     */
    private getFileExtension(fileName: string): string {
        return fileName.substring(fileName.lastIndexOf('.') + 1, fileName.length) || fileName;
    }
}
