import { Injectable } from '@angular/core';
import { Http } from '@angular/http';
import { RegistrationViewModel } from './registration-view.model';

@Injectable()
export class RegistrationService
{
    constructor(private _http: Http) { }

    public CreateUser(registrationViewModel: RegistrationViewModel)
    {
        let body = registrationViewModel;
        return this._http.post('api/user', body);
    }
}
