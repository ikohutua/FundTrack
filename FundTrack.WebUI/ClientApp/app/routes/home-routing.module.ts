import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { HomeComponent } from "../components/home/home.component";
import { AboutComponent } from "../components/about/about.component";
import { AllEventsComponent } from "../components/allevents/allevents.component";
import { EventDetailComponent } from "../components/eventdetail/eventdetail.component";


@NgModule({
    imports: [
        RouterModule.forChild([
            {
                path: '', component: HomeComponent,
                children:
                [
                    { path: '', component: AllEventsComponent }
                ]
            },
            {
                path: 'home', component: HomeComponent,
                children:
                [
                    { path: '', component: AllEventsComponent },
                    { path: 'allevents/:id', component: AllEventsComponent },
                    { path: 'eventdetail/:id', component: EventDetailComponent },
                ]
            },
            { path: 'about', component: AboutComponent },
        ])
    ],
    exports: [RouterModule]
})

export class HomeRoutingModule { }