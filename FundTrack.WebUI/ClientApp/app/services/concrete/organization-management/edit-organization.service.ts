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
import { TargetViewModel } from "../../../view-models/concrete/finance/donate/target.view-model";
import { GlobalUrlService } from "../global-url.service";
import { EditLogoViewModel } from "../../../view-models/concrete/edit-organization/edit-org-logo-view.model";

@Injectable()
export class EditOrganizationService {

    /**
 * @constructor
 * @param http
 */
    constructor(private _http: Http) {

    }

    editDescription(organization: OrganizationGeneralViewModel): Observable<OrganizationGeneralViewModel> {
        let body = organization;
        let headers = new Headers({ 'ContentType': 'application/json' });
        return this._http.put('api/OrganizationProfile/EditDescription', body, { headers: headers })
            .map(response => response.json() as OrganizationGeneralViewModel);
    }

    getAddress(id: number): Observable<OrgAddressViewModel> {
        return this._http.get('api/OrganizationProfile/GetAddress/' + id.toString()).
            map((response: Response) => response.json() as OrgAddressViewModel);
    }

    getModerators(id: number): Observable<ModeratorViewModel[]> {
        return this._http.get('api/Moderator/GetModerators/' + id.toString()).
            map((response: Response) => response.json() as ModeratorViewModel[]);
    }

    addModerator(moderator: AddModeratorViewModel): Observable<ModeratorViewModel> {
        let body = moderator;
        return this._http.post('api/Moderator/AddModerator/', body, this.getOptionsForRequest()).
            map((response: Response) => response.json() as ModeratorViewModel);
    }

    deactivateModerator(login: string) {
        return this._http.delete('api/Moderator/DeactivateModerator/' + login);
    }

    getAvailableUsers(id: number): Observable<AddModeratorViewModel[]> {
        return this._http.get('api/Moderator/GetAvailableUsers/' + id.toString()).
            map((response: Response) => response.json() as AddModeratorViewModel[]);
    }

    addAddresses(addresses: OrgAddressViewModel): Observable<OrgAddressViewModel> {

        let body = addresses;
        return this._http.post('api/OrganizationProfile/AddAddresses', body, this.getOptionsForRequest())
            .map((response: Response) => response.json() as OrgAddressViewModel);
    }

    deleteAddress(id: number) {
        return this._http.delete('api/OrganizationProfile/DeleteAddress/' + id.toString());
    }

    editAddress(address: AddressViewModel): Observable<OrgAddressViewModel> {
        let body = address;
        return this._http.put('api/organizationProfile/EditAddress', body, this.getOptionsForRequest())
            .map((response: Response) => response.json() as OrgAddressViewModel);
    }

    addTarget(target: TargetViewModel): Observable<TargetViewModel> {
        let body = target;
        return this._http.post('api/Target/CreateTarget/', body, this.getOptionsForRequest()).
            map((response: Response) => response.json() as TargetViewModel);
    }

    getOptionsForRequest(): RequestOptions {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        return new RequestOptions({ headers: headers });
    }

    getTargetsByOrganizationId(organizationId: number): Observable<TargetViewModel[]> {
        return this._http.get('api/Target/GetAllTargetsOfOrganization/' + organizationId).
            map((response: Response) => response.json() as TargetViewModel[]);
    }

    getTargetsWithDeletableField(orgId: number): Observable<TargetViewModel[]> {
        return this._http.get('api/Target/withDeletable/' + orgId, this.getOptionsForRequest()).
            map((response: Response) => response.json() as TargetViewModel[]);
    }

    editTarget(target: TargetViewModel): Observable<TargetViewModel> {
        let body = target;
        return this._http.put('api/Target/EditTarget/', body, this.getOptionsForRequest()).
            map((response: Response) => response.json() as TargetViewModel);
    }

    deleteTarget(targetId: number) {
        return this._http.delete('api/Target/DeleteTarget/' + targetId);
    }

    editLogo(item: EditLogoViewModel): Observable<EditLogoViewModel> {
        let body = item;
        return this._http.put(GlobalUrlService.editLogo, body, this.getOptionsForRequest())
            .map((response: Response) => response.json() as EditLogoViewModel);
    }
}
