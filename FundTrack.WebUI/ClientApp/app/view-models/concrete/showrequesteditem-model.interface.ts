import { IShowRequestedItem } from "../abstract/showrequesteditem-model.interface";

export class ShowRequestedItem implements IShowRequestedItem {
    id: number;
    goodsCategory: string;
    goodsType: string;
    createDate: Date;
    organization: string;
    status: string;
    name: string;
    description: number;
    mainImageUrl: string;
}
