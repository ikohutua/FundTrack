import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { BankImportComponent } from "../components/bank-import/bank-import.component";

@NgModule({
    imports: [RouterModule.forChild(
        [
            { path: 'privat', component: BankImportComponent }
        ])]
})
export class BankImportRoutingModule { }
