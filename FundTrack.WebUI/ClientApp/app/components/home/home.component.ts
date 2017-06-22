import { Component, OnInit } from '@angular/core';
import { MainPageViewModel } from "../../view-models/concrete/main-page-view-model";
import { IMainPageViewModel } from "../../view-models/abstract/main-page-view-model.interface";
import { EventComponent } from "../event/event.component";

@Component({
    selector: 'home',
    templateUrl: './home.component.html'
})

export class HomeComponent {
}
