/// <reference path="../../item-storage-service.ts" />
import { Component, Input } from "@angular/core";

@Component({
    selector: 'gallery-eventdetail',
    template: require('./gallery-event-detail.component.html'),
    styles: [require('./gallery-event-detail.component.css')]
})

export class GalleryEventDetailComponent {
    @Input() public datasource;
    public selectedImage: any;
    private isFirstImage: boolean;
    private isLastImage: boolean;
    private index: number;

    /**
     * find the index selecr image
     * @param image
     */
    public setSelectedImage(image) {
        this.selectedImage = image;
        this.index = this.datasource.indexOf(this.selectedImage);
        this.checkIndexes();
    }

    /**
     * define which image must be choosed
     * @param forward
     */
    public navigate(forward) {
        this.index = this.datasource.indexOf(this.selectedImage) + (forward ? 1 : -1);
        if (this.index >= 0 && this.index < this.datasource.length) {
            this.selectedImage = this.datasource[this.index];
            this.checkIndexes();
        }
    }
    /**
     * check if image is first or last in the list
     */
    public checkIndexes() {
        this.isFirstImage = false;
        this.isLastImage = false;
        if (this.index == 0) {
            this.isFirstImage = true;
        }
        if (this.index == this.datasource.length - 1) {
            this.isLastImage = true;
        }
    }
}