import { IReportModel } from "./report-model.interface";
import { IEventModel } from "./event-model.interface";
import { IRequestModel } from "./request-model.interface";

/** view model for main page */
export interface IMainPageViewModel {
    reports: IReportModel[];
    events: IEventModel[];
    requests: IRequestModel[];
}