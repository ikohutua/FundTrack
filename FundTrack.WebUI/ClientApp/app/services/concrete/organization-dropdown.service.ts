import { Injectable } from "@angular/core";
import { IOrganizationForFiltering } from "../../view-models/abstract/organization-for-filtering.interface";
import { BaseService } from "../abstract/base-service";
import { Http } from "@angular/http";

@Injectable()
export class OrganizationDropdownService extends BaseService<IOrganizationForFiltering> {
    /**
     * @constructor
     * @param http
     */
    constructor(private http: Http) {
        super(http, 'api/OrganizationsList');
    }
}