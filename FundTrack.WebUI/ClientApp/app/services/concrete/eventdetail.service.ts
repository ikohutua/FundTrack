import { Injectable } from "@angular/core";
import { Http } from "@angular/http";
import { BaseService } from "../abstract/base-service";
import { IEventDetailModel } from "../../view-models/abstract/eventdetail-model.interface";

@Injectable()
export class EventDetailService extends BaseService<IEventDetailModel>{
    /**
     * @constructor
     * @param http
     */
    constructor(http: Http) {
        super(http);
    }
}
