import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { SharedModule } from "./shared.module";
import { FinanceRoutingModule } from "./routes/finance-routing.module";
import { OrgAccountListComponent } from "./components/finance/orgaccountlist.component";
import { OrgAccountService } from "./services/concrete/finance/orgaccount.service";
import { CreateOrgAccountComponent } from "./components/finance/createorgaccount.component";
import { BrowserModule } from '@angular/platform-browser';
import { ReactiveFormsModule, FormsModule } from '@angular/forms';
import { OrgAccountDetailComponent } from "./components/finance/orgaccountdetail.component";
import { OrgAccountPaymentComponent } from "./components/finance/orgaccountpayment.component";
import { OrgAccountOperationComponent } from "./components/finance/orgaccountoperation.component";
import { BankImportComponent } from "./components/finance/bank-import.component";
import { MakeDonationComponent } from "./components/finance/donate-money.component";

@NgModule({
    declarations: [
        OrgAccountListComponent, 
        CreateOrgAccountComponent,
        OrgAccountDetailComponent,
        OrgAccountPaymentComponent,
        OrgAccountOperationComponent,
        BankImportComponent,    
        MakeDonationComponent
    ],
    imports: [
        CommonModule,
        SharedModule,
        FinanceRoutingModule,
        BrowserModule,
        ReactiveFormsModule , 
        FormsModule
    ],
    providers: [
        OrgAccountService
    ]
})
export class FinanceModule { }