import { Image } from "../../concrete/image.model";
//model for event management
export interface IEventManagementViewModel {
    id: number;
    errorMessage: string;
    organizationId: number;
    description: string;
    createDate: Date;
    images: Image[];
}