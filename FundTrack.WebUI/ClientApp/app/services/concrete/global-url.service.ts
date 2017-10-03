import { Injectable, Inject, NgZone } from '@angular/core';

@Injectable()
    // urls to server for all components
export class GlobalUrlService {
    public static organizationDetailUrl: string = "api/OrganizationDetail/";

    //organization account
    public static getExtractStatus: string = "api/OrgAccount/ExtractStatus/";
    public static getExtractCredentials: string = "api/OrgAccount/ExtractCredentials/";
    public static connectExtracts: string = "api/OrgAccount/ConnectExtracts";
}