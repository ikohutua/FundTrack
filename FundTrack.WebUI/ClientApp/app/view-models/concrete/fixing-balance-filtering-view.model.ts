import { BalanceViewModel } from "./finance/balance-view.model";

export class FixingBalanceFilteringViewModel {
    firstDayForFixingBalance: string;
    serverDate: string;
    lastFixing: BalanceViewModel;
    hasFinOpsAfterLastFixing: boolean;
}