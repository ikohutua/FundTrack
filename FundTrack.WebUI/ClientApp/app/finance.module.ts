import { NgModule } from "@angular/core";
import { CommonModule, DatePipe } from "@angular/common";
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
import { FinOpService } from "./services/concrete/finance/finOp.service";
import { OrgAccountExtractsComponent } from "./components/finance/org-account-extracts.component";
import { IncomeOperationComponent } from "./components/finance/incomeoperation.component";
import { SpendingOperationComponent } from "./components/finance/spendingoperation.component";
import { TransferOperationComponent } from "./components/finance/transferoperation.component";
import { FixingBalanceComponent } from "./components/finance/fixing-balance.component";
import { MyDonationsComponent } from "./components/finance/my-donations.component";
import { DonateService } from "./services/concrete/finance/donate-money.service";


@NgModule({
    declarations: [
        OrgAccountListComponent, 
        CreateOrgAccountComponent,
        OrgAccountDetailComponent,
        OrgAccountPaymentComponent,
        OrgAccountExtractsComponent,
        OrgAccountOperationComponent,
        BankImportComponent,    
        MakeDonationComponent,
        FixingBalanceComponent,
        MyDonationsComponent,
        IncomeOperationComponent,
        SpendingOperationComponent,
        TransferOperationComponent
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
        OrgAccountService,
        FinOpService,
        DatePipe,
        DonateService
    ]
})
export class FinanceModule { }