import { Injectable } from "@angular/core";
import { Http, Response, Headers, RequestOptionsArgs, RequestOptions } from "@angular/http";
import { Observable } from "rxjs/Observable";
import { OrganizationGeneralViewModel } from "../../../view-models/concrete/organization-general-view.model";
import { GlobalUrlService } from "../global-url.service";
import { RequestOptionsService } from "../request-options.service";
import { OrganizationDetailViewModel } from "../../../view-models/concrete/organization-detail-view.model";


@Injectable()
export class OrganizationsDetailsService {

    /**
   * Creates new instance of OrganizationsDetailsService
   * @param _http
   */
    constructor(private _http: Http) {
    }

    public getAllOrganizations(): Observable<OrganizationGeneralViewModel[]> {
        return this._http.get(GlobalUrlService.getAllOrganizationsUrl, RequestOptionsService.getRequestOptions())
            .map((res: Response) => <OrganizationGeneralViewModel[]>res.json());
    }

    public getOrganizationDetail(orgId: number): Observable<OrganizationDetailViewModel> {
        return this._http.get(GlobalUrlService.getAllOrganizationsUrl + orgId, RequestOptionsService.getRequestOptions())
            .map((res: Response) => <OrganizationDetailViewModel>res.json());
}
   
}