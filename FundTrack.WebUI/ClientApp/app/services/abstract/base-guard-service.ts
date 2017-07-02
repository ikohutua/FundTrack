import { CanActivate, Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { isBrowser } from 'angular2-universal';
import * as keys from '../../shared/key.storage';
import { AuthorizeUserModel } from '../../view-models/concrete/authorized-user-info-view.model';

/**
 * Abstract class for Guard service
 */
export abstract class BaseGuardService {

    public userModel: AuthorizeUserModel;
    public constructor(private _router: Router, private _rolesName: string[]) { }

    /**
     * check if user is authorized and his role is contsin in _rolesName
      */
    public canActivate() {
        if (isBrowser) {
            if (localStorage.getItem(keys.keyToken)) {
                this.userModel = JSON.parse(localStorage.getItem(keys.keyModel)) as AuthorizeUserModel;
                for (let i = 0; i < this._rolesName.length; i++) {
                    if (this._rolesName[i] == this.userModel.role) {
                        return true;
                    }
                }
                this._router.navigate(['/errorauthorize']);
                return false;
            }
            this._router.navigate(['/login']);
            return false;
        }
    }

}
