import { Component } from '@angular/core';
import { AuthorizationComponent } from '../authorization/authorization.component';
@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})
export class AppComponent {
    public login: string = "";
    /**
     * use for display login in main page
     * @param login
     */
    setLogin(login: string) {
        this.login = login;
        console.log("Login "+this.login);
    }
    /**
     * log on and clear user from session
     */
    exit() {
        sessionStorage.clear();
        this.login = "";
    }
}
