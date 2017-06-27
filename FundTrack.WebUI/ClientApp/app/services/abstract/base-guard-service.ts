import { CanActivate, Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { isBrowser } from 'angular2-universal';
import * as keys from '../../shared/key.storage';
import { AuthorizeUserModel } from '../../view-models/concrete/authorization.type';

/**
 * Abstract class for Guard service
 */

export abstract class BaseGuardService {

    public userModel: AuthorizeUserModel;
    public constructor(private _router: Router, private _roleName: string) { }

    /**
      * check if user is authorized and his role is superadmin
      */
    public canActivate() {
        if (isBrowser) {
            if (localStorage.getItem(keys.keyToken)) {
                this.userModel = JSON.parse(localStorage.getItem(keys.keyModel)) as AuthorizeUserModel;
                if (this.userModel.role == this._roleName) {
                    return true;
                }
                this._router.navigate(['/errorauthorize']);
                return false;
            }
            this._router.navigate(['/login']);
            return false;
        }
    }

}
