import { Injectable } from '@angular/core';
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { SuperAdminInitViewModel } from '../../../view-models/abstract/super-admin.view-models/super-admin-users-init-view-model';
import { SuperAdminItemsViewModel } from '../../../view-models/abstract/super-admin.view-models/super-admin-users-view-model';
import { SuperAdminChangeStatusViewModel } from '../../../view-models/abstract/super-admin.view-models/super-admin-change-status-view-model';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/map';

/**
 * Service for super admin actions
 */
@Injectable()
export class SuperAdminService {
    // urls to server
    private _url: string = 'api/SuperAdmin/GetUsersPaginationData';
    private _pagingUr: string = 'api/SuperAdmin/GetUsersPerPage';
    private _orgUrl: string = 'api/SuperAdmin/GetOrganizationsPaginationData';
    private _orgPagingUrl: string = '/api/SuperAdmin/GetOrganizationsPerPage';
    private _userStatusUrl: string = '/api/SuperAdmin/ChangeUserBanStatus';
    private _orgStatusUrl: string = '/api/SuperAdmin/ChangeOrganizationBanStatus';

    /**
     * Creates new instance of SuperAdminService
     * @param _http
     */
    constructor(private _http: Http) {
    }

    // gets initial pagination data from server
    private getInitData(url) {
        return this._http.get(url)
            .map((response: Response) => response.json() as SuperAdminInitViewModel)
    }

    // gets items to display on page from server
    private getItemsOnPage(url: string, currentPage: number, itemsPerPage: number) {
        return this._http.get(url + '/' + currentPage + '/' + itemsPerPage)
            .map((response: Response) => response.json() as SuperAdminItemsViewModel[])
    }  
    
    // Gets data for request to server
    private getItemBanStatusOptions() {
        let headers = new Headers({ 'ContentType': 'application/json' });
        let options = new RequestOptions({ headers: headers });

        return options;
    }

    /**
     * Gets initial pagination data about users
     */
    public getUsersInitData() {
        return this.getInitData(this._url);
    }

    /**
     * Gets initial pagination data about organizations
     */
    public getOrganizationInitData() {
        return this.getInitData(this._orgUrl);
    }

    /**
     * Gets Users to display on page from server
     * @param currentPage
     * @param itemsPerPage
     */
    public getUsersOnPage(currentPage: number, itemsPerPage: number) {
        return this.getItemsOnPage(this._pagingUr, currentPage, itemsPerPage)
    }

    /**
     * Gets organizations to display on page from server
     * @param currentPage
     * @param itemsPerPage
     */
    public getOrganizationsOnPage(currentPage: number, itemsPerPage: number) {
        return this.getItemsOnPage(this._orgPagingUrl, currentPage, itemsPerPage);
    }

    /**
     * Changes User ban Status
     * @param user
     */
    public changeUserBanStatus(user: SuperAdminChangeStatusViewModel) {  
        return this._http.post(this._userStatusUrl, user, this.getItemBanStatusOptions());
    }

    /**
     * Changes Organization Ban Status
     * @param organization
     */
    public changeOrganizationBanStatus(organization: SuperAdminChangeStatusViewModel) {
        return this._http.post(this._orgStatusUrl, organization, this.getItemBanStatusOptions());
    }
}
