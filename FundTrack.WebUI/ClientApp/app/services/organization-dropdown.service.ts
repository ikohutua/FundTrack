import { Injectable } from "@angular/core";
import { Http, Response } from "@angular/http";
import { Observable } from "rxjs/Observable";
import { IOrganizationForLayout } from "../view-models/abstract/organization-for-layout.interface";
import "rxjs/add/operator/map";
import "rxjs/add/operator/do";
import "rxjs/add/operator/catch";

@Injectable()
export class OrganizationDropdownService {

    private _url: string = 'api/OrganizationsList';

    constructor(private _http: Http) { }

    /**
     * gets list of organizations from backend
     */
    getOrganizations(): Observable<IOrganizationForLayout[]> {
        console.log('getOrganizations()');
        return this._http.get(this._url)
            .map((response: Response) => <IOrganizationForLayout[]>response.json())
            .do(data => console.log('All: ' + JSON.stringify(data)))//console out information about json
            .catch(this.handleError);//call error handler
    }

    /**
     * exception handler
     * @param error: Response
     */
    private handleError(error: Response) {
        return Observable.throw(error.json().error || 'Server error');
    }
}