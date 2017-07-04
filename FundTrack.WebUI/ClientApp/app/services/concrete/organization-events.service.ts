import { Injectable } from "@angular/core";
import { Http, Response, Headers, RequestOptionsArgs } from "@angular/http";
import { BaseService } from "../abstract/base-service";
import { IEventModel } from "../../view-models/abstract/event-model.interface";
import { EventModel } from "../../view-models/concrete/event-model";
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/do';

import { Observable } from "rxjs/Observable";

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