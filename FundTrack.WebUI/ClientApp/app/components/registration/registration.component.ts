import { Component } from '@angular/core';
import { Http } from '@angular/http';
import { RegistrationService } from '../shared/registration.service';
import { RegistrationViewModel } from '../shared/registration-view.model';

@Component({
    selector: 'registration',
    template: require('./registration.component.html'),
    styles: [require('./registration.component.css')],
    providers: [RegistrationService]
})

export class RegistrationComponent {

    constructor(private _registrationService: RegistrationService) { }

    private registrationViewModel: RegistrationViewModel = new RegistrationViewModel();

    createUser(registrationViewModel: RegistrationViewModel)
    {
        this._registrationService.CreateUser(registrationViewModel).subscribe();
    }

    showMessage()
    {
        alert(this.registrationViewModel.firstName);
    }
}