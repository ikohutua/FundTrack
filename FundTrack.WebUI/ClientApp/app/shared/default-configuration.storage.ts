import { ImportIntervalViewModel } from "../view-models/concrete/import-interval-view-model";

export const defaultOrganizationLogoUrl: string = "https://www.justpro.co/img/no-image.png";
export const imageRegExPattern: RegExp = /image*/;
export const maxImageSize: number = 4000000;
export const maxImagesCountInList: number = 8;
export const incomeUA: string = "Прихід";
export const spendingUA: string = "Розхід";
export const transferUA: string = "Переміщення";
export const incomeTransferUA: string = "Переміщення(+)";
export const spendingTransferUA: string = "Переміщення(-)";
export const nullTargetUA: string = "Не вказано";
export const cashUA: string = "Готівка";
export const bankUA: string = "Банк";
export const incomeId: number = 1;
export const spendingId: number = 0;
export const transferId: number = 2;
export const defaultTargetName: string = "Базове призначення";
export const selectedCategoryName: string = "Базові";
export const successMessageUA: string = "Операція виконана успішно";

 export const intervals: ImportIntervalViewModel[] = new Array<ImportIntervalViewModel>
    (new ImportIntervalViewModel("1 День", 1440), new ImportIntervalViewModel("2 Денs", 2880),
    new ImportIntervalViewModel("12 Годин", 720), new ImportIntervalViewModel("6 Годин", 360));