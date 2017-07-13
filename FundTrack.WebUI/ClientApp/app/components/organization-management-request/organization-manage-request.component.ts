import { Component, OnInit, Input } from '@angular/core';
import { Subscription } from "rxjs/Subscription";
import { ActivatedRoute, Router } from "@angular/router";
import { RequestManagementViewModel } from '../../view-models/abstract/organization-management-view-models/request-management-view-model';
import { OrganizationManagementRequestService } from "../../services/concrete/organization-management/organization-management-request.service";
import { GoodsTypeViewModel } from "../../view-models/concrete/goodsType-view.model";
import { GoodsCategoryViewModel } from "../../view-models/concrete/goodsCategory-view.model";
import { AmazonUploadComponent } from "../../shared/components/amazonUploader/amazon-upload.component";
import { RequestedImageViewModel } from "../../view-models/abstract/organization-management-view-models/requested-item-view.model";

@Component({
    selector: 'manage-request',
    template: require('./organization-manage-request.component.html'),
    styles: [require('./organization-manage-request.component.css')],
    providers: [OrganizationManagementRequestService]
})

export class OrganizationManageRequestComponent implements OnInit {
    public uploader: AmazonUploadComponent = new AmazonUploadComponent();

    private _requestedItem: RequestManagementViewModel = new RequestManagementViewModel();
    private _errorMessage: string;
    private _goodsTypes: GoodsTypeViewModel[];
    private _selecteType: GoodsTypeViewModel;
    private _subscription: Subscription;
    private _requestedItemId: number;
    private _currentOrgId: number;
    private _currentImageUrl: string[] = [];

    ngOnInit(): void {
        this._requestedItem.images = [];
        this.fillGoodtypes();
        this._route.params.subscribe(params => {
            this._currentOrgId = +params["idOrganization"];
        });
        this._subscription = this._route
            .params.subscribe(params => {
                this._requestedItemId = +params["idRequest"];
            });

        if (this._requestedItemId > 0) {
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
     * Add new requested item
     */
    private addRequestedItem() {
        console.log(this._requestedItem);
        this._service.addRequestedItem(this._requestedItem)
            .subscribe(error => this._errorMessage = <any>error);
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

    private deleteCurrentImage(currentImageId: number) {
        this._service.deleteCurrentImage(currentImageId)
            .subscribe(data => this._requestedItem.images
                .splice(this._requestedItem.images.findIndex(i => i.id == currentImageId), 1),
            error => this._errorMessage = <any>error);
    }

    /**
     * Edit requested item
     * @param item
     */
    private editRequestetItem(item: RequestManagementViewModel) {
        this._service.editRequestedItem(item)
            .subscribe(r => this._requestedItem = r,
            error => this._errorMessage = <any>error);
    }

    /**
     * Manage requested items wich method will be called
     */
    private manageRequestedItems() {
        console.log(this._requestedItem)
        if (this._requestedItemId > 0) {
            this.editRequestetItem(this._requestedItem);
        }
        else {
            this.addRequestedItem();
        }
    }

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

    /**
     * Saves passed file in Amazon Web Storage
     * @param fileInput: file to be saved in AWS
     */
    private saveFileInAws(fileInput: any): void {
        var that = this;
        var maxFileSize = 4000000;
        let file = fileInput.target.files[0];
        let uploadedFileName = this._requestedItem.name + '.' + this.getFileExtension(file.name);
        if (file.size != null && file.size < maxFileSize) {
            this.uploader.UploadImageToAmazon(file, uploadedFileName).then(function (data) {
                let requestedItemImage = new RequestedImageViewModel();
                requestedItemImage.requestedItemId = 0;
                requestedItemImage.imageUrl = data.Location;

                if (that._requestedItem.images == null) {
                    that._requestedItem.images = [];
                }

                that._requestedItem.images.push(requestedItemImage);
            })
        }
        else {
            alert('Розмр файлу не може перевищувати ' + Math.ceil(maxFileSize / 1000000) + 'МБ');
        }
    }
}
