import { ITotalSum } from "../../view-models/abstract/total-sum-money-amount-interface";

export class IncomeReportDataViewModel implements ITotalSum {
    moneyAmount: number;
    description: string;
    from?: any;
    targetTo: string;
    date: Date;
}