import { NgModule } from '@angular/core';
import { CommonModule } from "@angular/common";
import { MapComponent } from "./shared/components/map/map.component";
import { FormsModule, ReactiveFormsModule } from "@angular/forms";
import { UniversalModule } from 'angular2-universal';
import { BrowserModule } from "@angular/platform-browser";
//import { AgmCoreModule } from "@agm/core";

@NgModule({
    imports: [
        CommonModule,
        FormsModule,
        ReactiveFormsModule,
 //       AgmCoreModule.forRoot({ apiKey: 'AIzaSyD7ERhdsJHPHRAkxeRuBm4e0pekX1H2lZ8' })
    ],
    declarations: [
        MapComponent
    ],
    exports: [
        MapComponent,
        FormsModule
    ]
})
export class MapModule { }
