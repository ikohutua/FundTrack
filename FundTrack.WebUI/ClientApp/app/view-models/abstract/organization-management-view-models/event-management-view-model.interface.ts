import { IImageModel } from "./image-url-view-model.interface";
//model for event management
export interface IEventManagementViewModel {
    id: number;
    organizationId: number;
    description: string;
    createDate: Date;
    images: IImageModel[];
}