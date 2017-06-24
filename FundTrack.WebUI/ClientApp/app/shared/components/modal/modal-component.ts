import { Component } from '@angular/core';
@Component({
    selector: 'app-modal',
    templateUrl: './modal-component.html'
})
export class ModalComponent {

    public visible = false;
    public visibleAnimate = false;
    //Shows modal window
    public show(): void {
        this.visible = true;
        setTimeout(() => this.visibleAnimate = true, 200);
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