import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { CommonModule } from "@angular/common";

import { SuperAdminComponent } from './components/superAdmin/superAdmin.component';

@NgModule({
    declarations: [
        SuperAdminComponent
    ],
    imports: [
        CommonModule,
        RouterModule.forChild([
            { path: 'superAdmin', component: SuperAdminComponent }
        ])
    ]
})
export class SuperAdminModule { }
