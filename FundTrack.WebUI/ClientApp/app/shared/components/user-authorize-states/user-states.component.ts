declare var localStorage: any;
import { Component, OnInit } from "@angular/core";
import * as keys from '../../key.storage';

@Component({
    selector: 'user-states',
    template: require('./user-states.component.html')
})

export class UserStatesComponent{

    login: string;

    /**
     * close the session current user
     */
    exit()
    {
        this.login = "";
        localStorage.clear();
    }

    /**
     * check if user is authorized and show login on main page 
     */
    isAuthorize():boolean
    {
        if (typeof window == 'undefined') {
            return false;
        }
        if (localStorage.getItem(keys.keyToken))
        {
            this.login = localStorage.getItem(keys.keyLogin);
            return true;
        }
        return false;
    }
}