import { IMainPageViewModel } from "../Abstract/IMainPageViewModel";
import { IReportModel } from "../Abstract/IReportModel";
import { IEventModel } from "../Abstract/IEventModel";
import { IRequestModel } from "../Abstract/IRequestModel";

export class MainPageViewModel implements IMainPageViewModel {
    reports: IReportModel[];
    events: IEventModel[];
    requests: IRequestModel[];
}