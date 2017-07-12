import { Component, ViewChild } from '@angular/core'; 
import { MainPageViewModel } from "../../view-models/concrete/main-page-view-model";
import { IMainPageViewModel } from "../../view-models/abstract/main-page-view-model.interface";
import { ActivatedRoute } from "@angular/router";
import { DropdownOrganizationsComponent } from '../../shared/components/dropdown-filtering/dropdown-filtering.component';


@Component({
    selector: 'home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})

export class HomeComponent {
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

    @ViewChild(DropdownOrganizationsComponent) childComponent: DropdownOrganizationsComponent;

    callChild() {
        this.childComponent.onSelect();
    }
}