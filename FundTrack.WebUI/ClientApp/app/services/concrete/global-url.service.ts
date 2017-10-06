import { Injectable, Inject, NgZone } from '@angular/core';

@Injectable()
export class GlobalUrlService {
    // urls to server for all components
    public static getAllOrganizationsUrl: string = "api/OrganizationDetail/";
    public static getFixingBalanceUrl: string = "api/FixingBalance/";

    //organization account Extracts
    public static getExtractStatus: string = "api/OrgAccount/ExtractStatus/";
    public static getExtractCredentials: string = "api/OrgAccount/ExtractCredentials/";
    public static connectExtracts: string = "api/OrgAccount/ConnectExtracts";
    public static toggleExtracts: string = "api/OrgAccount/ToggleExtractsFunction";
    public static disableExtracts: string = "api/OrgAccount/DisableExtractsFunction";
    public static checkExtractsFunction: string = "api/OrgAccount/CheckExtractsFunction/";

    //organization profile
    public static editLogo: string = "api/organizationProfile/Logo/";
    public static organizationProfileAddresses: string = "api/organizationProfile/Addresses/";
    public static organizationProfileAddress: string = "api/organizationProfile/Address/";
    public static organizationEditDescription: string = "api/OrganizationProfile/EditDescription";
    
    //register organization
    public static registerOrganization = "api/OrganizationRegistration/RegisterNewOrganization/";
    public static readonly banksUrl: string = "api/Bank/";

    //target urls
    public static readonly editTarget = "api/Target/EditTarget/";
    public static readonly deleteTarget = "api/Target/DeleteTarget/";
    public static readonly getTargetsWithDeletableField = "api/Target/withDeletable/";
    public static readonly getAllTargetsOfOrganization = "api/Target/GetAllTargetsOfOrganization/";
    public static readonly addTarget = "api/Target/CreateTarget/";

    //moderator urls
    public static readonly getAvailableUsers = "api/Moderator/GetAvailableUsers/";
    public static readonly deactivateModerator = "api/Moderator/DeactivateModerator/";
    public static readonly addModerator = "api/Moderator/AddModerator";
    public static readonly getModerators = "api/Moderator/GetModerators/";
}