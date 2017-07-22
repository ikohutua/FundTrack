import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { TestMap } from "../components/test-map/test-map";

@NgModule({
    imports: [RouterModule.forChild(
    [
            { path: 'map', component: TestMap }
    ])]
})
export class MapRoutingModule { }