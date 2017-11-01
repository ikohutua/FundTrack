import { Injectable, Inject, NgZone } from '@angular/core';

@Injectable()
export class ReportUrlsService {
    //donate page
    public static _urlForPagination: string = 'api/RequestedItem/GetRequestedItemPaginationData';
    public static _urlGetRequestedItemToShowPerPage: string = 'api/RequestedItem/GetRequestedItemToShowPerPage';
    public static _urlGetOrganizations: string = 'api/RequestedItem/GetOrganizations';
    public static _urlGetCategories: string = 'api/RequestedItem/GetCategories';
    public static _urlGetTypes: string = 'api/RequestedItem/GetTypes';
    public static _urlGetStatuses: string = 'api/RequestedItem/GetStatuses';
    public static _urlFilterRequestedItem: string = 'api/RequestedItem/GetFilterRequestedItemPaginationData';
    public static _urlGetIncomeReportData: string = 'api/reports/IncomeReport';
    public static _urlGetOutcomeReportData: string = 'api/reports/OutcomeReport';
    public static _urlGetFinOpImagesById: string = 'api/reports/FinOpImages';
    public static _urlGetInvoiceDeclaration: string = 'api/reports/InvoiceDeclaration';
    public static _urlGetRequestedItemToShow: string = 'api/RequestedItem/GetRequestedItemToShow';
}