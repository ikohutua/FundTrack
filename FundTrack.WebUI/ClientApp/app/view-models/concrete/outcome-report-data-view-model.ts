import { ITotalSum } from "../../view-models/abstract/total-sum-money-amount-interface";

export class OutcomeReportDataViewModel implements ITotalSum {
    moneyAmount: number;
    id: number;
    description: string;
    target: number;
    date: Date;
}