import { Injectable, Inject, EventEmitter } from '@angular/core';
import { AuthorizedUserInfoViewModel, AuthorizeUserModel } from "../view-models/concrete/authorized-user-info-view.model";

@Injectable()
//Service to get user data
export class StorageService {

    /**
    * Indicates that the organization is banned
    */
    public bannedDescription: string = '';
    public showDropDown: boolean = true;

    navchange: EventEmitter<number> = new EventEmitter();
    authorizeUser: EventEmitter<AuthorizeUserModel> = new EventEmitter();

    constructor() { }

    emitNavChangeEvent(number) {
        this.navchange.emit(number);
    }

    getNavChangeEmitter() {
        return this.navchange;
    }

    emitAuthorizeUserEvent(user) {
        this.authorizeUser.emit(user);
    }

    getAuthorizeUserEmitter() {
        return this.authorizeUser;
    }   
}

