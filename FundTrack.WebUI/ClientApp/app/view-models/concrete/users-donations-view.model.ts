import { ITotalSum } from "../../view-models/abstract/total-sum-money-amount-interface";

export class UsersDonationsReportDataViewModel implements ITotalSum {
    userId: number;
    userLogin: string;
    userFirstName: string;
    userLastName: string;
    moneyAmount: number;
    target: number;
    description: string;
    date: Date;
}