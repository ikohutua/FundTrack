import { Component, OnInit, ViewChild, OnDestroy, DoCheck, AfterViewInit, QueryList, ViewChildren, OnChanges} from "@angular/core";
import { Router, ActivatedRoute } from '@angular/router';
import { FormControl, FormGroup } from '@angular/forms';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { OrganizationGetGeneralInfoService } from '../../services/concrete/organization-management/organization-get-general-info.service';
import { OrganizationGeneralViewModel } from '../../view-models/concrete/organization-general-view.model';
import { EditOrganizationService } from '../../services/concrete/organization-management/edit-organization.service'
import { MapComponent } from '../../shared/components/map/map.component';
import { OrgAddressViewModel } from '../../view-models/concrete/edit-organization/org-address-view.model';
import { ModalComponent } from '../../shared/components/modal/modal-component';
import { IAddressViewModel } from '../../view-models/abstract/address-model.interface';
import { AddressViewModel } from '../../view-models/concrete/edit-organization/address-view.model';
import { ModeratorViewModel } from '../../view-models/concrete/edit-organization/moderator-view.model';
import { AddModeratorViewModel } from "../../view-models/concrete/edit-organization/add-moderator-view.model";
import { isBrowser } from "angular2-universal";
import { AuthorizeUserModel } from "../../view-models/concrete/authorized-user-info-view.model";
import * as key from '../../shared/key.storage';

@Component({
    selector: 'edit',
    templateUrl: './organization-edit.component.html',
    styleUrls: ['./organization-edit.component.css'],
    providers: [OrganizationGetGeneralInfoService, EditOrganizationService]
})
export class OrganizationEditComponent implements OnInit, OnDestroy, AfterViewInit {
    googleQuote: number = 5;
    minimumAddresses: number = 1;
    disableDelete: boolean = false;
    organizationId: number;
    organization: OrganizationGeneralViewModel;
    private sub: any;
    orgAddress: OrgAddressViewModel = new OrgAddressViewModel();
    newOrgAddress: OrgAddressViewModel = new OrgAddressViewModel();
    newAddress: AddressViewModel = new AddressViewModel();
    private user: AuthorizeUserModel = new AuthorizeUserModel();
    isAdmin: boolean = false;

    currentFile: File;
    isNewLogoAvailable: boolean = false;

    newLogo: string = '';
    orgLogo: string = "http://cadeuc.com/wp-content/themes/musen/img/no-image.png";
    maxSize: number = 4000000;
    newLogoByte64Code: string;

    errorMessage: string;
    isError: boolean = false;

    //modal window to add moderator
    @ViewChild("moderator")
    public modal: ModalComponent;

    //modal window to edit address
    @ViewChild("mapModal")
    public mapModal: ModalComponent;

    //modal window to add address
    @ViewChild("addAddressModal")
    public addAddressModal: ModalComponent;

    //maps 
    @ViewChildren(MapComponent)
    public Maps: QueryList<MapComponent>;
    private map: MapComponent;
    private mapInModal: MapComponent;

    

    addressArray: Array<AddressViewModel> = new Array(this.googleQuote);
    moderatorArray: ModeratorViewModel[];
    possibleModerators: AddModeratorViewModel[];
    isAnyModerators: boolean = false;
    isAddressShown: boolean = false;
    newModerator: AddModeratorViewModel = new AddModeratorViewModel();
    addressToEdit: AddressViewModel = new AddressViewModel();
    isModalShown: boolean = false;
    constructor(private _actRouter: ActivatedRoute, private _getInfoService: OrganizationGetGeneralInfoService, private _editService: EditOrganizationService) {

    }

    ngOnInit() {
        this.sub = this._actRouter.params.subscribe(params => {
            this.organizationId = +params["id"];
                this.getInformationOfOrganization(this.organizationId);
                this.getAddresses(this.organizationId);
                this.getModerators(this.organizationId);
                this.getPossibleModerators(this.organizationId);
        });
        if (isBrowser) {
            if (localStorage.getItem(key.keyToken)) {
                this.user = JSON.parse(localStorage.getItem(key.keyModel)) as AuthorizeUserModel;
            }
        };
        if (this.user.role === 'admin')
            this.isAdmin = true;
    }

    ngAfterViewInit() {
        this.Maps.changes.subscribe((maps: QueryList<MapComponent>) => {
            this.map = maps.first;
            this.mapInModal = maps.last;
            this.setMap(this.map);            
        });        
    }

    ngOnDestroy() {
        this.sub.unsubscribe;
    }

    /**
    * gets information about organization by its id
    * @param id
    */
    private getInformationOfOrganization(id: number): void {
        this._getInfoService.getById(id, 'api/OrganizationProfile/GetInformationById')
            .subscribe(model => {
                this.organization = model;
            })
    }

    /**
     * edits organization description
     */
    editDescriptionOfOrganization(): void {
        this._editService.editDescription(this.organization).subscribe
            (model => {
             
                this.organization = model;
            })
    }

    /**
     * gets addresses of organization by its id
     * @param id
     */
    getAddresses(id: number): void {
        this._editService.getAddress(this.organizationId).subscribe
            (model => {
                this.orgAddress = model;
                this.disableDelete = this.disableButton(this.orgAddress.addresses);
            })
    }

    /**
     * gets moderators of organization by its id
     * @param id
     */
    getModerators(id: number): void {
        this._editService.getModerators(id).subscribe
            (model => {
              
                this.moderatorArray = model;
                if (this.moderatorArray.length > 0) {
                    this.isAnyModerators = true;
                }
            })
    }

    /**
     * shows addresses on the map
     * @param map
     */
    setMap(map: MapComponent): void {
        this.map.setAllMarkersOnTheMap(this.orgAddress.addresses);
      
    }

    /**
     * adds moderator
     */
    addModerator(): void {
        this.newModerator.orgId = this.organizationId;
        this._editService.addModerator(this.newModerator).
            subscribe(model => {
            
                this.getModerators(this.organizationId);
                this.isAnyModerators = true;
                this.getPossibleModerators(this.organizationId);
            });
        this.closeModal(this.modal);
    }

    /**
     * deletes moderaotr of organization
     * @param moderator to delete
     */
    deactivateModerator(moderator: ModeratorViewModel): void {
        this._editService.deactivateModerator(moderator.login).
            subscribe(model => {
                this.moderatorArray.splice(this.moderatorArray.findIndex(m => m.login == moderator.login), 1);
                if (this.moderatorArray.length == 0) {
                    this.isAnyModerators = false;
                }
                this.getPossibleModerators(this.organizationId);
            });
    }

    /**
     * gets list of users
     * @param id
     */
    getPossibleModerators(id: number)
    {
        this._editService.getAvailableUsers(id)
            .subscribe(users => {
                this.possibleModerators = users;
             
            });
    }

    /**
     * deletes address
     * @param addressToDelete
     */
    deleteAddress(addressToDelete: AddressViewModel)
    {
        let length = this.orgAddress.addresses.length;
        this._editService.deleteAddress(addressToDelete.id)
            .subscribe(address => {
                this.orgAddress.addresses
                    .splice(this.orgAddress.addresses
                        .findIndex(a => (a.id == addressToDelete.id), 1));
                this.disableDelete = this.disableButton(this.orgAddress.addresses);
                this.getAddresses(this.organizationId);
            })
    }

    private disableButton(addressArray: AddressViewModel[]): boolean
    {
        if (addressArray.length == this.minimumAddresses) {
            return true;
        } else {
            return false;
        }
    }

    /**
     * adds address
     * @param addressToAdd
     */
    addAddress(addressToAdd: AddressViewModel): void {
        addressToAdd.country = 'Україна';
        this.newOrgAddress.orgId = this.organizationId;
        this.newOrgAddress.addresses = Array <AddressViewModel>();
        this.newOrgAddress.addresses.push(addressToAdd);
        this._editService.addAddresses(this.newOrgAddress).
            subscribe(addresses => {
                this.orgAddress = addresses;
                this.getAddresses(this.organizationId);
                this.setMap(this.map);
                console.log(this.orgAddress.errorMessage);
            });
        this.addAddressModal.hide();
    }

    /**
     * updates address
     * @param addressToEdit
     */
    updateAddress(addressToEdit: AddressViewModel): void {
        console.log("in update address", addressToEdit);
        this._editService.editAddress(addressToEdit).subscribe(
            address => {
                this.orgAddress = address;
                this.getAddresses(this.organizationId);
                this.setMap(this.map);
            }
        )
        this.closeModal(this.mapModal);
    }

    openModalWithMap(modal: ModalComponent, addressToEdit: AddressViewModel): void {
        this.addressToEdit = addressToEdit;
        modal.showEmits();
    }

    /**
     * subscribes to map onChangeAddress event
     * @param addresses
     */
    public showAddress(addresses: IAddressViewModel[]): void {
        console.log(addresses);
        //this.addressToEdit.id = addresses[0].id;
        this.addressToEdit.city = addresses[0].city;
        this.addressToEdit.street = addresses[0].street;
        this.addressToEdit.house = addresses[0].house;
    }

    /**
     * subscribes to modal show event
     */
    public modalOpens(): void {
        this.mapInModal.triggerResize();      
    }

    //open modal window
    openModal(modal: ModalComponent): void {
        modal.show();
    }

    //closes modal
    closeModal(modal: ModalComponent): void {
        modal.hide();
    }

    //get image from input
    handleInputChange(e: any) {
        this.isError = false;
        this.currentFile = e.dataTransfer ? e.dataTransfer.files[0] : e.target.files[0];

        var pattern = /image-*/;
        var reader = new FileReader();

        if (this.currentFile.size > this.maxSize) {
            this.warning('Завеликий розмір зображення. Допустимо ' + this.maxSize / 1000000 + 'Мб');
            return;
        }

        if (!this.currentFile.type.match(pattern)) {
            this.warning('Невірний формат!');
            return;
        }
        reader.onload = this._handleReaderLoaded.bind(this);
        reader.readAsDataURL(this.currentFile);
    }

    _handleReaderLoaded(e: any) {
        var reader = e.target;
        this.newLogo = reader.result;

        var commaInd = this.newLogo.indexOf(',');
        this.newLogoByte64Code = this.newLogo.substring(commaInd + 1);

        this.isNewLogoAvailable = true;
    }

    warning(message: string) {
        this.errorMessage = message;
        this.isError = true;
    }

    //clear new image and set current organization logo
    clearImage() {
        this.isNewLogoAvailable = false;
        this.isError = false;
    }

    //set new organization logo
    saveImage() {
        this.isError = false;
    }
}