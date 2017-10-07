import { Component, Input, Output, EventEmitter, OnInit } from '@angular/core';
import { Image } from "../../../../view-models/concrete/image.model";
import * as message from '../../../common-message.storage';
import * as defaultConfiguration from '../../../default-configuration.storage';
import { ImputImageService } from "../../../input-image-service";

@Component({
    selector: 'image-list',
    templateUrl: './image-list.component.html',
    styleUrls: ['./image-list.component.css'],
    inputs: ['activeColor', 'baseColor', 'overlayColor']
})

export class ImageListComponent implements OnInit {
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
    @Input() maxCount: number;
    @Input() maxSize: number;

    colorStyle: string;
    outlineColorStyle: string;

    ngOnInit(): void {
        if (this.maxCount == 0) {
            this.maxCount = defaultConfiguration.maxImagesCountInList;
        }
    }

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
                this.setMain(this.images[0]);
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


    handleInputChange(startFile: any) {
        if (!startFile.target.files.length) {
            return;
        }

        if (this.images.length >= this.maxCount) {
            alert(message.maxImagesCount);
            return;
        }

        this.loaded = false;
        let imgInpServ: ImputImageService = new ImputImageService();

        imgInpServ.UploadImageFromFile(startFile)
            .then((res) => {
                if (this.images.length == 0) {
                    res.isMain = true;
                }
                this.images.push(res);
                this.getImageChange.emit(this.images);
                this.loaded = true;
            })
            .catch((err) => {
                alert(err.message);
            });
    }
}