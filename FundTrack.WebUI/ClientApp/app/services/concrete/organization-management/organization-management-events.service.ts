import { Injectable } from "@angular/core";
import { Http, Response, Headers, RequestOptionsArgs } from "@angular/http";
import { IEventManagementViewModel } from "../../../view-models/abstract/organization-management-view-models/event-management-view-model.interface";
import { Observable } from "rxjs/Observable";
import "rxjs/add/operator/map";
import 'rxjs/add/operator/do';
import "rxjs/add/operator/catch";
import { PaginationInitViewModel } from "../../../view-models/abstract/organization-management-view-models/pagination-init-view-model.interface";

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
            .catch(this.handleError);
    }

    /**
     * Gets events per page by organization identifier
     * @param idOrganization
     * @param currentPage
     * @param pageSize
     * @returns Collection of events - Observable<IEventManagementViewModel[]>
     */
    public getEventsByOrganizationIdForPage(idOrganization: number, currentPage: number, pageSize: number): Observable<IEventManagementViewModel[]> {
        return this._http.get(this._url + 'GetEventsByOrganizationIdForPage/' + idOrganization + "/" + currentPage + "/" + pageSize)
            .map((response: Response) => <IEventManagementViewModel[]>response.json())
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
            .catch(this.handleError);
    }

    /**
     * Updates event
     * @param updatedEvent
     * @returns updated event - Observable<IEventManagementViewModel>
     */
    public updateEvent(updatedEvent: IEventManagementViewModel): Observable<IEventManagementViewModel> {
        let body = updatedEvent;
        return this._http.put(this._url + "UpdateEvent/", body, this.getRequestArgs(updatedEvent))
            .map((response: Response) => <IEventManagementViewModel>response.json())
            .catch(this.handleError);
    }

    /**
     * Deletes the event
     * @param id
     */
    public deleteEvent(id: number) {
        return this._http.delete(this._url + 'DeleteEvent/' + id);
    }

    /**
     * Gets Events Init Data
     * @param idOrganization
     */
    public getEventsInitData(idOrganization: number): Observable<PaginationInitViewModel> {
        return this._http.get(this._url + "GetEventsInitData/" + idOrganization)
            .map((response: Response) => <PaginationInitViewModel>response.json())
            .catch(this.handleError);
    }

    /**
     * Deletes the current image.
     * @param idImage
     */
    public deleteCurrentImage(idImage: number): Observable<any> {
        return this._http.delete(this._url + "DeleteCurrentImage/" + idImage)
            .catch(this.handleError);
    }
}