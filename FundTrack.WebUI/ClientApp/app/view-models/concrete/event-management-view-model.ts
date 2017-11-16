import { IEventManagementViewModel } from "../abstract/organization-management-view-models/event-management-view-model.interface";
import { Image } from "./image.model";

export class EventManagementViewModel implements IEventManagementViewModel {
    errorMessage: string;
    id: number;
    organizationId: number;
    description: string;
    createDate: Date;
    images: Image[];
    mainImage: Image;

}