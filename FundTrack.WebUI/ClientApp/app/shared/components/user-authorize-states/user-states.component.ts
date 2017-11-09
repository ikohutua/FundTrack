import { Component, OnInit, AfterContentChecked, DoCheck } from "@angular/core";
import * as keys from '../../key.storage';
import { isBrowser } from 'angular2-universal';
import { UserService } from '../../../services/concrete/user.service';
import { AuthorizeUserModel } from '../../../view-models/concrete/authorized-user-info-view.model';
import { StorageService } from '../../item-storage-service';

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
    private idOfOrganization: number;
    public constructor(private _authorizationService: UserService, private _storage: StorageService) { }

    /**
     * close the session current user
     */
    public exit(): void {
        this._authorizationService.sendMessage("From user-states");
        console.log("this._authorizationService.sendMessage(\"From user- states\") was called.");
        this.name = null;
        this.isAdmin = false;
        this._authorizationService.logOff();
        this._storage.emitAuthorizeUserEvent(null);
        this._storage.bannedDescription = '';
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
