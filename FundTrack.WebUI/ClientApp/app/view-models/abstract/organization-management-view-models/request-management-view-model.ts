import { RequestedItemViewModel } from "./requested-item-view.model";

export class RequestManagementViewModel {
    id: number;
    name: string;
    description: string;
    goodsCategory: string;
    goodsCategoryId: number;
    organizationId: number;
    status: string;
    images: RequestedItemViewModel[];
}