import { CanActivate, Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { isBrowser } from 'angular2-universal';
import { StorageService } from '../../../shared/item-storage-service';
import * as keys from '../../../shared/key.storage';

@Injectable()
export class BannedOrgGuard {
    /**
     * Creates new instance of BannedOrgGuard
     * @param _router
     * @param _storage
     */
    constructor(private _router: Router, private _storage: StorageService){

    }

    /**
     * checks if user is authorized and his role is contsin in _rolesName
     * _storage is injected in the app module
     * _storage.bannedDescription is initialized in the UserStatesComponent
     */
    public canActivate() {
        if (isBrowser) {            
            if (localStorage.getItem(keys.keyToken)) {
                if (this._storage.bannedDescription.length == 0) {
                    return true;
                }
                this._router.navigate(['/orgbanned']);
                return false;
            }
            this._router.navigate(['/login']);
            return false;
        }
    }    
}
