import { Injectable } from "@angular/core";
import { Http } from "@angular/http";
import { BaseService } from "../abstract/base-service";
import { IEventModel } from "../../view-models/abstract/event-model.interface";

@Injectable()
export class OrganizationEventService extends BaseService<IEventModel>{
    /**
 * @constructor
 * @param http
 */
    constructor(http: Http) {
        super(http, 'api/Event/AllEvents');
    }
}
