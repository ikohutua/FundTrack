import { Injectable } from "@angular/core";
import * as message from '../shared/common-message.storage';
import { Image } from "../view-models/concrete/image.model";
import { BehaviorSubject } from "rxjs/BehaviorSubject";

@Injectable()
export class ImputImagService {
    private startFile: any;
    private currentImageFile: File;
    private image: Image;
    private isImageReady: boolean = false;

    pattern: RegExp = /image-*/;
    maxSize: number = 4000000;

    public imageObserv: BehaviorSubject<Image> = new BehaviorSubject<Image>(this.image);

    public MakeImage(file: any) {
        this.startFile = file;
        this.handleImageLoader();
    }

    private handleImageLoader() {
        this.currentImageFile = this.startFile.dataTransfer ? this.startFile.dataTransfer.files[0] : this.startFile.target.files[0];
        var reader = new FileReader();

        if (!this.currentImageFile.type.match(this.pattern)) {
            throw message.invalidFormat;
        }

        if (this.currentImageFile.size > this.maxSize) {
            throw message.exceededImageSize + " " + message.acceptable + " " + this.maxSize / 1000000 + "Мб";
        }

        reader.onload = this._handleReaderLoaded.bind(this);
        reader.readAsDataURL(this.currentImageFile);
    }

    private _handleReaderLoaded(e: any) {
        var reader = e.target;
        let newLogoUrl = reader.result;

        var commaInd = newLogoUrl.indexOf(',');
        let base64Code = newLogoUrl.substring(commaInd + 1);

        this.image = new Image(this.currentImageFile.name, newLogoUrl, base64Code);
        this.imageObserv.next(this.image);

    }
}