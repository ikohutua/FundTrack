import { Injectable } from "@angular/core";
import { Http } from "@angular/http";
import { BaseService } from "../../abstract/base-service";
import { OrganizationGeneralViewModel } from "../../../view-models/concrete/organization-general-view.model";

@Injectable()
export class OrganizationGetGeneralInfoService extends BaseService<OrganizationGeneralViewModel>{
    /**
 * @constructor
 * @param http
 */
    constructor(http: Http) {
        super(http);
    }
}
