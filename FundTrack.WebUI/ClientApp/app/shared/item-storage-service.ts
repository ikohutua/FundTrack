import { Injectable, Inject, EventEmitter } from '@angular/core';

@Injectable()
//Service to get user data
export class StorageService {

    /**
    * Indicates that the organization is banned
    */
    public bannedDescription: string = '';
    public showDropDown: boolean = true;

    navchange: EventEmitter<number> = new EventEmitter();
    constructor() { }
    emitNavChangeEvent(number) {
        this.navchange.emit(number);
    }
    getNavChangeEmitter() {
        return this.navchange;
    }
    //public selectedRequestedItem: string = null;
}
