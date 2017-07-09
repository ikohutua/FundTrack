import {IOfferViewModel} from '../abstract/offer-model.interface';

export class OfferViewModel implements IOfferViewModel{
    description: string;
    error: string;
    goodsCategoryName: string;
    goodsTypeName: string;
    id: number;
    imageUrl: string[];
    name: string;
    statusName: string;
    userId: number;
}