import {IOfferViewModel} from '../abstract/offer-model.interface';

export class OfferViewModel implements IOfferViewModel{
    description: string;
    error: string;
    goodsCategoryName: string;
    goodsTypeName: string;
    id: number;
    imageUrl: string[] = new Array<string>();
    name: string;
    statusName: string;
    userId: number;
    contactAddress: string;
    contactPhone: string;
    contactName: string;
    constructor() {
        for (var i = 0; i < 6; i++) {
            this.imageUrl[i] = 'https://s3.eu-central-1.amazonaws.com/fundtrack/default-placeholder.png';
        }
        
    }
}