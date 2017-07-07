import { Injectable } from "@angular/core";
import { Http, Response, Headers, RequestOptionsArgs } from "@angular/http";
import { IEventManagementViewModel } from "../../../view-models/abstract/organization-management-view-models/event-management-view-model.interface";
import { Observable } from "rxjs/Observable";
import "rxjs/add/operator/map";
import 'rxjs/add/operator/do';
import "rxjs/add/operator/catch";

@Injectable()
export class OrganizationManagementEventsService {
    private _url: string = 'api/EventManagement/GetAllEventsByOrganizationId';

    public constructor(private _http: Http) { }

    /**
     * Gets all events by organization id
     * @param id
     * @returns collection of all events
     */
    public GetAllEventsByOrganizationId(id: number): Observable<IEventManagementViewModel[]> {
        return this._http.get(this._url + '/' + id)
            .map((response: Response) => <IEventManagementViewModel[]>response.json())
            .do(data => console.log('ALL ' + JSON.stringify(data)))
            .catch(this.handleError);
    }

    /**
     * Creates RequestOptionsArgs
     * @param body:T
     * @returns interface RequestOptionsArgs
     */
    private getRequestArgs(body: IEventManagementViewModel): RequestOptionsArgs {
        let headers = new Headers({ 'ContentType': 'application/json' });
        return { headers: headers, body: body };
    }

    /**
    * Exception handler
    * @param error: Response
    */
    private handleError(error: Response) {
        return Observable.throw(error.json().error || 'Server error');
    }
}