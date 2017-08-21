import { Injectable } from "@angular/core";
import { BaseService } from "../abstract/base-service";
import { Http, Response, Headers, RequestOptions } from '@angular/http';
import { Observable } from 'rxjs/Observable';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/operator/do';
import { IOrganizationRegistrationViewModel } from '../../view-models/abstract/organization-registration-view-model.interface';
import { OrganizationRegistrationViewModel } from '../../view-models/concrete/organization-registration-view-model';
import { BaseSpinnerService } from "../abstract/base-spinner-service";
import { SpinnerComponent } from "../../shared/components/spinner/spinner.component";

/**
 * Service to register new organization
 */
@Injectable()
export class OrganizationRegistrationService extends BaseSpinnerService<OrganizationRegistrationViewModel> {
    public constructor(private _http: Http) {
        super(_http);
    }

    /**
     * register organization with http post method
     * @param organization to create
     */
    registerOrganization(organization: OrganizationRegistrationViewModel, spinner?: SpinnerComponent): Observable<OrganizationRegistrationViewModel> {
        //let headers = new Headers({ 'Content-Type': 'application/json' });
        //let options = new RequestOptions({ headers: headers });
        //let body = organization;
        let url = 'api/OrganizationRegistration/RegisterNewOrganization/';
        return super.create(url, organization, this.getRequestOptions());
        //return this.http.post('api/OrganizationRegistration/RegisterNewOrganization/', body, options).map((response: Response) => <OrganizationRegistrationViewModel>response.json());
    }

    private getRequestOptions() {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        return options;
    }
}

