import { IMainPageViewModel } from "../abstract/main-page-view-model.interface";
import { IReportModel } from "../abstract/report-model.interface";
import { IRequestModel } from "../abstract/request-model.interface";
import { IEventModel } from "../abstract/event-model.interface";

export class MainPageViewModel implements IMainPageViewModel {
    reports: IReportModel[];
    events: IEventModel[];
    requests: IRequestModel[];
}