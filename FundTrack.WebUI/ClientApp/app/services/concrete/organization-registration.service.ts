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
import { GlobalUrlService } from "../../services/concrete/global-url.service";
import { RequestOptionsService } from "../../services/concrete/request-options.service";

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
        return super.create(GlobalUrlService.registerOrganization, organization, RequestOptionsService.getRequestOptions());
    }
}

