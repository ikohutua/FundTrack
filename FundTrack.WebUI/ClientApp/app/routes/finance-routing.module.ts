import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { HomeComponent } from "../components/home/home.component";
import { OrgAccountListComponent } from "../components/finance/orgaccountlist.component";
import { CreateOrgAccountComponent } from "../components/finance/createorgaccount.component";
import { BankImportComponent } from "../components/finance/bank-import.component";
import { MakeDonationComponent } from "../components/finance/donate-money.component";
import { AdminRouteGuard } from "../services/concrete/security/admin-route-guard";
import { PartnerRouteGuard } from "../services/concrete/security/partner-route-guard";
import { MyDonationsComponent } from "../components/finance/my-donations.component";
import { IncomeOperationComponent } from "../components/finance/incomeoperation.component";
import { SpendingOperationComponent } from "../components/finance/spendingoperation.component";
import { TransferOperationComponent } from "../components/finance/transferoperation.component";

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
                    path: 'income',
                    component: IncomeOperationComponent,
                    canActivate: [AdminRouteGuard]
                },
                {
                    path: 'income/:id',
                    component: IncomeOperationComponent,
                    canActivate: [AdminRouteGuard]
                },
                {
                    path: 'spending',
                    component: SpendingOperationComponent,
                    canActivate: [AdminRouteGuard]
                },
                {
                    path: 'spending/:id',
                    component: SpendingOperationComponent,
                    canActivate: [AdminRouteGuard]
                },
                {
                    path: 'transfer/:id',
                    component: TransferOperationComponent,
                    canActivate: [AdminRouteGuard]
                },
                {
                    path: 'donate',
                    component: MakeDonationComponent
                },
                {
                    path: 'donate/:id',
                    component: MakeDonationComponent
                }
            ]
            
        },
        {
            path: 'mydonations', component: MyDonationsComponent
        }
    ])],
    exports: [RouterModule]
})
export class FinanceRoutingModule { }