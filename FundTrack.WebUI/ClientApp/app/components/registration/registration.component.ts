import { Component } from '@angular/core';
import { Http } from '@angular/http';
import { RegistrationViewModel } from '../shared/registration-view.model';

@Component({
    selector: 'registration',
    template: require('./registration.component.html'),
    styles: [require('./registration.component.css')],
})

export class RegistrationComponent {
    private registrationViewModel: RegistrationViewModel = new RegistrationViewModel();
    showMessage()
    {
        alert(this.registrationViewModel.firstName);
    }
}