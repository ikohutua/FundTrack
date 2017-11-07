import { Injectable, Inject, NgZone } from '@angular/core';
import * as key from '../../shared/key.storage';
import { Http, Response, Headers, RequestOptionsArgs, RequestOptions } from "@angular/http";

@Injectable()
export class RequestOptionsService {
    public static getRequestOptions() {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        headers.append("Authorization", localStorage.getItem(key.keyToken));
        let options = new RequestOptions({ headers: headers });
        return options;
    }
}