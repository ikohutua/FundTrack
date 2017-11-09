import { Component, ViewChild, Input} from '@angular/core';
import { Angular2FontawesomeModule } from 'angular2-fontawesome/angular2-fontawesome';
import { DropdownOrganizationsComponent } from "../../shared/components/dropdown-filtering/dropdown-filtering.component";
import { StorageService } from "../../shared/item-storage-service";
import { UserService } from "../../services/concrete/user.service";
import { Subscription } from 'rxjs/Subscription';

@Component({
    selector: 'app',
    templateUrl: './app.component.html',
    styleUrls: ['./app.component.css']
})

export class AppComponent {
    private _versionNumber: String = 'v-1.0 (changeset-10623)';
    subscription: Subscription;
    message: any;
    @ViewChild(DropdownOrganizationsComponent) childComponent: DropdownOrganizationsComponent;

    constructor(private _service: StorageService, private _userService: UserService) {
        this.subscription = this._userService.getMessage().subscribe(message => {
            this.message = message;
            console.log(message);
        });
    }

    callChild() {
        this.childComponent.onSelect();
    }

    /*-------------------------------*/
    /*           For sidebar         */
    /*-------------------------------*/
    public margin: boolean = false;

    public toggleMargin(event: boolean): void {
        this.margin = event;
    }
    /*-------------------------------*/
    /*           For sidebar         */
    /*-------------------------------*/
}
