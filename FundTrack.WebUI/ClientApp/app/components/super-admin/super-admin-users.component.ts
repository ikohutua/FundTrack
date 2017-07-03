import { Component,OnInit } from '@angular/core';
import { SuperAdminService } from '../../services/concrete/super-admin/super-admin.service';
import { SuperAdminInitViewModel } from '../../view-models/abstract/super-admin.view-models/super-admin-users-init-view-model';
import { SuperAdminItemsViewModel } from '../../view-models/abstract/super-admin.view-models/super-admin-users-view-model';
import { SuperAdminChangeStatusViewModel } from '../../view-models/abstract/super-admin.view-models/super-admin-change-status-view-model';

@Component({
    selector: 'super-admin-users',
    template: require('./super-admin-users.component.html'),
    styleUrls: ['./super-admin-users.component.css'],
    providers: [SuperAdminService]
})

/**
* Component for Users on super admin page
*/
export class SuperAdminUsersComponent implements OnInit {

    /**
    * Total items on page
    */
    public totalItems;

    /**
    * Items on one page
    */
    public itemPerPage: number = 4;

    /**
    * Current Items offset
    */
    public offset: number = 0;

    /**
    * Current Page
    */
    public currentPage: number = 1;

    /**
    * Users View Model
    */
    public users: SuperAdminItemsViewModel[];

    /**
     * Creates new instance of SuperAdminUsersComponent
     * @param _superAdminService
     */
    constructor(private _superAdminService: SuperAdminService) {       
    }

    /**
     * Trigers when Component is created
     */
    public ngOnInit() : void {
        this._superAdminService.getUsersInitData().subscribe((data: SuperAdminInitViewModel) => {
            this.totalItems = data.totalItemsCount;
            this.itemPerPage = data.itemsPerPage;
        });

        this._superAdminService.getUsersOnPage(this.currentPage, this.itemPerPage)
            .subscribe((users: SuperAdminItemsViewModel[]) => {
                this.users = users;
            });
    }

    /**
     * Trigers when user changes page
     * @param page
     */
    public onPageChange(page) : void {
        this._superAdminService.getUsersOnPage(page, this.itemPerPage)
            .subscribe((users: SuperAdminItemsViewModel[]) => {
                this.users = users;
                this.offset = (page - 1) * this.itemPerPage;
            });
    }

    /**
     * Trigers when user changes 
     * @param banStatus
     */
    public changeUserBanStatus(banStatus: SuperAdminChangeStatusViewModel) : void {       
        this._superAdminService.changeUserBanStatus(banStatus).subscribe(() => {         
            let itemToUpdate = this.users.find(u => u.id == banStatus.id);

            itemToUpdate.isBanned = !itemToUpdate.isBanned;
            itemToUpdate.bannDescription = itemToUpdate.isBanned == true ? banStatus.banDescription : '';
        });
    }

    /**
     * Trigers when user changes items to display on page
     * @param amount
     */
    public itemsPerPageChange(amount: number) : void {   
        this._superAdminService.getUsersOnPage(1, amount)
            .subscribe((users: SuperAdminItemsViewModel[]) => {               
                this.offset = 0;
                this.users = users;
                this.itemPerPage = amount;
            });
    }
}