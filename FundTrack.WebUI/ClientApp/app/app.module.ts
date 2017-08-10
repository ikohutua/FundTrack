import { NgModule } from '@angular/core';
import { UniversalModule } from 'angular2-universal';
import { AppComponent } from './components/app/app.component'
import { HomeModule } from "./home.module";
import { SharedModule } from "./shared.module";
import { AuthorizationModule } from "./authorization.module";
import { SuperAdminModule } from './super-admin.module';
import { AppRoutingModule } from "./routes/app-routing.module";
import { MapModule } from "./map.module";
import { Angular2FontawesomeModule } from 'angular2-fontawesome/angular2-fontawesome';
import { OrganizationManagementModule } from "./organization-management.module";
import { StorageService } from './shared/item-storage-service';
import { OfferManagementModule } from './offer-management.module';
//import { SignalRModule, SignalRConfiguration } from 'ng2-signalr';
import { SpinnerComponent } from "./shared/components/spinner/spinner.component";
import { BankImportModule } from "./bank-import.module";
import { ReactiveFormsModule } from '@angular/forms';
import { FinanceModule } from "./finance.module";
import { ValidatorsService } from "./services/concrete/validators/validator.service";

//function createConfig(): SignalRConfiguration {
//    let signalrConfiguration = new SignalRConfiguration();
//    signalrConfiguration.hubName = 'SuperAdminChatHub';
//    signalrConfiguration.url = 'http://localhost:51116/signalr/hubs';

//    return signalrConfiguration;
//}

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
        BankImportModule,
        MapModule,
        Angular2FontawesomeModule,
        OrganizationManagementModule,
        OfferManagementModule,
        FinanceModule,
        ReactiveFormsModule
        //,
        //SignalRModule.forRoot(createConfig)
    ],
    providers: [StorageService, ValidatorsService]
})
export class AppModule { }
