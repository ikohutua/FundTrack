import { Http, Response, RequestOptionsArgs } from "@angular/http";
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
import { GoodsTypeViewModel } from "../../../view-models/concrete/goodsType-view.model";

@Injectable()
export class UserOfferService{
    private _getOfferUrl: string = 'api/offer/get';
    private _deleteOfferUrl: string = 'api/offer/delete';
    private _createOfferUrl: string = 'api/offer/create';
    private _getOfferById: string = 'api/offer/GetUserOfferById';
    private _editOfferUrl: string = 'api/offer/edit';
    private _goodsTypeUrl: string = "api/requestedItem/GetGoodsType";
    constructor(private _http: Http,
                private _router: Router){
    }
    /**
     * Creates RequestOptionsArgs
     * @param body:T
     * @returns interface RequestOptionsArgs
     */
    

    public getUserOffers(userId: number): Observable<OfferViewModel[]> {
        return this._http.get(this._getOfferUrl + '/' + userId)
            .map((response: Response) => <OfferViewModel[]>response.json())
            .do(data => console.log('Item: ' + JSON.stringify(data)))
            .catch(this.handleError);
    }
    public createOffer(newOfferItem: OfferViewModel): Observable<OfferViewModel> {
        let body = newOfferItem;
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        return this._http.post(this._createOfferUrl, body, options)
            .map((response: Response) => <OfferViewModel>response.json())
            .do(data => console.log('ALL ' + JSON.stringify(data)))
            .catch(this.handleError);
    }
    public deleteOffer(offerId: number) {
        return this._http.delete(this._deleteOfferUrl + '/' + offerId)
            .catch(this.handleError);
    }
    public getUserOfferById(id: number): Observable<OfferViewModel> {
        return this._http.get(this._getOfferById + '/' + id)
            .map((response: Response) => <OfferViewModel>response.json())
            .catch(this.handleError);
    }
    ///Error handler to report into console
    private handleError(error: Response) {
        console.error(error);
        return Observable.throw(error.json().error || 'Server error');
    }
    public editOffer(offerItem: OfferViewModel): Observable<OfferViewModel> {
        let body = offerItem;
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        return this._http.put(this._editOfferUrl, body, options)
            .map((response: Response) => <OfferViewModel>response.json())
            .catch(this.handleError);
    }
    /**
     * Gets all goods types
     */
    public getAllGoodsTypes(): Observable<GoodsTypeViewModel[]> {
        return this._http.get(this._goodsTypeUrl)
            .map((response: Response) => <GoodsTypeViewModel[]>response.json())
            .catch(this.handleError);
    }
}