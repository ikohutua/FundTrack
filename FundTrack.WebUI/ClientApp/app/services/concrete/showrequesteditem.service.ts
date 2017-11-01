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
import { InvoiceDeclarationResponseViewModel } from "../../view-models/concrete/invoice-declaration-response-view-model";
import { ReportUrlsService } from "./report-urls.service";
import Requestoptionsservice = require("./request-options.service");
import RequestOptionsService = Requestoptionsservice.RequestOptionsService;

@Injectable()
export class ShowRequestedItemService extends BaseService<IShowRequestedItem>{
   

    /**
 * @constructor
 * @param http
 */
    constructor(private http: Http) {
        super(http, ReportUrlsService._urlGetRequestedItemToShow);
    }

    /**
     * Gets initial pagination data about organizations
     */
    public getRequestedItemInitData() {
        return this.http.get(ReportUrlsService._urlForPagination)
            .map((response: Response) => response.json() as RequestedItemInitViewModel);
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
        return this.http.post(ReportUrlsService._urlGetRequestedItemToShowPerPage, JSON.stringify(body), this.getRequestOptions())
            .map((response: Response) => response.json() as ShowRequestedItem[])
            .catch(this.handleErrorHere);
    }

    /**
     * send request to controller to filter data in the according to 'filters'
     * @param filters
     */
    public getFilterRequestedItemInitData(filters: FilterRequstedViewModel): Observable<RequestedItemInitViewModel> {
        return this.http.post(ReportUrlsService._urlFilterRequestedItem, JSON.stringify(filters), this.getRequestOptions())
            .map((response: Response) => response.json() as ShowRequestedItem[])
            .catch(this.handleErrorHere);
    }

    public getIncomeReportData(organizationId: number, startDate: string, endDate: string): Observable<IncomeReportDataViewModel[]> {
        return this.getCollections<IncomeReportDataViewModel>(ReportUrlsService._urlGetIncomeReportData + '/'+organizationId+'?datefrom='+startDate+'&dateto='+endDate);
    }

    public getOutcomeReportData(organizationId: number, startDate: string, endDate: string): Observable<OutcomeReportDataViewModel[]> {
        return this.getCollections<OutcomeReportDataViewModel>(ReportUrlsService._urlGetOutcomeReportData + '/' + organizationId + '?datefrom=' + startDate + '&dateto=' + endDate);
    }

    public getInvoiceDeclarationData(organizationId: number, startDate: string, endDate: string): Observable<InvoiceDeclarationResponseViewModel[]> {
        return this.getCollections<InvoiceDeclarationResponseViewModel>(ReportUrlsService._urlGetInvoiceDeclaration + '/' + organizationId + '?datefrom=' + startDate + '&dateto=' + endDate);
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