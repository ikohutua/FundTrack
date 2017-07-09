import { Http, Response } from "@angular/http";
import { Observable } from 'rxjs/Observable';
import { IOfferViewModel } from '../../../view-models/abstract/offer-model.interface';
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch';
import { RequestOptions, Request, RequestMethod } from '@angular/http';
import { Headers } from '@angular/http';
import { OfferViewModel } from "../../../view-models/concrete/offer-view.model";

@Injectable()
export class UserOfferService{
    private _getOfferUrl: string = 'api/offer/get';
    private _deleteOfferUrl: string = 'api/offer/delete';
    constructor(private _http: Http,
                private _router: Router){
    }
    public getUserOffers(userId: number): Observable<OfferViewModel[]> {
        return this._http.get(this._getOfferUrl + '/' + userId)
            .map((response: Response) => <OfferViewModel[]>response.json())
            .do(data => console.log('Item: ' + JSON.stringify(data)))
            .catch(this.handleError);
    }
    public deleteOffer(offerId: number): Observable<IOfferViewModel[]> {
        return this._http.delete(this._deleteOfferUrl + '/' + offerId)
            .catch(this.handleError)
    }
    ///Error handler to report into console
    private handleError(error: Response) {
        console.error(error);
        return Observable.throw(error.json().error || 'Server error');
    }
}