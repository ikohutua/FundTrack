import { NgModule } from "@angular/core";
import { ImageItemComponent } from './shared/components/upload-image/image-item.component/image-item.component';
import { ImageListComponent } from './shared/components/upload-image/image-list.component/image-list.component';
import { CommonModule } from "@angular/common";
import { FormsModule } from "@angular/forms";

@NgModule({
    declarations: [
        ImageItemComponent,
        ImageListComponent
    ],
})
export class ImageUploadModule { }