import { Component, OnInit } from '@angular/core';
import { SuperAdminService } from '../../services/concrete/super-admin/super-admin.service';
import { SuperAdminInitViewModel } from '../../view-models/abstract/super-admin.view-models/super-admin-users-init-view-model';
import { SuperAdminItemsViewModel } from '../../view-models/abstract/super-admin.view-models/super-admin-users-view-model';
import { SuperAdminChangeStatusViewModel } from '../../view-models/abstract/super-admin.view-models/super-admin-change-status-view-model';

@Component({
    selector: 'super-admin-organizations',
    template: require('./super-admin-organizations.component.html'),
    providers: [SuperAdminService]
})

/**
* Component for Organizations on super admin page
*/
export class SuperAdminOrganizationsComponent implements OnInit {
    private totalItems;
    private itemPerPage : number = 2;
    private offset: number = 0;
    private currentPage: number = 1;
    private headers: string[] = ['Назва Організації', 'Дія'];
    private numbers: number[] = [2, 3, 4, 5, 6, 7, 8, 9, 10];

    private organizations: SuperAdminItemsViewModel[];

    /**
     * Creates new instance of SuperAdminOrganizationsComponent
     * @param _superAdminService
     */
    constructor(private _superAdminService: SuperAdminService) {
    }

    /**
     * Trigers when Component is created
     */
    ngOnInit() {
        this._superAdminService.getOrganizationInitData().subscribe((data: SuperAdminInitViewModel) => {
            this.totalItems = data.totalItemsCount;
            this.itemPerPage = data.itemsPerPage;
        });

        this._superAdminService.getOrganizationsOnPage(this.currentPage, this.itemPerPage)
            .subscribe((organizations: SuperAdminItemsViewModel[]) => {
                this.organizations = organizations;
            });
    }

    /**
     * Trigers when user changes page
     * @param page
     */
    onPageChange(page) {
        this._superAdminService.getOrganizationsOnPage(page, this.itemPerPage)
            .subscribe((organizations: SuperAdminItemsViewModel[]) => {
                this.organizations = organizations;
                this.offset = (page - 1) * this.itemPerPage;
            });
    }

    /**
     * Trigers when user changes organization ban status
     * @param banStatus
     */
    changeOrganizationBanStatus(banStatus: SuperAdminChangeStatusViewModel) {
        this._superAdminService.changeOrganizationBanStatus(banStatus).subscribe(() => {
            let itemToUpdate = this.organizations.find(o => o.id == banStatus.id);

            itemToUpdate.isBanned = !itemToUpdate.isBanned;
            itemToUpdate.bannDescription = itemToUpdate.isBanned == true ? banStatus.banDescription : '';
        });
    }

    /**
     * Trigers when user changes items per page to display
     * @param amount
     */
    itemsPerPageChange(amount: number) {
        this._superAdminService.getOrganizationsOnPage(1, amount)
            .subscribe((organizations: SuperAdminItemsViewModel[]) => {
                this.offset = 0;
                this.organizations = organizations;
                this.itemPerPage = amount;
            });
    }
}