import { Component, OnInit, AfterContentChecked } from "@angular/core";
import * as keys from '../../key.storage';
import { isBrowser } from 'angular2-universal';
import { UserService } from '../../../services/concrete/user.service';
import { AuthorizeUserModel } from '../../../view-models/concrete/authorized-user-info-view.model';

@Component({
    selector: 'user-states',
    template: require('./user-states.component.html'),
    styleUrls: ['./user-states.component.css'],
    providers: [UserService]
})

export class UserStatesComponent implements AfterContentChecked {

    public user: AuthorizeUserModel;
    public name: string = '';
    private isAdmin: boolean = false;
    public constructor(private _authorizationService: UserService
    ) { }

    /**
     * close the session current user
     */
    public exit():void {
        this.name = null;
        this.isAdmin = false;
        this._authorizationService.logOff();
    }

    /**
     * check if user is authorized and show login on main page 
     */
    ngAfterContentChecked(): boolean {
        if (isBrowser) {
            if (localStorage.getItem(keys.keyToken)) {
                this.user = JSON.parse(localStorage.getItem(keys.keyModel)) as AuthorizeUserModel;
                this.name = this.user.firstName;
                if (this.user.role == 'superadmin') {
                    this.isAdmin = true;
                }
            }
            return true;
        }
        return false;
    }
}
