export class ImportDetailPrivatViewModel {
    public card: string;
    public trandate: Date;
    public amount: string;
    public appCode: string;
    public cardAmount: string;
    public rest: string;
    public terminal: string;
    public description: string;
}

export class ImportPrivatViewModel {
    public importsDetail: ImportDetailPrivatViewModel[];
}
