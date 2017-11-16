import { Image } from "../../../view-models/concrete/image.model";

export class FinOpListViewModel {
    id: number;
    orgId: number;
    finOpType: number;
    finOpName: string;
    accFromId: number;
    accToId: number;
    date: Date;
    description: string;
    amount: number;
    target: string;
    targetId: number;
    images: Image[];
    userId: number;
    currencyShortName: string;
    currencyFullName: string;
    isEditable: boolean;
    error: string;
    donationId: number;
}