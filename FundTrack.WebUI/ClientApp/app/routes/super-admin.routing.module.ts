import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { SuperAdminComponent } from "../components/super-admin/super-admin.component";

@NgModule({
    imports: [
        RouterModule.forChild([
            { path: 'superadmin', component: SuperAdminComponent }
        ])
    ],
    exports: [RouterModule]
})
export class SuperAdminRoutingModule{ }