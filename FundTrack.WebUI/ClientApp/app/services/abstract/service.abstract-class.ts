import { Http, Response, Headers } from "@angular/http";
import { Observable } from "rxjs/Observable";
import { IOrganizationForLayout } from "../../view-models/abstract/organization-for-layout.interface";
import "rxjs/add/operator/map";
import "rxjs/add/operator/catch";

/**
 * abstract generic class for services
 */
export class Service<T> {
    /**
     * @constructor
     * @param _url: string
     * @param _http
     */
    constructor(private _http: Http, private _url: string) { }

    /**
    * exception handler
    * @param error: Response
    */
    private handleError(error: Response) {
        return Observable.throw(error.json().error || 'Server error');
    }

    /**
     * gets the collection of view models
     * @returns Observable<T[]>
     */
    public getCollection(): Observable<T[]> {
        return this._http.get(this._url)
            .map((response: Response) => <T[]>response.json())
            .catch(this.handleError);
    }

    /**
     * create new item
     * @param newItem: T
     * @returns Observable<T>
     */
    public create(newItem: T): Observable<T> {
        let body = newItem;
        let headers = new Headers({ 'ContentType': 'application/json' });
        return this._http.post(this._url, body, { headers: headers })
            .map((response: Response) => <T>response.json());
    }

    /**
     * update item 
     * @param updatedItem: T
     * @returns Observable<Response>
     */
    public update(updatedItem: T): Observable<Response> {
        let body = updatedItem;
        let headers = new Headers({ 'ContentType': 'application/json' });
        return this._http.put(this._url, body, { headers: headers });
    }

    /**
     * delete one item by id
     * @param id
     * @returns Observable<Response>
     */
    public delete(id: number): Observable<Response> {
        return this._http.delete(this._url + '/' + id);
    }
}

