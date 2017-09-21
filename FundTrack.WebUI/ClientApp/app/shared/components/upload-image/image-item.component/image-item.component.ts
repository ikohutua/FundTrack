import { Component, Input, Output, EventEmitter } from '@angular/core';
import { Image } from "../../../../view-models/concrete/image.model";


@Component({
    selector: 'image-item',
    templateUrl: './image-item.component.html',
    styleUrls: ['./image-item.component.css']
})

export class ImageItemComponent{
    @Input() image: Image;
    @Output() delete: EventEmitter<Image> = new EventEmitter<Image>();
   
    onDelete(){
        this.delete.emit(this.image);
    }
}