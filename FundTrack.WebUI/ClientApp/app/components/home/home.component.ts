import { Component } from '@angular/core'; 
import { MainPageViewModel } from "../../view-models/concrete/main-page-view-model";
import { IMainPageViewModel } from "../../view-models/abstract/main-page-view-model.interface";
import { AllEventsComponent } from "../allevents/allevents.component";
import { ActivatedRoute } from "@angular/router";

@Component({
    selector: 'home',
    templateUrl: './home.component.html',
    styleUrls: ['./home.component.css']
})

export class HomeComponent {}