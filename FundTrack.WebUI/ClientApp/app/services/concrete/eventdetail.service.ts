import { Injectable } from "@angular/core";
import { Http, Response, Headers } from "@angular/http";
import { BaseService } from "../abstract/base-service";
import { IEventDetailModel } from "../../view-models/abstract/eventdetail-model.interface";
import { Observable } from "rxjs/Observable";
import "rxjs/add/operator/map";
import 'rxjs/add/operator/do';
import "rxjs/add/operator/catch";

@Injectable()
export class EventDetailService extends BaseService<IEventDetailModel>{
    /**
 * @constructor
 * @param http
 */
    constructor(private http: Http) {
        super(http);
    }

    public getById(id: number, additionString: string): Observable<IEventDetailModel> {
        return this.http.get(additionString + '/' + id.toString())
            .map((response: Response) => <IEventDetailModel>response.json())
            .catch(this.handleErrorHere);
    }

    private handleErrorHere(error: Response) {
        return Observable.throw(error.json().error || 'Server error');
    }
}
