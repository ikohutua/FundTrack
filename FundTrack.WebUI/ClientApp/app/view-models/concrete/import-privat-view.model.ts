export class ImportDetailPrivatViewModel {
    public id: number;
    public card: string;
    public trandate: Date;
    public amount: string;
    public appCode: string;
    public cardAmount: string;
    public rest: string;
    public terminal: string;
    public description: string;
    public isLooked: boolean;
}

export class ImportPrivatViewModel {
    public importsDetail: ImportDetailPrivatViewModel[] = new Array<ImportDetailPrivatViewModel>();
    public error: string;
}
