import { Component, Output, EventEmitter } from "@angular/core";

@Component({
    selector: 'sidebar',
    templateUrl: './sidebar.component.html',
    styleUrls: ['./sidebar.component.css'],
})
export class SidebarComponent {

    @Output() isOpen: EventEmitter<boolean> = new EventEmitter();

    //property for side bar visible mode
    private sideBarIsClosed: boolean = true;

    //hide or show side bar
    private showSideBar(): void {
        if (this.sideBarIsClosed) {
            this.sideBarIsClosed = false;
            this.isOpen.emit(true);
        } else {
            this.sideBarIsClosed = true;
            this.isOpen.emit(false);
        }
    }
}