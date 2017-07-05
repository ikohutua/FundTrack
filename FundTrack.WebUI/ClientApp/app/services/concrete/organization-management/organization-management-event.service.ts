import { BaseService } from "../../abstract/base-service";
import { Http } from "@angular/http";
import { IEventModel } from "../../../view-models/abstract/event-model.interface";
import { Injectable } from "@angular/core";

@Injectable()
export class OrganizationManagementEventService extends BaseService<IEventModel> {
    constructor(http: Http) {
        super(http,'api/Event');
    }
}