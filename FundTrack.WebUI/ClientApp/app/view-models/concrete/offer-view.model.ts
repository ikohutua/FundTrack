import { OfferedItemImageViewModel } from "./offered-item-image-view.model";
import { Image } from "./image.model";

export class OfferViewModel {
    description: string;
    error: string;
    goodsCategory: string;
    goodsCategoryId: number;
    goodsTypeName: string;
    goodsTypeId: number;
    id: number;
    images = new Array<OfferedItemImageViewModel>();
    mainImage = new OfferedItemImageViewModel();
    name: string;
    statusName: string;
    userId: number;
    contactAddress: string;
    contactPhone: string;
    contactName: string;
}