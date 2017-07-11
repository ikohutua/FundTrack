import { NgModule } from '@angular/core';
import { HomeComponent } from './components/home/home.component';
import { AboutComponent } from "./components/about/about.component";
import { AllEventsComponent } from "./components/all-events/all-events.component";
import { EventDetailComponent } from "./components/event-detail/event-detail.component";
import { AllRequestsComponent } from "./components/all-requests/all-requests.component";
import { RequestDetailComponent } from './components/request-detail/request-detail.component';
import { GalleryComponent } from './shared/components/gallery/gallery.component';
import { CommonModule } from "@angular/common";
import { HomeRoutingModule } from "./routes/home-routing.module";
import { MapModule } from "./map.module";
import { SharedModule } from "./shared.module";

@NgModule({
    declarations: [
        HomeComponent,
        AboutComponent,
        AllEventsComponent,
        EventDetailComponent,
        AllRequestsComponent,
        RequestDetailComponent,
        GalleryComponent
    ],
    imports: [
        CommonModule,
        HomeRoutingModule,
        MapModule,
        SharedModule
    ]
})

export class HomeModule { }
