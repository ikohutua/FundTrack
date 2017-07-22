import { Component , EventEmitter, Output} from '@angular/core';
import { Observable } from "rxjs/Observable";
import { EmptyObservable } from 'rxjs/observable/EmptyObservable';
import 'rxjs/add/observable/of';
@Component({
    selector: 'app-modal',
    templateUrl: './modal-component.html'
})
export class ModalComponent {

    public visible = false;
    public visibleAnimate = false;

    @Output()
    onModalOpen: EventEmitter<any> = new EventEmitter();    

    //Shows modal window
    public show(): void {
        this.visible = true;
        setTimeout(() => this.visibleAnimate = true, 200);
    }

    //show modal with event emitter 
    public showEmits(): void {
        this.show();
        this.onModalOpen.emit(null);
          
    }

    //Hides modal window
    public hide(): void {
        this.visibleAnimate = false;
        setTimeout(() => this.visible = false, 200);
    }
    
    //Handles mouse events inside the modal
    public onContainerClicked(event: MouseEvent): void {
        if ((<HTMLElement>event.target).classList.contains('modal')) {
            this.hide();
        }
    }
}