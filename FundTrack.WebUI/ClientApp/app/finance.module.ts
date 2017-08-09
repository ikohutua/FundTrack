import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { SharedModule } from "./shared.module";
import { FinanceRoutingModule } from "./routes/finance-routing.module";
import { OrgAccountListComponent } from "./components/finance/orgaccountlist.component";
import { OrgAccountService } from "./services/concrete/finance/orgaccount.service";
import { CreateOrgAccountComponent } from "./components/finance/createorgaccount.component";
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule } from '@angular/forms';

@NgModule({
    declarations: [
        OrgAccountListComponent, 
        CreateOrgAccountComponent
    ],
    imports: [
        CommonModule,
        SharedModule,
        FinanceRoutingModule,
        BrowserModule,
        ReactiveFormsModule 
    ],
    providers: [
        OrgAccountService
    ]
})
export class FinanceModule { }