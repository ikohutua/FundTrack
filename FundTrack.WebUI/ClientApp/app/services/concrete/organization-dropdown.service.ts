import { Injectable } from "@angular/core";
import { IOrganizationForLayout } from "../../view-models/abstract/organization-for-layout.interface";
import { Service } from "../abstract/service.abstract-class";
import { Http } from "@angular/http";

@Injectable()
export class OrganizationDropdownService extends Service<IOrganizationForLayout> {
    /**
     * @constructor
     * @param http
     */
    constructor(http: Http) {
        super(http, 'api/OrganizationsList');
    }
}