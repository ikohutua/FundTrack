import { Injectable } from "@angular/core";
import { Http, Response, Headers, RequestOptionsArgs, RequestOptions } from "@angular/http";
import { Observable } from "rxjs/Observable";
import { GlobalUrlService } from "../global-url.service";
import { RequestOptionsService } from "../request-options.service";
import { BaseTargetReportViewModel, SubTargetReportViewModel } from "../../../view-models/concrete/edit-organization/target-report-view.model";
import { FinOpListViewModel } from "../../../view-models/concrete/finance/finop-list-viewmodel";

@Injectable()
export class OrganizationStatisticsService {
    constructor(private _http: Http) {
    }

    public getReportForFinopsByTargets(orgId: number, finOpType:number, dateFrom: string, dateTo: string):
        Observable<BaseTargetReportViewModel[]> {
        return this._http.get(GlobalUrlService.getReportForFinopsByTargets + orgId.toString() + '/' + finOpType.toString() + '?dateFrom=' + dateFrom + '&dateTo=' + dateTo, RequestOptionsService.getRequestOptions())
            .map((res: Response) => <BaseTargetReportViewModel[]>res.json())
            .do(res => console.log(res));
    }

    public getSubTargets(orgId: number, finOpType: number, baseTargetId: number, dateFrom: string, dateTo: string): Observable<SubTargetReportViewModel[]>{
        return this._http.get(GlobalUrlService.getSubTargets + orgId.toString() + '/' + finOpType.toString() + '/' + baseTargetId.toString() + '?dateFrom=' + dateFrom + '&dateTo=' + dateTo, RequestOptionsService.getRequestOptions())
            .map((res : Response) => <SubTargetReportViewModel[]>res.json());
    }

    public getFinOpsByTargetId(finOpType: number, targetId: number, dateFrom: string, dateTo: string):
        Observable<FinOpListViewModel[]> {
        return this._http.get(GlobalUrlService.getFinOpsByTargetId + finOpType.toString() + '/' +  targetId.toString() + '?dateFrom=' + dateFrom + '&dateTo=' + dateTo, RequestOptionsService.getRequestOptions())
            .map((res : Response) => <FinOpListViewModel[]>res.json());
    }
}
