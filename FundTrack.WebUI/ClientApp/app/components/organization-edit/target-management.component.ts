import { Component, OnInit, ViewChild} from "@angular/core";
import { OrganizationGetGeneralInfoService } from '../../services/concrete/organization-management/organization-get-general-info.service';
import { EditOrganizationService } from '../../services/concrete/organization-management/edit-organization.service'
import { OrganizationGeneralViewModel } from '../../view-models/concrete/organization-general-view.model';
import { TargetViewModel } from "../../view-models/concrete/finance/donate/target.view-model"
import { isBrowser } from "angular2-universal";
import * as key from '../../shared/key.storage';
import { AuthorizeUserModel } from "../../view-models/concrete/authorized-user-info-view.model";
import { ModalComponent } from '../../shared/components/modal/modal-component';

@Component({
    selector: 'targets',
    templateUrl: './target-management.component.html',
    styleUrls: ['./target-management.component.css'],
    providers: [OrganizationGetGeneralInfoService, EditOrganizationService]
})

export class TargetManagementComponent implements OnInit {
    private user: AuthorizeUserModel = new AuthorizeUserModel();
    targetArray: TargetViewModel[];
    editableTarget: TargetViewModel;
    //modal window to add new target
    @ViewChild("targetModal")
    public targetModal: ModalComponent;
    //modal window to delete target
    @ViewChild("submitModal")
    public submitModal: ModalComponent;
    // flags of modal component buttons
    isSubTargetSelected: boolean;
    isTargetForEditing: boolean;
    isTargetForAdding: boolean;

    constructor( private _getInfoService: OrganizationGetGeneralInfoService, private _editService: EditOrganizationService) {

    }

    ngOnInit() {
        this.editableTarget = new TargetViewModel();
        if (isBrowser) {
            if (localStorage.getItem(key.keyToken)) {
                this.user = JSON.parse(localStorage.getItem(key.keyModel)) as AuthorizeUserModel;
            }
        };
        this.getTargetsByOrganizationId();

    }

    private ifTargetIsParent(target: TargetViewModel): boolean {
        if (target.parentTargetId == undefined) {
            return true;
        }
        if (target.parentTargetId != undefined) {
            return false;
        }
    }

    private ifFirstTargetIsParentForSecondTarget(firstTarget: TargetViewModel, secondTarget: TargetViewModel): boolean {
        if (firstTarget.targetId === secondTarget.parentTargetId) {
            return true;
        }
        if (firstTarget.targetId !== secondTarget.parentTargetId) {
            return false;
        }
    }

    openModalForSubmitDeleteTarget(target: TargetViewModel) {
        this.editableTarget = target;
        this.openModal(this.submitModal);
    }

    openModal(modal: ModalComponent): void {
        modal.show();
    }

    openModalForEditTarget(target: TargetViewModel) {
        this.isTargetForEditing = true;
        this.editableTarget = Object.assign({}, target);
        this.openModal(this.targetModal);
    }

    openModalForAddTarget() {
        debugger;
        this.isTargetForAdding = true;
        this.editableTarget = new TargetViewModel();
        this.openModal(this.targetModal);
    }

    openModalForAddingSubTarget(target: TargetViewModel) {
        this.isSubTargetSelected = true;
        this.editableTarget = new TargetViewModel();
        this.editableTarget.organizationId = target.organizationId;
        this.editableTarget.parentTargetId = target.targetId;
        this.openModal(this.targetModal);
    }

    //closes modal
    closeModal(modal: ModalComponent): void {
        this.isSubTargetSelected = false;
        this.isTargetForAdding = false;
        this.isTargetForEditing = false;
        modal.hide();
    }

    public addTarget() {
        this.isTargetForAdding = false;
        this.editableTarget.organizationId = this.user.orgId;
        this._editService.addTarget(this.editableTarget).subscribe(model => {
            this.targetArray.push(model);
        });
        this.closeModal(this.targetModal);
    }

    public addSubTarget() {
        this.isSubTargetSelected = false;
        this._editService.addTarget(this.editableTarget).subscribe(model => {
            this.targetArray.push(model);
        });
        this.closeModal(this.targetModal);
    }

    editTarget() {
        this.isTargetForEditing = false;
        this.editableTarget.organizationId = this.user.orgId;
        this._editService.editTarget(this.editableTarget)
            .subscribe(model => {
                this.getTargetsByOrganizationId();
            });
        this.closeModal(this.targetModal);
    }

    getTargetsByOrganizationId() {
        this._editService.getTargetsByOrganizationId(this.user.orgId).subscribe(model => {
            this.targetArray = model;
        });
    }

    deleteTarget() {

        this._editService.deleteTarget(this.editableTarget.targetId).subscribe((model) => {
            this.getTargetsByOrganizationId();
        }, error => {
            this.showToast();
        });
        this.closeModal(this.submitModal);
    }

    public showToast() {
        var x = document.getElementById("snackbar");
        x.className = "show";
        setTimeout(function () { x.className = x.className.replace("show", ""); }, 3000);
    }
}

