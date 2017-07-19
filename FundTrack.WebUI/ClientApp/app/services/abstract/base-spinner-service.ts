import { Http, Response, Headers, RequestOptionsArgs, RequestOptions } from "@angular/http";
import { Observable } from "rxjs/Observable";
import "rxjs/add/operator/map";
import 'rxjs/add/operator/do';
import "rxjs/add/operator/catch";
import { SpinnerComponent } from "../../shared/components/spinner/spinner.component";

/**
 * Abstract generic class for services
 */
export abstract class BaseSpinnerService<T> {
    /**
     * @constructor
     * @param _url: string
     * @param _httpClient
     */
    public constructor(private _httpClient: Http) { }

    /**
     * Gets the collection of view models
     * @param
     * @returns Observable<T[]>
     */
    public getCollection(_url: string, requestOptions?: RequestOptions, spinner?: SpinnerComponent): Observable<T[]> {
        this.showSpinner(spinner);
        return this._httpClient.get(_url, requestOptions)
            .map((response: Response) => {
                this.hideSpinner(spinner);
                return <T[]>response.json();
            })
            .catch(this.handleErrorResponse);        
    }

    /**
     * Gets By Id
     * @param id
     * @param additionString
     */
    public getById(_url: string, id: number, requestOptions?: RequestOptions, spinner?: SpinnerComponent): Observable<T> {
        this.showSpinner();
        return this._httpClient.get(_url + '/' + id.toString(), requestOptions)
            .map((response: Response) => {
                this.hideSpinner();
                return <T>response.json();
            })
            .catch(this.handleErrorResponse);
    }

    /**
     * Create new item
     * @param newItem: T
     * @returns Observable<T>
     */
    public create(_url: string, newItem: T, requestOptions?: RequestOptions): Observable<T> {
        return this._httpClient.post(_url, newItem, requestOptions)
            .map((response: Response) => <T>response.json())
            .catch(this.handleErrorResponse);
    }

    /**
     * Update item 
     * @param updatedItem: T
     * @returns Observable<Response>
     */
    public update(_url: string, updatedItem: T, requestOptions?: RequestOptions): Observable<T> {
        return this._httpClient.put(_url, updatedItem, requestOptions)
            .map((response: Response) => <T>response.json())
            .catch(this.handleErrorResponse);
    }

    /**
     * Delete one item by id
     * @param id
     * @returns Observable<Response>
     */
    public delete(_url: string, id: number, requestOptions?: RequestOptions): Observable<Response> {
        return this._httpClient.delete(_url + '/' + id, requestOptions)
            .catch(this.handleErrorResponse);
    }

    private showSpinner(spinner?: SpinnerComponent) {
        if (spinner) {
            spinner.show();
        }
    }

    private hideSpinner(spinner?: SpinnerComponent) {
        if (spinner) {
            spinner.hide();
        }
    }
    /**
    * Exception handler
    * @param error: Response
    */
    private handleErrorResponse(error: Response) {
        return Observable.throw(error.json().error || 'Server error');
    }
}

