import { Component, OnInit } from "@angular/core";
import { IEventManagementViewModel } from "../../view-models/abstract/organization-management-view-models/event-management-view-model.interface";
import { OrganizationManagementEventsService } from "../../services/concrete/organization-management/organization-management-events.service";
import { Subscription } from "rxjs/Subscription";
import { ActivatedRoute } from "@angular/router";

@Component({
    selector: 'org-management-event',
    templateUrl: './organization-manadement-event-edit.component.html',
    styleUrls: ['./organization-manadement-event-edit.component.css']
})
export class OrganizationManadementEventEditComponent implements OnInit {
    private _idForCurrentEvent: number;
    private _event: IEventManagementViewModel;
    private _subscription: Subscription;

    public constructor(private _router: ActivatedRoute, private _service: OrganizationManagementEventsService) { }

    ngOnInit(): void {
        this._subscription = this._router.params.subscribe(params => {
            this._idForCurrentEvent = +params['id'];
            this.getInformationOfEvent(this._idForCurrentEvent);
        });
    }

    ngDestroy(): void {
        this._subscription.unsubscribe();
    }

    public getInformationOfEvent(id: number): void{
        this._service.getOneEventById(id).subscribe(event => this._event = event);
    }
}