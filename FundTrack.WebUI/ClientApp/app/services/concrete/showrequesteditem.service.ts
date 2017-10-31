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

@Injectable()
export class ShowRequestedItemService extends BaseService<IShowRequestedItem>{
    private _urlForPagination: string = 'api/RequestedItem/GetRequestedItemPaginationData';
    private _urlGetRequestedItemToShowPerPage: string = 'api/RequestedItem/GetRequestedItemToShowPerPage';
    private _urlGetOrganizations: string = 'api/RequestedItem/GetOrganizations';
    private _urlGetCategories: string = 'api/RequestedItem/GetCategories';
    private _urlGetTypes: string = 'api/RequestedItem/GetTypes';
    private _urlGetStatuses: string = 'api/RequestedItem/GetStatuses';
    private _urlFilterRequestedItem: string = 'api/RequestedItem/GetFilterRequestedItemPaginationData';
    private _urlGetIncomeReportData: string = 'api/reports/IncomeReport';
    private _urlGetOutcomeReportData: string = 'api/reports/OutcomeReport';
    private _urlGetFinOpImagesById: string = 'api/reports/FinOpImages';

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
        return this.http.get(this._urlForPagination)
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
        return this.http.post(this._urlGetRequestedItemToShowPerPage, JSON.stringify(body), this.getRequestOptions())
            .map((response: Response) => response.json() as ShowRequestedItem[])
            .catch(this.handleErrorHere);
    }

    /**
     * send request to controller to filter data in the according to 'filters'
     * @param filters
     */
    public getFilterRequestedItemInitData(filters: FilterRequstedViewModel): Observable<RequestedItemInitViewModel> {
        return this.http.post(this._urlFilterRequestedItem, JSON.stringify(filters), this.getRequestOptions())
            .map((response: Response) => response.json() as ShowRequestedItem[])
            .catch(this.handleErrorHere);
    }

    public getIncomeReportData(organizationId: number, startDate: string, endDate: string): Observable<IncomeReportDataViewModel[]> {
        return this.getCollections<IncomeReportDataViewModel>(this._urlGetIncomeReportData + '?orgId=' + organizationId + '&datefrom=' + startDate + '&dateto=' + endDate);
    }

    public getOutcomeReportData(organizationId: number, startDate: string, endDate: string): Observable<OutcomeReportDataViewModel[]> {
        return this.getCollections<OutcomeReportDataViewModel>(this._urlGetOutcomeReportData + '?orgId=' + organizationId + '&datefrom=' + startDate + '&dateto=' + endDate);
    }

    prepareDate(date: Date): string {
        return this.dp.transform(date, 'yyyy-MM-dd')
    }

    public getUsersDonationsPaginatedReport(reportModel: ReportFilterQueryViewModel): Observable<UsersDonationsReportDataViewModel[]> {
        return this.getCollections<UsersDonationsReportDataViewModel>(GlobalUrlService.usersDonationsPaginatedReport + '?orgId=' + reportModel.id + '&datefrom=' + this.prepareDate(reportModel.dateFrom) + '&dateto=' + this.prepareDate(reportModel.dateTo) + '&pageIndex=' + reportModel.currentPage + '&pageSize=' + reportModel.pageSize + '&filterValue=' + reportModel.filterValue);
    }

    public getCountOfUsersDonationsReportItems(reportModel: ReportFilterQueryViewModel): Observable<number> {
        debugger;
        let _myUrl = GlobalUrlService.countOfUsersDonationsReportItems + '?orgId=' + reportModel.id + '&dateFrom=' + this.prepareDate(reportModel.dateFrom) + '&dateTo=' + this.prepareDate(reportModel.dateTo) + '&filterValue=' + reportModel.filterValue;
        return this.http.get(_myUrl, RequestOptionsService.getRequestOptions())
            .map((response: Response) => response.json() as number);
    }


    public getCommonUsersDonationsPaginatedReport(reportModel: ReportFilterQueryViewModel): Observable<UsersDonationsReportDataViewModel[]> {
        return this.getCollections<UsersDonationsReportDataViewModel>(GlobalUrlService.commonUsersDonationsPaginatedReport + '?orgId=' + reportModel.id + '&datefrom=' + this.prepareDate(reportModel.dateFrom) + '&dateto=' + this.prepareDate(reportModel.dateTo) + '&pageIndex=' + reportModel.currentPage + '&pageSize=' + reportModel.pageSize);
    }

    public getCountOfCommonUsersDonationsReportItems(reportModel: ReportFilterQueryViewModel): Observable<number> {
        let _myUrl = GlobalUrlService.countOfCommonUsersDonationsReportItems + '?orgId=' + reportModel.id + '&dateFrom=' + this.prepareDate(reportModel.dateFrom) + '&dateTo=' + this.prepareDate(reportModel.dateTo);
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
            '&datefrom=' + this.prepareDate(reportModel.dateFrom) +
            '&dateto=' + this.prepareDate(reportModel.dateTo) +
            '&filterValue=' + targetId);
    }

    public getFinOpImages(finOpId: number): Observable<string[]> {
        return this.getCollections<string>(this._urlGetFinOpImagesById + '?finopid=' + finOpId);
    }

    public getOrgaizations(): Observable<IOrganizationForFiltering[]> {
        return this.getCollections<IOrganizationForFiltering>(this._urlGetOrganizations);
    }

    public getCategories(): Observable<GoodsCategoryViewModel[]> {
        return this.getCollections<GoodsCategoryViewModel>(this._urlGetCategories);
    }

    public getTypes(): Observable<GoodsTypeShortViewModel[]> {
        return this.getCollections<GoodsTypeShortViewModel>(this._urlGetTypes);
    }

    public getStatuses(): Observable<GoodsStatusViewModel[]> {
        return this.getCollections<GoodsStatusViewModel>(this._urlGetStatuses);
    }

    private getCollections<T>(_myUrl: string): Observable<T[]> {
        return this.http.get(_myUrl)
            .map((response: Response) => response.json() as T[])
            .do(data => console.log('ALL ' + JSON.stringify(data)))
            .catch(this.handleErrorHere);
    }

    private handleErrorHere(error: Response) {
        return Observable.throw(error.json().error || 'Server error');
    }

    /**
    * Create RequestOptions
    */
    private getRequestOptions() {
        let headers = new Headers({ 'Content-Type': 'application/json' });
        let options = new RequestOptions({ headers: headers });
        return options;
    }

}