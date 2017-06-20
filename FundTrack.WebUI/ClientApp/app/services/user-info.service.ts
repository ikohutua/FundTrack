import { Injectable } from '@angular/core';
import { Http, Response } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/do';
import 'rxjs/add/operator/map';
import { UserInfo } from '../view-models/concrete/user-info.model';


@Injectable()
    //Service to get user data
export class UserInfoService {
    private _url: string = 'api/userinfo';
    constructor(private _http: Http) {
    }
    //Returns userInfo view model
    public getCurrentUser() {
        return this._http.get(this._url)
            .map((response: Response) => <UserInfo>response.json())
            .do(data => console.log('Input object: ' + JSON.stringify(data)))
            .catch(this.HandleError);
    }
    private HandleError(error: Response) {
        console.error(error);
        return Observable.throw(error.json().error || 'Server error');
    }
}
