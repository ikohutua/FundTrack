import { NgModule } from '@angular/core';
import { HomeComponent } from './components/home/home.component';
import { AboutComponent } from "./components/about/about.component";
import { AllEventsComponent } from "./components/allevents/allevents.component";
import { EventDetailComponent } from "./components/eventdetail/eventdetail.component";
import { CommonModule } from "@angular/common";
import { HomeRoutingModule } from "./routes/home-routing.module";
import { MapModule } from "./map.module";
import { SharedModule } from "./shared.module";

@NgModule({
    declarations: [
        HomeComponent,
        AboutComponent,
        AllEventsComponent,
        EventDetailComponent
    ],
    imports: [
        CommonModule,
        HomeRoutingModule,
        MapModule,
        SharedModule
    ]
})

export class HomeModule { }
