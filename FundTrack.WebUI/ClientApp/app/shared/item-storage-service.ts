import { Injectable, Inject } from '@angular/core';

@Injectable()
//Service to get user data
export class StorageService {

    /**
    * Indicates that the organization is banned
    */
    public bannedDescription: string = '';
    public showDropDown: boolean = true;
    public newUserResponseCount: number = 0;
    //public selectedRequestedItem: string = null;
}
