import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { MapComponent } from "../shared/components/map/map.component";

@NgModule({
    imports: [RouterModule.forChild(
    [
        { path: 'map', component: MapComponent }
    ])]
})
export class MapRoutingModule { }