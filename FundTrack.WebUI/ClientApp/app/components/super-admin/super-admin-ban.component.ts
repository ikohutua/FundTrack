import { Component,Input, Output, EventEmitter } from '@angular/core';

@Component({
    selector: 'super-admin-ban',
    templateUrl: './super-admin-ban.component.html'
})

export class SuperAdminBanComponent { 
    @Input() BanDescription: string = '';
    @Input() banStatus: string = '';

    @Output() onStatusChange = new EventEmitter();

    public changeStatus() {       
        this.onStatusChange.emit(this.BanDescription);
    }
}