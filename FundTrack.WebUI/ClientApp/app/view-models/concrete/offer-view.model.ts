import { OfferedItemImageViewModel } from "./offered-item-image-view.model";

export class OfferViewModel {
    description: string;
    error: string;
    goodsCategory: string;
    goodsCategoryId: number;
    goodsTypeName: string;
    goodsTypeId: number;
    id: number;
    images: OfferedItemImageViewModel[] = [];
    imageUrl = new Array<string>();
    name: string;
    statusName: string;
    userId: number;
    contactAddress: string;
    contactPhone: string;
    contactName: string;
}