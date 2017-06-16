import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { UniversalModule } from 'angular2-universal';
import { FormsModule } from '@angular/forms';
import { HttpModule } from '@angular/http';
import { AuthorizationModule } from './authorization.module';
import { AppComponent } from './components/app/app.component'
import { HomeModule } from "./home.module";
import { DropdownOrganizationsComponent } from "./shared/components/dropdown-filtering/dropdown-filtering.component";
import { DropdownOrganizationFilterPipe } from "./shared/pipes/organization-list.pipe";

@NgModule({
    bootstrap: [ AppComponent ],
    declarations: [
        AppComponent,
        DropdownOrganizationsComponent,
        DropdownOrganizationFilterPipe
    ],
    imports: [
        UniversalModule, // Must be first import. This automatically imports BrowserModule, HttpModule, and JsonpModule too.
        AuthorizationModule,
        HomeModule,
        FormsModule,
        RouterModule.forRoot([
        ])
    ]
})
export class AppModule {
}
