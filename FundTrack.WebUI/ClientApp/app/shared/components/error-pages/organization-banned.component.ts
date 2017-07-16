import { Component, OnInit } from "@angular/core";
import { StorageService } from '../../../shared/item-storage-service';
//import { SignalR, BroadcastEventListener, SignalRConnection, ConnectionStatus } from 'ng2-signalr'
import { ActivatedRoute } from '@angular/router';

@Component({
    selector: 'org-banned',
    template: require('./organization-banned.component.html'),
})

/**
* Component for Banned Organization
*/
export class OrganizationBannedComponent implements OnInit {
    private _bannedMessage: string = '';

    /**
     * Creates new instance of OrganizationBannedComponent
     * @param _storage
     */
    public constructor(private _storage: StorageService) {       
    }

    /**
     * Trigers when the component is created
     */
    public ngOnInit() {
        this._bannedMessage = this._storage.bannedDescription;            
    }

}
