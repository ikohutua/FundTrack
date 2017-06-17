import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { HomeComponent } from "../components/home/home.component";
import { AboutComponent } from "../components/about/about.component";

@NgModule({
    imports: [
        RouterModule.forChild([
            { path: '', component: HomeComponent },
            { path: 'about', component: AboutComponent }
        ])
    ],
    exports: [RouterModule]
})
export class HomeRoutingModule{ }