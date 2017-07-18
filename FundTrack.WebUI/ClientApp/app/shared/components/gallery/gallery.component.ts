import { Component, Input, ViewChild, OnInit } from "@angular/core";
import { ModalComponent } from '../modal/modal-component';

@Component({
    selector: 'gallery',
    host: { '(window:keydown)': 'hotkeys($event)' },
    template: require('./gallery.component.html'),
    styles: [require('./gallery.component.css')]
})

export class GalleryComponent{
    @Input() public datasource: string[];
    public selectedImage: any;
    private isFirstImage: boolean;
    private isLastImage: boolean;
    private index: number;

    @ViewChild(ModalComponent)

    public modalWindow: ModalComponent

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

    public hotkeys(event) {
        if (this.selectedImage) {
            if (event.keyCode == 37) {
                this.navigate(false);
            } else if (event.keyCode == 39) {
                this.navigate(true);
            }
        }
    }
}