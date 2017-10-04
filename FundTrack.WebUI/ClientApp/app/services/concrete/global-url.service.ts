import { Injectable, Inject, NgZone } from '@angular/core';

@Injectable()
    // urls to server for all components
export class GlobalUrlService {
    // urls to server for all components
    public static getAllOrganizationsUrl: string = "api/OrganizationDetail";
    public static getOrganizationDetailUrl: string = "api/OrganizationDetail/OrganinzationDetailByOrgId/";

    //organization account
    public static getExtractStatus: string = "api/OrgAccount/ExtractStatus/";
    public static getExtractCredentials: string = "api/OrgAccount/ExtractCredentials/";
    public static connectExtracts: string = "api/OrgAccount/ConnectExtracts";

    //organization profile
    public static editLogo: string = "api/organizationProfile/EditLogo";

    //register organization
    public static registerOrganization = "api/OrganizationRegistration/RegisterNewOrganization/";
    public static readonly banksUrl: string = "api/Bank";
}