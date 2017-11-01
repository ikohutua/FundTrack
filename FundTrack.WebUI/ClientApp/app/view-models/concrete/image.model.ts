export class Image {
    name: string;
    imageSrc: string;
    base64Data: string;
    public isMain: boolean;

    constructor(Name: string, ImageSrc: string, Base64Data: string, ) {
        this.name = Name;
        this.imageSrc = ImageSrc;
        this.base64Data = Base64Data;
    }
}