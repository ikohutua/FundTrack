import { Component, Input } from '@angular/core';

@Component({
    selector: 'spinner',
    template: require('./spinner.component.html'),
    styles: [require('./spinner.component.css')]
})

export class SpinnerComponent {
    @Input() public showSpinner: boolean = false;
}