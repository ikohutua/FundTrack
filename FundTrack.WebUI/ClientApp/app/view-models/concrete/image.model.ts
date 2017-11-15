export class Image {
    id: number;
    name: string;
    imageUrl: string;
    base64Data: string;
    imageExtension: string;
    public isMain: boolean;

    constructor(Name: string, ImageSrc: string, Base64Data: string, ) {
        this.name = Name;
        this.imageUrl = ImageSrc;
        this.base64Data = Base64Data;
    }
}