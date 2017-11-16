import { Injectable } from "@angular/core";
import { Http, Response, Headers, RequestOptions } from "@angular/http";
import { BaseService } from "../abstract/base-service";
import { IShowRequestedItem } from "../../view-models/abstract/showrequesteditem-model.interface";
import { Observable } from "rxjs/Observable";
import "rxjs/add/operator/map";
import 'rxjs/add/operator/do';
import "rxjs/add/operator/catch";
import { RequestedItemInitViewModel } from "../../view-models/abstract/requesteditem-initpaginationdata-view-model";
import { ShowRequestedItem } from "../../view-models/concrete/showrequesteditem-model.interface";
import { GoodsTypeShortViewModel } from "../../view-models/concrete/goods-type-view.model";
import { GoodsCategoryViewModel } from "../../view-models/concrete/goods-category-view.model";
import { GoodsStatusViewModel } from "../../view-models/concrete/goods-status-model";
import { IOrganizationForFiltering } from "../../view-models/abstract/organization-for-filtering.interface";
import { FilterRequstedViewModel } from '../../view-models/concrete/filter-requests-view.model';
import { IncomeReportDataViewModel } from "../../view-models/concrete/income-report-data-view-model";
import { OutcomeReportDataViewModel } from "../../view-models/concrete/outcome-report-data-view-model";
import { UsersDonationsReportDataViewModel } from "../../view-models/concrete/users-donations-view.model";
import { GlobalUrlService } from "./global-url.service";
import { RequestOptionsService } from "./request-options.service";
import { ReportFilterQueryViewModel } from "../../view-models/concrete/report-filter-query-view-model";
import { DatePipe } from "@angular/common";
import { TargetViewModel } from "../../view-models/concrete/finance/donate/target.view-model";
import { DataSetViewModel } from "../../view-models/concrete/data-set-view.model";
import { InvoiceDeclarationResponseViewModel } from "../../view-models/concrete/invoice-declaration-response-view-model";
import { ReportUrlsService } from "./report-urls.service";

@Injectable()
export class ShowRequestedItemService extends BaseService<IShowRequestedItem>{
      /**
 * @constructor
 * @param http
 */
    constructor(private http: Http, private dp: DatePipe) {
        super(http, 'api/RequestedItem/GetRequestedItemToShow');
    }

    /**
     * Gets initial pagination data about organizations
     */
    public getRequestedItemInitData() {
        return this.http.get(ReportUrlsService._urlForPagination)
            .map((response: Response) => response.json() as RequestedItemInitViewModel)
    }

    /**
     * get Requested Item on page by pagination
     * @param itemsPerPage
     * @param currentPage
     * @param filters
     */
    public getRequestedItemOnPage(itemsPerPage: number, currentPage: number, filters: FilterRequstedViewModel) {

        let body = {
            "filterOptions": filters,
            "currentPage": currentPage,
            "itemsPerPage": itemsPerPage
        };
        return this.http.post(ReportUrlsService._urlGetRequestedItemToShowPerPage, JSON.stringify(body), RequestOptionsService.getRequestOptions())
            .map((response: Response) => response.json() as ShowRequestedItem[])
            .catch(this.handleErrorHere);
    }

    /**
     * send request to controller to filter data in the according to 'filters'
     * @param filters
     */
    public getFilterRequestedItemInitData(filters: FilterRequstedViewModel): Observable<RequestedItemInitViewModel> {
        return this.http.post(ReportUrlsService._urlFilterRequestedItem, JSON.stringify(filters), RequestOptionsService.getRequestOptions())
            .map((response: Response) => response.json() as ShowRequestedItem[])
            .catch(this.handleErrorHere);
    }

    public getIncomeReportData(organizationId: number, startDate: string, endDate: string): Observable<IncomeReportDataViewModel[]> {
        return this.getCollections<IncomeReportDataViewModel>(ReportUrlsService._urlGetIncomeReportData + '/' + organizationId + '?datefrom=' + startDate + '&dateto=' + endDate);
    }

    public getOutcomeReportData(organizationId: number, startDate: string, endDate: string): Observable<OutcomeReportDataViewModel[]> {
        return this.getCollections<OutcomeReportDataViewModel>(ReportUrlsService._urlGetOutcomeReportData + '/' + organizationId + '?datefrom=' + startDate + '&dateto=' + endDate);
    }

    public getInvoiceDeclarationData(organizationId: number, startDate: string, endDate: string): Observable<InvoiceDeclarationResponseViewModel[]> {
        return this.getCollections<InvoiceDeclarationResponseViewModel>(ReportUrlsService._urlGetInvoiceDeclaration + '/' + organizationId + '?datefrom=' + startDate + '&dateto=' + endDate);
    }


    public getUsersDonationsPaginatedReport(reportModel: ReportFilterQueryViewModel): Observable<UsersDonationsReportDataViewModel[]> {
        return this.getCollections<UsersDonationsReportDataViewModel>(GlobalUrlService.usersDonationsPaginatedReport + '?orgId=' + reportModel.id + '&datefrom=' + reportModel.dateFrom + '&dateto=' + reportModel.dateTo + '&pageIndex=' + reportModel.currentPage + '&pageSize=' + reportModel.pageSize + '&filterValue=' + reportModel.filterValue);
    }

    public getCountOfUsersDonationsReportItems(reportModel: ReportFilterQueryViewModel): Observable<number> {
        let _myUrl = GlobalUrlService.countOfUsersDonationsReportItems + '?orgId=' + reportModel.id + '&dateFrom=' + reportModel.dateFrom + '&dateTo=' + reportModel.dateTo + '&filterValue=' + reportModel.filterValue;
        return this.http.get(_myUrl, RequestOptionsService.getRequestOptions())
            .map((response: Response) => response.json() as number);
    }


    public getCommonUsersDonationsPaginatedReport(reportModel: ReportFilterQueryViewModel): Observable<UsersDonationsReportDataViewModel[]> {
        return this.getCollections<UsersDonationsReportDataViewModel>(GlobalUrlService.commonUsersDonationsPaginatedReport + '?orgId=' + reportModel.id + '&datefrom=' + reportModel.dateFrom + '&dateto=' + reportModel.dateTo + '&pageIndex=' + reportModel.currentPage + '&pageSize=' + reportModel.pageSize);
    }

    public getCountOfCommonUsersDonationsReportItems(reportModel: ReportFilterQueryViewModel): Observable<number> {
        let _myUrl = GlobalUrlService.countOfCommonUsersDonationsReportItems + '?orgId=' + reportModel.id + '&dateFrom=' + reportModel.dateFrom + '&dateTo=' + reportModel.dateTo;
        return this.http.get(_myUrl, RequestOptionsService.getRequestOptions())
            .map((response: Response) => response.json() as number);
    }

    public getAllTargetsOfOrganization(orgId: number): Observable<TargetViewModel[]> {
        let _myUrl = GlobalUrlService.getAllTargetsOfOrganization + orgId;
        return this.http.get(_myUrl, RequestOptionsService.getRequestOptions())
            .map((response: Response) => response.json() as TargetViewModel[]);
    }

    public DonationsValueReportPerDay(reportModel: ReportFilterQueryViewModel, targetId: number): Observable<DataSetViewModel[]> {
        return this.getCollections<DataSetViewModel>(GlobalUrlService.donationsvalueReportPerDay + '?orgId=' + reportModel.id +
            '&datefrom=' + reportModel.dateFrom +
            '&dateto=' + reportModel.dateTo +
            '&filterValue=' + targetId);
    }

    public getFinOpImages(finOpId: number): Observable<string[]> {
        return this.getCollections<string>(ReportUrlsService._urlGetFinOpImagesById + '?finopid=' + finOpId);
    }

    public getOrgaizations(): Observable<IOrganizationForFiltering[]> {
        return this.getCollections<IOrganizationForFiltering>(ReportUrlsService._urlGetOrganizations);
    }

    public getCategories(): Observable<GoodsCategoryViewModel[]> {
        return this.getCollections<GoodsCategoryViewModel>(ReportUrlsService._urlGetCategories);
    }

    public getTypes(): Observable<GoodsTypeShortViewModel[]> {
        return this.getCollections<GoodsTypeShortViewModel>(ReportUrlsService._urlGetTypes);
    }

    public getStatuses(): Observable<GoodsStatusViewModel[]> {
        return this.getCollections<GoodsStatusViewModel>(ReportUrlsService._urlGetStatuses);
    }

    private getCollections<T>(_myUrl: string): Observable<T[]> {
        return this.http.get(_myUrl, RequestOptionsService.getRequestOptions())
            .map((response: Response) => response.json() as T[])
            .catch(this.handleErrorHere);
    }

    private handleErrorHere(error: Response) {
        return Observable.throw(error.json().error || 'Server error');
    }
}