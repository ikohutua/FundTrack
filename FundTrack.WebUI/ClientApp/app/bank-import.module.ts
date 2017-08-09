﻿import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule } from '@angular/forms';
import { SharedModule } from "./shared.module";
import { BankImportComponent } from "./components/bank-import/bank-import.component";
import { BankImportRoutingModule } from "./routes/bank-import-routing.module";

@NgModule({
    declarations: [
        BankImportComponent
    ],
    imports: [
        CommonModule,
        BankImportRoutingModule,
        FormsModule,
        SharedModule
    ]
})
export class BankImportModule { }