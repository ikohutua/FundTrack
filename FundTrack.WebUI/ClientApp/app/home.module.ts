import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { AboutComponent } from "./components/about/about.component";
import { CommonModule } from "@angular/common";

@NgModule({
    declarations: [
        HomeComponent,
        AboutComponent
    ],
    imports: [
        CommonModule,
        RouterModule.forChild([
            { path: '', component: HomeComponent },
            { path: 'about', component: AboutComponent }
        ])
    ]
})
export class HomeModule { }
