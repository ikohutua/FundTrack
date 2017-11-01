import { Injectable } from "@angular/core";
import * as message from '../shared/common-message.storage';
import * as defaultConfig from '../shared/default-configuration.storage';
import { Image } from "../view-models/concrete/image.model";
import { BehaviorSubject } from "rxjs/BehaviorSubject";

@Injectable()
export class ImputImageService {

    public UploadImageFromFile(startFile: any): Promise<Image> {
        
        return new Promise<Image>((resolve, reject) => {

            let currentImageFile = startFile.dataTransfer ? startFile.dataTransfer.files[0] : startFile.target.files[0];

            if (!currentImageFile.type.match(defaultConfig.imageRegExPattern)) {
                reject(new Error( message.invalidFormat));
            }

            if (currentImageFile.size > defaultConfig.maxImageSize) {
                reject(new Error( message.exceededImageSize + " " + message.acceptable + " " + defaultConfig.maxImageSize / 1000000 + "Мб"));
            }

            var reader = new FileReader();

            reader.onload = (e: any) => {
                let newLogoUrl = e.target.result;

                var commaInd = newLogoUrl.indexOf(',');
                let base64Code = newLogoUrl.substring(commaInd + 1);

                let image = new Image(currentImageFile.name, newLogoUrl, base64Code);
                resolve(image);
            }
            reader.readAsDataURL(currentImageFile);
        });
    }
}