import { ImageModel } from "../../concrete/image-url-view-model";
//model for event management
export interface IEventManagementViewModel {
    id: number;
    organizationId: number;
    description: string;
    createDate: Date;
    images: ImageModel[];
}