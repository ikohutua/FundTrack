import { NgModule } from '@angular/core';
import { CommonModule } from "@angular/common";
import { SuperAdminComponent } from './components/super-admin/super-admin.component';
import { SuperAdminRoutingModule } from "./routes/super-admin.routing.module";

@NgModule({
    declarations: [
        SuperAdminComponent
    ],
    imports: [
        CommonModule,
        SuperAdminRoutingModule
    ]
})
export class SuperAdminModule { }
