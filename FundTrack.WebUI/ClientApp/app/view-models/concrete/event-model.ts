import { IEventModel } from "../abstract/event-model.interface";

export class EventModel implements IEventModel {
    id: number;
    organizationId: number;
    organizationName: string;
    description: string;
    createDate: Date;
    imageUrl: string;
}