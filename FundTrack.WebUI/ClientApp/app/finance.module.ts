import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { SharedModule } from "./shared.module";
import { FinanceRoutingModule } from "./routes/finance-routing.module";
import { OrgAccountListComponent } from "./components/finance/orgaccountlist.component";
import { OrgAccountService } from "./services/concrete/finance/orgaccount.service";
import { CreateOrgAccountComponent } from "./components/finance/createorgaccount.component";

@NgModule({
    declarations: [
        OrgAccountListComponent, 
        CreateOrgAccountComponent
    ],
    imports: [
        CommonModule,
        SharedModule,
        FinanceRoutingModule
    ],
    providers: [
        OrgAccountService
    ]
})
export class FinanceModule { }