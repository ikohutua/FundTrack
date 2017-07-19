import { Component, OnInit } from '@angular/core';
import { UserResponseService } from '../../services/concrete/organization-management/user-responses.service';
import { ActivatedRoute, Router } from "@angular/router";
import { UserResponseOnRequestsViewModel } from '../../view-models/concrete/user-response-on-requests-view.model'

@Component({
    template: require('./user-response.component.html'),
    styles: [require('./user-response.component.css')],
    providers: [UserResponseService]
})

export class UserResponseComponent implements OnInit {
    private _userResponses: UserResponseOnRequestsViewModel[] = new Array<UserResponseOnRequestsViewModel>();

    public constructor(private _userResponseService: UserResponseService,
        private _router: Router,
        private _route: ActivatedRoute, ) { }

    ngOnInit() {
        let organizationId;
        this._route.params.subscribe(params => {
            organizationId = +params["idOrganization"];
        });
        this._userResponseService.getUserResponsesByOrganization(organizationId).subscribe(
            response => {
                this._userResponses = response;
                console.log('User Response');
                console.log(this._userResponses);
            })
    }
}