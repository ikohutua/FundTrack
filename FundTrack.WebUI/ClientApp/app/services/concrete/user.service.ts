import { RegistrationViewModel } from '../../view-models/concrete/registration-view.model';
import { Service } from "../abstract/service.abstract-class";
import { AuthorizationType } from '../../view-models/concrete/authorization.type';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import { Headers, RequestOptions } from '@angular/http';
import { Injectable, Inject } from '@angular/core';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/catch'

@Injectable()
export class UserService  {
    constructor(private _http: Http) { }

    public create(newItem: RegistrationViewModel): Observable<AuthorizationType> {
        let body = newItem;
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        return this._http.post("api/user/register", body, options)
            .map((response: Response) => response.json() as AuthorizationType);
    }
}

