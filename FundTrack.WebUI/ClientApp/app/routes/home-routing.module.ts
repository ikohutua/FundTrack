import { NgModule } from "@angular/core";
import { RouterModule } from "@angular/router";
import { HomeComponent } from "../components/home/home.component";
import { AboutComponent } from "../components/about/about.component";
import { AllEventsComponent } from "../components/all-events/all-events.component";
import { EventDetailComponent } from "../components/event-detail/event-detail.component";
import { AllRequestsComponent } from "../components/all-requests/all-requests.component";


@NgModule({
    imports: [
        RouterModule.forChild([
            {
                //path: '', component: HomeComponent,
                //children:
                //[
                //    { path: '', component: AllEventsComponent }
                //]
                path: '', redirectTo: '/home',
                pathMatch: 'full',
            },
            {
                path: 'home', component: HomeComponent,
                children:
                [
                    { path: '', component: AllEventsComponent },
                    { path: 'allevents/:id', component: AllEventsComponent },
                    { path: 'eventdetail/:id', component: EventDetailComponent },
                    { path: 'allrequests', component: AllRequestsComponent }
                ]
            },
            { path: 'about', component: AboutComponent },
        ])
    ],
    exports: [RouterModule]
})

export class HomeRoutingModule { }