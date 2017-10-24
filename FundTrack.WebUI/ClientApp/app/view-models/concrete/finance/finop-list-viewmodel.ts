export class FinOpListViewModel {
    id: number;
    orgId: number;
    finOpType: number;
    finOpName: string;
    cardFromId: number;
    cardToId: number;
    date: string;
    description: string;
    amount: number;
    target: string;
    targetId: number;
    images: string[];
    userId: number;
    currencyShortName: string;
    currencyFullName: string;
    isEditable: boolean;
    error: string;
    donationId: number;
}