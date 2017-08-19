import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { HomeComponent } from "../components/home/home.component";
import { OrgAccountListComponent } from "../components/finance/orgaccountlist.component";
import { CreateOrgAccountComponent } from "../components/finance/createorgaccount.component";
import { BankImportComponent } from "../components/finance/bank-import.component";
import { MakeDonationComponent } from "../components/finance/donate-money.component";
import { AdminRouteGuard } from "../services/concrete/security/admin-route-guard";
import { PartnerRouteGuard } from "../services/concrete/security/partner-route-guard";

@NgModule({
    imports: [RouterModule.forChild([
        {
            path: 'finance',
            children: [
                {
                    path: 'orgaccounts',
                    component: OrgAccountListComponent,
                    canActivate: [AdminRouteGuard]
                },
                {
                    path: 'createaccount',
                    component: CreateOrgAccountComponent,
                    canActivate: [AdminRouteGuard]
                },
                {
                    path: 'bank-import',
                    component: BankImportComponent,
                    canActivate: [AdminRouteGuard]
                }, 
                {
                    path: 'donate',
                    component: MakeDonationComponent
                }
            ]
            
        }
    ])],
    exports: [RouterModule]
})
export class FinanceRoutingModule { }