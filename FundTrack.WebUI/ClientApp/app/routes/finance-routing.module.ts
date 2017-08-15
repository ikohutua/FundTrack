import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { HomeComponent } from "../components/home/home.component";
import { OrgAccountListComponent } from "../components/finance/orgaccountlist.component";
import { CreateOrgAccountComponent } from "../components/finance/createorgaccount.component";
import { BankImportComponent } from "../components/finance/bank-import.component";

@NgModule({
    imports: [RouterModule.forChild([
        {
            path: 'finance',
            children: [
                { path: 'orgaccounts', component: OrgAccountListComponent },
                { path: 'createaccount', component: CreateOrgAccountComponent },
                { path: 'bank-import', component: BankImportComponent}
            ]
            
        }
    ])],
    exports: [RouterModule]
})
export class FinanceRoutingModule { }