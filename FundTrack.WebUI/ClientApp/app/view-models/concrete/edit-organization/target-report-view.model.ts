import { FinOpListViewModel } from "../finance/finop-list-viewmodel";

export class AbctractTargetViewModel {
    id: number;
    targetName: string;
    sum: number;
    isOpen: boolean = false;
}


export class BaseTargetReportViewModel  extends  AbctractTargetViewModel {
    subTargetsArray: Array<SubTargetReportViewModel>;
}

export class SubTargetReportViewModel extends AbctractTargetViewModel {
    finOpsArray: Array<FinOpListViewModel>;
}