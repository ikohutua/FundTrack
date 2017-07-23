import { Injectable, Inject } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Headers, RequestOptions } from '@angular/http';
import { RequestedItemDetailViewModel } from '../../view-models/concrete/requested-item-detail-view.model';
import { UserResponseViewModel } from '../../view-models/concrete/user-response-view.model';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch';
import { BaseSpinnerService } from "../abstract/base-spinner-service";
import { SpinnerComponent } from "../../shared/components/spinner/spinner.component";

@Injectable()
export class RequestDetailService {

    public constructor(private _http: Http) {
    }

    /**
     * Send request to controller return request whuch have this id
     * @param id
     */
    public getRequestDetail(id: number, spinner?: SpinnerComponent): Observable<RequestedItemDetailViewModel> {
        let requestDetailUrl = 'api/RequestedItem/GetRequestDetail';
        return this._http.get(requestDetailUrl + '/' + id.toString())
            .map((response: Response) => <RequestedItemDetailViewModel>response.json())
            .catch(this.handleError);
    }

    /**
     * Send request to controller to create new user in response on the request
     * @param userResponse
     */
    public setUserResponse(userResponse: UserResponseViewModel): Observable<UserResponseViewModel> {
        let userResponseUrl = 'api/RequestedItem/SetUserResponse'
        return this._http.post(userResponseUrl, JSON.stringify(userResponse), this.getRequestOptions())
            .map((response: Response) => <UserResponseViewModel>response.json())
            .catch(this.handleError);
    }

    /**
    * Create RequestOptions
    */
    private getRequestOptions() {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        return options;
    }

    /**
    * Catch error
    * @param error
    */
    private handleError(error: Response): any {
        return Observable.throw(error.json().error);
    }
}