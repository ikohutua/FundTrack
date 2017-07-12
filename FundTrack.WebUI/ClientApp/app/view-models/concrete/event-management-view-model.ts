import { IEventManagementViewModel } from "../abstract/organization-management-view-models/event-management-view-model.interface";
import { IImageModel } from "../abstract/organization-management-view-models/image-url-view-model.interface";

export class EventManagementViewModel implements IEventManagementViewModel {
    id: number;
    organizationId: number;
    description: string;
    createDate: Date;
    images: IImageModel[];
}