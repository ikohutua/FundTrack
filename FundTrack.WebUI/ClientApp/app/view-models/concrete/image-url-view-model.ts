import { IImageModel } from "../abstract/organization-management-view-models/image-url-view-model.interface";

export class ImageModel implements IImageModel {
    id: number;
    imageUrl: string;
}