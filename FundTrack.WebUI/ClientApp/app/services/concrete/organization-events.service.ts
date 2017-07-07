import { Injectable } from "@angular/core";
import { Http, Response, Headers, RequestOptionsArgs } from "@angular/http";
import { BaseService } from "../abstract/base-service";
import { IEventModel } from "../../view-models/abstract/event-model.interface";
import { EventModel } from "../../view-models/concrete/event-model";
import { EventInitViewModel } from '../../view-models/abstract/event-initpaginationdata-view-model';

import 'rxjs/add/operator/map';
import 'rxjs/add/operator/do';

import { Observable } from "rxjs/Observable";

@Injectable()
export class OrganizationEventService extends BaseService<IEventModel>{
    /**
 * @constructor
 * @param http
 */
    constructor(private http: Http) {
        super(http);
    }

    // gets initial pagination data from server
    public getInitData(url) {
        return this.http.get(url)
            .map((response: Response) => response.json() as EventInitViewModel)
    }

    // gets items to display on page from server
    public getItemsOnScroll(additionString: string, itemsPerPage: number, currentPage: number): Observable<IEventModel[]> {
        return this.http.get(additionString + '/' + itemsPerPage + '/' + currentPage)
            .map((response: Response) => <IEventModel[]>response.json())
            .catch(this.handleErrorHere);
    }

    public getCollectionById(id: number, additionString: string): Observable<IEventModel[]> {
        return this.http.get(additionString + '/' + id.toString())
            .map((response: Response) => <IEventModel[]>response.json())
            //.do(data => console.log('ALL ' + JSON.stringify(data)))
            .catch(this.handleErrorHere);
    }

    private handleErrorHere(error: Response) {
        return Observable.throw(error.json().error || 'Server error');
    }
}



    //public getCollection(id?: number, additionString?: string): Observable<T[]> {
    //    if (id && additionString) {
    //        return this._http.get(additionString + '/' + id.toString())
    //            .map((response: Response) => <T[]>response.json())
    //            .catch(this.handleError);
    //    }
    //    if(id){
    //        return this._http.get(this._url + '/' + id.toString())
    //            .map((response: Response) => <T[]>response.json())
    //            .catch(this.handleError);
    //    }
    //     if (additionString) {
    //        return this._http.get(this._url + additionString)
    //            .map((response: Response) => <T[]>response.json())
    //            .do(data => console.log('ALL ' + JSON.stringify(data)))
    //            .catch(this.handleError);
    //    }
    //        return this._http.get(this._url)
    //            .map((response: Response) => <T[]>response.json())
    //            .do(data => console.log('ALL ' + JSON.stringify(data)))

    //            .catch(this.handleError);
    //}
