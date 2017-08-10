import { NgModule } from "@angular/core";
import { CommonModule } from "@angular/common";
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { SharedModule } from "./shared.module";
import { BankImportComponent } from "./components/bank-import/bank-import.component";
import { BankImportRoutingModule } from "./routes/bank-import-routing.module";
import { BrowserModule } from "@angular/platform-browser";

@NgModule({
    declarations: [
        BankImportComponent
    ],
    imports: [
        CommonModule,
        BankImportRoutingModule,
        FormsModule,
        BrowserModule,
        SharedModule,
        ReactiveFormsModule 
    ]
})
export class BankImportModule { }