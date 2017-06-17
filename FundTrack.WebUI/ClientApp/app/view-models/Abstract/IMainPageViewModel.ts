import { IReportModel } from "./IReportModel";
import { IEventModel } from "./IEventModel";
import { IRequestModel } from "./IRequestModel";

/** view model for main page */
export interface IMainPageViewModel {
    reports: IReportModel[];
    events: IEventModel[];
    requests: IRequestModel[];
}