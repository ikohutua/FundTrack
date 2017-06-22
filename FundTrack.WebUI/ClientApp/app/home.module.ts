import { NgModule } from '@angular/core';
import { HomeComponent } from './components/home/home.component';
import { AboutComponent } from "./components/about/about.component";
import { CommonModule } from "@angular/common";
import { HomeRoutingModule } from "./routes/home-routing.module";
import { MapModule } from "./map.module";

@NgModule({
    declarations: [
        HomeComponent,
        AboutComponent
    ],
    imports: [
        CommonModule,
        HomeRoutingModule,
        MapModule
    ]
})
export class HomeModule { }
