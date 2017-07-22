import { Injectable } from "@angular/core";
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/do'
import { OrganizationGeneralViewModel } from "../../../view-models/concrete/organization-general-view.model";
import { OrgAddressViewModel } from "../../../view-models/concrete/edit-organization/org-address-view.model";
import { ModeratorViewModel } from '../../../view-models/concrete/edit-organization/moderator-view.model';
import { AddModeratorViewModel } from "../../../view-models/concrete/edit-organization/add-moderator-view.model";
import { AddressViewModel } from "../../../view-models/concrete/edit-organization/address-view.model";

@Injectable()
export class EditOrganizationService{
    
    /**
 * @constructor
 * @param http
 */
    constructor(private _http: Http) {
        
    }

    editDescription(organization: OrganizationGeneralViewModel): Observable<OrganizationGeneralViewModel>
    {
        let body = organization;
        let headers = new Headers({ 'ContentType': 'application/json' });
        return this._http.put('api/OrganizationProfile/EditDescription', body, { headers: headers })
            .map(response => response.json() as OrganizationGeneralViewModel);
    }

    getAddress(id: number): Observable<OrgAddressViewModel>
    {
        return this._http.get('api/OrganizationProfile/GetAddress/'+id.toString()).
            map((response:Response) => response.json() as OrgAddressViewModel);
    }

    getModerators(id: number): Observable<ModeratorViewModel[]>
    {
        return this._http.get('api/Moderator/GetModerators/' + id.toString()).
            map((response: Response) => response.json() as ModeratorViewModel[]);
    }

    addModerator(moderator: AddModeratorViewModel): Observable<ModeratorViewModel>
    {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        let body = moderator;
        return this._http.post('api/Moderator/AddModerator/', body, options).
            map((response: Response) => response.json() as ModeratorViewModel);
    }

    deactivateModerator(login: string) {
        return this._http.delete('api/Moderator/DeactivateModerator/' + login);
    }

    getAvailableUsers(id: number): Observable<AddModeratorViewModel[]>{
        return this._http.get('api/Moderator/GetAvailableUsers/' + id.toString()).
            map((response: Response) => response.json() as AddModeratorViewModel[]);
    }

    addAddresses(addresses: OrgAddressViewModel): Observable<OrgAddressViewModel> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        let body = addresses;
        return this._http.post('api/OrganizationProfile/AddAddresses', body, options)
            .map((response: Response) => response.json() as OrgAddressViewModel);
    }

    deleteAddress(id: number)
    {
        return this._http.delete('api/OrganizationProfile/DeleteAddress/' + id.toString());
    }

    editAddress(address: AddressViewModel): Observable<OrgAddressViewModel> {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        let body = address;
        return this._http.put('api/organizationProfile/EditAddress', body, options)
            .map((response: Response) => response.json() as OrgAddressViewModel);
    }
}
