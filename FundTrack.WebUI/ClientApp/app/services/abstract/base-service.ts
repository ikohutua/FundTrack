import { Http, Response, Headers, RequestOptionsArgs } from "@angular/http";
import { Observable } from "rxjs/Observable";
import "rxjs/add/operator/map";
import 'rxjs/add/operator/do';
import "rxjs/add/operator/catch";

/**
 * Abstract generic class for services
 */
export abstract class BaseService<T> {
    /**
     * @constructor
     * @param _url: string
     * @param _http
     */
    public constructor(private _http: Http, private _url: string) { }

    /**
     * Gets the collection of view models
     * @returns Observable<T[]>
     */
    public getCollection(): Observable<T[]> {
        return this._http.get(this._url)
            .map((response: Response) => <T[]>response.json())
            //.do(data => console.log('ALL ' + JSON.stringify(data)))
            .catch(this.handleError);
    }

    /**
    * Gets one item of model
    * @returns Observable<T>
    */
    public getOne(additionString?: string): Observable<T> {
        return this._http.get(this._url + additionString)
            .map((response: Response) => <T>response.json())
            //.do(data => console.log('ALL ' + JSON.stringify(data)))
            .catch(this.handleError);
    }

    /**
     * Create new item
     * @param newItem: T
     * @returns Observable<T>
     */
    public create(newItem: T): Observable<T> {
        return this._http.post(this._url, this.getRequestArgs(newItem))
            .map((response: Response) => <T>response.json())
            .catch(this.handleError);
    }

    /**
     * Update item 
     * @param updatedItem: T
     * @returns Observable<Response>
     */
    public update(updatedItem: T): Observable<T> {
        return this._http.put(this._url, this.getRequestArgs(updatedItem))
            .map((response: Response) => <T>response.json())
            .catch(this.handleError);
    }

    /**
     * Delete one item by id
     * @param id
     * @returns Observable<Response>
     */
    public delete(id: number): Observable<Response> {
        return this._http.delete(this._url + '/' + id)
            .catch(this.handleError);
    }

    /**
    * Exception handler
    * @param error: Response
    */
    private handleError(error: Response) {
        return Observable.throw(error.json().error || 'Server error');
    }

    /**
     * Creates RequestOptionsArgs
     * @param body:T
     * @returns interface RequestOptionsArgs
     */
    private getRequestArgs(body: T): RequestOptionsArgs {
        let headers = new Headers({ 'ContentType': 'application/json' });
        return { headers: headers, body: body };
    }
}

