import { NgModule } from '@angular/core';
import { UniversalModule } from 'angular2-universal';
import { AppComponent } from './components/app/app.component'
import { HomeModule } from "./home.module";
import { SharedModule } from "./shared.module";
import { AuthorizationModule } from "./authorization.module";
import { SuperAdminModule } from './super-admin.module';
import { AppRoutingModule } from "./routes/app-routing.module";
import { MapModule } from "./map.module";



@NgModule({
    bootstrap: [AppComponent],
    declarations: [
        AppComponent
    ],
    imports: [
        UniversalModule, // Must be first import. This automatically imports BrowserModule, HttpModule, and JsonpModule too.
        HomeModule,
        SharedModule,
        AuthorizationModule,
        SuperAdminModule,
        AppRoutingModule, 
        MapModule
    ]
})
export class AppModule { }
