/**
 * model which describe information about RequestedItem
 */
export class RequestedItemDetailViewModel {
    id: number;
    name: string;
    description: string;
    goodsCategoryId: number;
    goodsCategoryName: string;
    goodsTypeId: number;
    goodsTypeName: string;
    statusId: number;
    statusName: string;
    organizationId: number;
    organizationName: string;
    imagesUrl: string[];
    mainImageUrl: string;
}