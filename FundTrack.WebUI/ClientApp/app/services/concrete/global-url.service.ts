import { Injectable, Inject, NgZone } from '@angular/core';

@Injectable()
export class GlobalUrlService {
     // urls to server for all components
    public static getAllOrganizationsUrl: string = "api/OrganizationDetail";
    public static getOrganizationDetailUrl: string = "api/OrganizationDetail/OrganinzationDetailByOrgId/";
}