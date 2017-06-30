import { IEventDetailModel } from "../abstract/eventdetail-model.interface";

export class EventDetailModel implements IEventDetailModel {
    id: number;
    organizationId: number;
    organizationName: string;
    description: string;
    createDate: Date;
    imageUrl: string[];
}