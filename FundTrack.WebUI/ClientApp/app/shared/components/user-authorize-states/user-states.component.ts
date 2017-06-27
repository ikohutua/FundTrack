import { Component, OnInit, AfterContentChecked } from "@angular/core";
import * as keys from '../../key.storage';
import { isBrowser } from 'angular2-universal';
import { UserService } from '../../../services/concrete/user.service';
import { AuthorizeUserModel } from '../../../view-models/concrete/authorization.type';

@Component({
    selector: 'user-states',
    template: require('./user-states.component.html'),
    styleUrls: ['./user-states.component.css'],
    providers: [UserService]
})

export class UserStatesComponent implements AfterContentChecked {

    public user: AuthorizeUserModel;
    public login: string = "";

    public constructor(private _authorizationService: UserService) { }

    /**
     * close the session current user
     */
    exit() {
        this.login = "";
        this._authorizationService.logOff();
    }

    /**
     * check if user is authorized and show login on main page 
     */
    ngAfterContentChecked() {
        if (isBrowser) {
            if (localStorage.getItem(keys.keyToken)) {
                this.user = JSON.parse(localStorage.getItem(keys.keyModel)) as AuthorizeUserModel;
                this.login = this.user.login;
            }
            return true;
        }
        return false;
    }
}
