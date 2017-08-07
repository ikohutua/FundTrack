import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { HomeComponent } from "../components/home/home.component";
import { OrgAccountListComponent } from "../components/finance/orgaccountlist.component";
import { CreateOrgAccountComponent } from "../components/finance/createorgaccount.component";

@NgModule({
    imports: [RouterModule.forChild([
        {
            path: 'finance',
            children: [
                { path: 'orgaccounts', component: OrgAccountListComponent },
                { path: 'createaccount', component: CreateOrgAccountComponent }
            ]
            
        }
    ])],
    exports: [RouterModule]
})
export class FinanceRoutingModule { }