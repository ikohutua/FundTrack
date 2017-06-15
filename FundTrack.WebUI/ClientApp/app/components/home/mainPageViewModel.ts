/** for display a list with all reports. */
export interface IReportMainPageViewModel {
    id: number;
    organizationId: number;
    organizationName: string;
    date: Date;
}

/** for display new events */
export interface IEventMainPageViewModel {
    id: number;
    organizationId: number;
    organizationName: string;
    description: string;
    date: Date;
    pathToCoverImage: string;
}

/** for display new requests */
export interface IRequestMainPageViewModel {
    id: number;
    organizationId: number;
    organizationName: string;
    description: string;
    date: Date;
    pathToCoverImage: string;
}

/** view model for main page */
export interface IMainPageViewModel {
    reports: IReportMainPageViewModel[];
    events: IEventMainPageViewModel[];
    requests: IRequestMainPageViewModel[];
}

export class MainPageViewModel implements IMainPageViewModel {
    reports: IReportMainPageViewModel[];
    events: IEventMainPageViewModel[];
    requests: IRequestMainPageViewModel[];
}