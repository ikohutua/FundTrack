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
import { RequestOptionsService } from "../request-options.service";

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
        return this._http.put(GlobalUrlService.organizationEditDescription, body, RequestOptionsService.getRequestOptions())
            .map(response => response.json() as OrganizationGeneralViewModel);
    }

    getAddress(id: number): Observable<OrgAddressViewModel> {
        return this._http.get(GlobalUrlService.organizationProfileAddress + id.toString()).
            map((response: Response) => response.json() as OrgAddressViewModel);
    }

    getModerators(id: number): Observable<ModeratorViewModel[]> {
        return this._http.get(GlobalUrlService.getModerators + id.toString()).
            map((response: Response) => response.json() as ModeratorViewModel[]);
    }

    addModerator(moderator: AddModeratorViewModel): Observable<ModeratorViewModel> {
        let body = moderator;
        return this._http.post(GlobalUrlService.addModerator, body, RequestOptionsService.getRequestOptions()).
            map((response: Response) => response.json() as ModeratorViewModel);
    }

    deactivateModerator(login: string) {
        return this._http.delete(GlobalUrlService.deactivateModerator + login);
    }

    getAvailableUsers(id: number): Observable<AddModeratorViewModel[]> {
        return this._http.get(GlobalUrlService.getAvailableUsers + id.toString()).
            map((response: Response) => response.json() as AddModeratorViewModel[]);
    }

    addAddresses(addresses: OrgAddressViewModel): Observable<OrgAddressViewModel> {

        let body = addresses;
        return this._http.post(GlobalUrlService.organizationProfileAddresses, body, RequestOptionsService.getRequestOptions())
            .map((response: Response) => response.json() as OrgAddressViewModel);
    }

    deleteAddress(id: number) {
        return this._http.delete(GlobalUrlService.organizationProfileAddress + id.toString());
    }

    editAddress(address: AddressViewModel): Observable<OrgAddressViewModel> {
        let body = address;
        return this._http.put(GlobalUrlService.organizationProfileAddress, body, RequestOptionsService.getRequestOptions())
            .map((response: Response) => response.json() as OrgAddressViewModel);
    }

    addTarget(target: TargetViewModel): Observable<TargetViewModel> {
        let body = target;
        return this._http.post(GlobalUrlService.addTarget, body, RequestOptionsService.getRequestOptions()).
            map((response: Response) => response.json() as TargetViewModel);
    }

    getTargetsByOrganizationId(organizationId: number): Observable<TargetViewModel[]> {
        return this._http.get(GlobalUrlService.getAllTargetsOfOrganization + organizationId).
            map((response: Response) => response.json() as TargetViewModel[]);
    }

    getTargetsWithDeletableField(orgId: number): Observable<TargetViewModel[]> {
        return this._http.get(GlobalUrlService.getTargetsWithDeletableField + orgId, RequestOptionsService.getRequestOptions()).
            map((response: Response) => response.json() as TargetViewModel[]);
    }

    editTarget(target: TargetViewModel): Observable<TargetViewModel> {
        let body = target;
        return this._http.put(GlobalUrlService.editTarget, body, RequestOptionsService.getRequestOptions()).
            map((response: Response) => response.json() as TargetViewModel);
    }

    deleteTarget(targetId: number) {
        return this._http.delete(GlobalUrlService.deleteTarget + targetId).map((response: Response) => response.json());
    }

    editLogo(item: EditLogoViewModel): Observable<EditLogoViewModel> {
        let body = item;
        return this._http.put(GlobalUrlService.editLogo, body, RequestOptionsService.getRequestOptions())
            .map((response: Response) => response.json() as EditLogoViewModel);
    }
}
