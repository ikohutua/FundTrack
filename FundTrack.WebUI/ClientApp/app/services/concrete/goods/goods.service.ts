import { Http, Response } from "@angular/http";
import { Observable } from 'rxjs/Observable';
import { Injectable } from "@angular/core";
import { Router } from "@angular/router";
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch';
import { RequestOptions, Request, RequestMethod } from '@angular/http';
import { Headers } from '@angular/http';
import { GoodsCategoryViewModel } from "../../../view-models/concrete/goods-category-view.model";
import { GoodsTypeViewModel } from "../../../view-models/concrete/goods-type-view.model";

@Injectable()
export class GoodsService {
    private _getCategoriesUrl: string = 'api/goods/allcategories';
    private _getTypesUrl: string = 'api/goods/alltypes';
    constructor(private _http: Http,
        private _router: Router) {
    }
    public getCategories(): Observable<GoodsCategoryViewModel[]> {
        return this._http.get(this._getCategoriesUrl)
            .map((response: Response) => <GoodsCategoryViewModel[]>response.json())
            .do(data => console.log('Item: ' + JSON.stringify(data)))
            .catch(this.handleError);
    }
    public getTypes(): Observable<GoodsTypeViewModel[]> {
        return this._http.get(this._getTypesUrl)
            .map((response: Response) => <GoodsTypeViewModel[]>response.json())
            .do(data => console.log('Item: ' + JSON.stringify(data)))
            .catch(this.handleError);
    }
    ///Error handler to report into console
    private handleError(error: Response) {
        console.error(error);
        return Observable.throw(error.json().error || 'Server error');
    }
}