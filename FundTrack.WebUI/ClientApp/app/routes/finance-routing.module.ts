import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { HomeComponent } from "../components/home/home.component";
import { OrgAccountListComponent } from "../components/finance/orgaccountlist.component";

@NgModule({
    imports: [RouterModule.forChild([
        {
            path: 'finance/orgaccounts', component: OrgAccountListComponent
        }

    ])],
    exports: [RouterModule]
})
export class FinanceRoutingModule { }