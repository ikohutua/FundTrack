import { Injectable, Inject, NgZone } from '@angular/core';

@Injectable()
export class GlobalUrlService {
    // bank imports
    public static getAllSuggestedBankImportUrl: string = "api/BankImport/SuggestedImports";

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

    //donation URLs
    public static userDonations: string = "api/Donate/User/";
    public static userDonationsByDate: string = "api/Donate/UserByDate/";

    //FinOp URLs
    public static processMultipleFinOps: string = "api/FinOp/ProcessMultiple/";
    public static createFinOp: string = "api/FinOp/CreateFinOp/";
    public static readonly banksUrl: string = "api/Bank";


    //BankImport URLs

    public static readonly RegisterNewExtracts: string = 'api/BankImport/RegisterNewExtracts';
    public static readonly PrivatExtract: string = 'api/BankImport/ImportPrivat';
    public static readonly PrivatExtractWithDate: string = 'api/BankImport/Privat';
    public static readonly UpdateDate: string = 'api/BankImport/UpdateDate';
    public static readonly LastUpdate: string = 'api/BankImport/LastUpdate';

    // donate service
    public static readonly getSuggestedDonations = "api/Donate/suggested/";

    // finOp service
    public static readonly bindDonationAndFinOp = "api/FinOp/bindDonationAndFinOp";
    public static readonly getFinOpsUrl: string = 'api/finop/getFinOpsByOrgAccId';
    public static readonly getTargetsUrl: string = "api/finop/GetTargets";
    public static readonly incomeUrl: string = "api/finop/income";
    public static readonly spendingUrl: string = "api/finop/spending";
    public static readonly transferUrl: string = "api/finop/transfer";
    public static readonly editUrl: string = "api/finop";
    public static readonly getOrgAccForFinOpsUrl: string = 'api/OrgAccount/GetOrgAccountForFinOp';
    public static readonly createUrl: string = 'api/FinOp/CreateFinOp';
    public static readonly getFinOpUrl: string = 'api/finop/getFinOpsById';
    public static readonly getFinOpUrlForPage: string = 'api/finop/getFinOpsByIdForPage';
    public static readonly getFinOpInitData: string = 'api/finop/getFinOpInitData';

    // user service
    public static readonly getAllUsers = "api/User";

    // organizationStatistics service
    public static readonly getReportForFinopsByTargets = 'api/Reports/';
    public static readonly getSubTargets = 'api/reports/GetSubTargets/';
    public static readonly getFinOpsByTargetId = 'api/reports/GetFinOpsByTargetId/';

    //ShowRequestedItemService
    public static readonly countOfUsersDonationsReportItems = "api/reports/CountOfUsersDonationsReport";
    public static readonly usersDonationsPaginatedReport = 'api/reports/UsersDonationsPaginatedReport';

    public static readonly countOfCommonUsersDonationsReportItems = "api/reports/CountOfCommonUsersDonationsReport";
    public static readonly commonUsersDonationsPaginatedReport = 'api/reports/CommonUsersDonationsPaginatedReport';
    public static readonly donationsvalueReportPerDay = 'api/reports/DonationsValueReportPerDay';
}