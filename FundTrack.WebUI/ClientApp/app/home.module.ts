import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { UniversalModule } from 'angular2-universal';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { AuthorizationModule } from './authorization.module';
import { HomeComponent } from './components/home/home.component';
import { AboutComponent } from "./components/about/about.component";

@NgModule({
    declarations: [
        HomeComponent,
        AboutComponent
    ],
    imports: [
        UniversalModule, // Must be first import. This automatically imports BrowserModule, HttpModule, and JsonpModule too.
        AuthorizationModule,
        RouterModule.forChild([
            { path: '', component: HomeComponent },
            { path: 'about', component: AboutComponent }
        ])
    ]
})
export class HomeModule {
}
