import { Component, ViewChild, Input } from '@angular/core';
import { Angular2FontawesomeModule } from 'angular2-fontawesome/angular2-fontawesome';
import { DropdownOrganizationsComponent } from "../../shared/components/dropdown-filtering/dropdown-filtering.component";
import { StorageService } from "../../shared/item-storage-service";

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})

export class AppComponent {

    private _versionNumber: String = 'v-1.0 (changeset-10623)';
    @ViewChild(DropdownOrganizationsComponent) childComponent: DropdownOrganizationsComponent;

    constructor(private _service: StorageService) { }

    callChild() {
        this.childComponent.onSelect();
    }

    /*-------------------------------*/
    /*           For sidebar         */
    /*-------------------------------*/
    public margin: boolean = false;

    public toggleMargin(event: boolean): void {
        this.margin = event;
    }
    /*-------------------------------*/
    /*           For sidebar         */
    /*-------------------------------*/
}
