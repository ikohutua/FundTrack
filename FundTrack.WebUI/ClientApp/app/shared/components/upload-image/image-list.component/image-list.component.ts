import { Component, Input, Output, EventEmitter } from '@angular/core';
import { Image } from "../../../../view-models/concrete/image.model";
import * as message from '../../../common-message.storage';
import * as defaultConfiguration from '../../../default-configuration.storage';

@Component({
    selector: 'image-list',
    templateUrl: './image-list.component.html',
    styleUrls: ['./image-list.component.css'],
    inputs: ['activeColor', 'baseColor', 'overlayColor']
})

export class ImageListComponent {
    @Input() images: Image[] = [];
    @Output() getImageChange = new EventEmitter<Image[]>();

    activeColor: string = 'green';
    baseColor: string = '#ccc';
    overlayColor: string = 'rgba(255,255,255,0.5)';

    dragging: boolean = false;
    loaded: boolean = false;
    imageLoaded: boolean = false;
    imageSrc: string = '';
    currentFile: File;
    @Input() maxCount: number = 5;
    @Input() maxSize: number;

    colorStyle: string;
    outlineColorStyle: string;

    changeColorStyle() {
        this.colorStyle = this.dragging ?
            ((this.imageSrc.length > 0) ? this.overlayColor : this.activeColor)
            : ((this.imageSrc.length > 0) ? this.overlayColor : this.baseColor);
    }

    changeOutlineColorStyle() {
        this.outlineColorStyle = this.dragging ? this.activeColor : this.baseColor;
    }

    delete(img: Image) {
        let index = this.images.indexOf(img);
        if (index > -1) {
            this.images.splice(index, 1);
            if (img.isMain && this.images.length > 0) {
                this.images[0].isMain = true;
            }
        }
    }

    setMain(img: Image) {
        for (var i = 0; i < this.images.length; i++) {
            this.images[i].isMain = false;
        }
        this.setImageAsMain(img);
    }

    private setImageAsMain(img: Image) {
        img.isMain = true;
    }

    handleDragEnter() {
        this.dragging = true;
        this.changeColorStyle();
        this.changeOutlineColorStyle();
    }

    handleDragLeave() {
        this.dragging = false;
        this.changeColorStyle();
        this.changeOutlineColorStyle();
    }

    handleDrop(e: any) {
        e.preventDefault();
        this.dragging = false;
        this.changeColorStyle();
        this.changeOutlineColorStyle();
        this.handleInputChange(e);
    }

    handleImageLoad() {
        this.imageLoaded = true;
    }

    handleInputChange(e: any) {

        if (this.images.length > this.maxCount - 1) {
            alert(message.maxImagesCount + ": " + this.maxCount);
            return;
        }

        this.currentFile = e.dataTransfer ? e.dataTransfer.files[0] : e.target.files[0];
        var reader = new FileReader();


        if (this.currentFile.size > defaultConfiguration.maxImageSize) {
            alert(message.exceededImageSize + " " + message.acceptable + " " + defaultConfiguration.maxImageSize / 1000000 + 'Мб');
            return;
        }

        if (!this.currentFile.type.match(defaultConfiguration.imageRegExPattern)) {
            alert(message.invalidFormat);
            return;
        }

        if (this.images.find(i => i.name == this.currentFile.name) != null) {
            alert(message.imageIsAlreadySelected);
            return;
        }
        this.loaded = false;
        reader.onload = this._handleReaderLoaded.bind(this);
        reader.readAsDataURL(this.currentFile);
    }

    _handleReaderLoaded(e: any) {
        var reader = e.target;
        this.imageSrc = reader.result;

        var commaInd = this.imageSrc.indexOf(',');
        var byteCharacters = this.imageSrc.substring(commaInd + 1);

        var item = new Image(this.currentFile.name, this.imageSrc, byteCharacters);
        if (this.images.length == 0) {
            this.setImageAsMain(item);
        }
        this.images.push(item);

        this.getImageChange.emit(this.images);
        this.loaded = true;
    }
}