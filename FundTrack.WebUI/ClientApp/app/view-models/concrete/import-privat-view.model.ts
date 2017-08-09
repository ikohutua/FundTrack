export class ImportDetailPrivatViewModel {
    public card: string;
    public trandate: string;
    public trantime: string;
    public amount: string;
    public appCode: string;
    public cardAmount: string;
    public rest: string;
    public terminal: string;
    public description: string;
}

export class ImportPrivatViewModel {
    public idMerchant: number;
    public credit: string;
    public debet: string;
    public importsDetail: ImportDetailPrivatViewModel[];
}
