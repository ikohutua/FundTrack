import { Injectable } from "@angular/core";
import { Observable } from "rxjs/Observable";
import { Http, Response } from "@angular/http";
import { BaseService } from "../abstract/base-service";
import { GoogleGeoCodeResponse } from "../../models/map/google-geo-code-response.model";

/**
 * Service for work with Google Maps API
 */
@Injectable()
export class MapService extends BaseService<GoogleGeoCodeResponse>{
    constructor(_http: Http) {
        super(_http, 'https://maps.googleapis.com/maps/api/geocode/json?');
    }
}