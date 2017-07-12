import { Injectable } from "@angular/core";
import { Http, Response, Headers, RequestOptionsArgs } from "@angular/http";
import { IEventManagementViewModel } from "../../../view-models/abstract/organization-management-view-models/event-management-view-model.interface";
import { Observable } from "rxjs/Observable";
import "rxjs/add/operator/map";
import 'rxjs/add/operator/do';
import "rxjs/add/operator/catch";

@Injectable()
export class OrganizationManagementEventsService {
    private _url: string = 'api/EventManagement/';

    public constructor(private _http: Http) { }

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

    /**
     * Adds new event
     * @param newEvent
     * @returns new event - Observable<IEventManagementViewModel>
     */
    public addNewEvent(newEvent: IEventManagementViewModel): Observable<IEventManagementViewModel> {
        let body = newEvent;
        return this._http.post(this._url + 'AddNewEvent/', body, this.getRequestArgs(newEvent))
            .map((response: Response) => <IEventManagementViewModel>response.json())
            .do(data => console.log('ALL ' + JSON.stringify(data)))
            .catch(this.handleError);
    }

    /**
     * Gets all events by organization id
     * @param id
     * @returns Collection of all events - Observable<IEventManagementViewModel[]>
     */
    public getAllEventsByOrganizationId(id: number): Observable<IEventManagementViewModel[]> {
        return this._http.get(this._url + 'GetAllEventsByOrganizationId/' + id)
            .map((response: Response) => <IEventManagementViewModel[]>response.json())
            .do(data => console.log('ALL ' + JSON.stringify(data)))
            .catch(this.handleError);
    }

    /**
     * Gets one event by event id
     * @param id
     * @returns One event - Observable<IEventManagementViewModel>
     */
    public getOneEventById(id: number): Observable<IEventManagementViewModel> {
        return this._http.get(this._url + "GetOneEventById/" + id)
            .map((response: Response) => <IEventManagementViewModel>response.json())
            .do(data => console.log('All ' + JSON.stringify(data)))
            .catch(this.handleError);
    }

    /**
     * Deletes the event
     * @param id
     */
    public deleteEvent(id: number) {
        return this._http.delete(this._url + 'DeleteEvent/' + id);
    }
}