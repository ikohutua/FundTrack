import { Component, Input, Injectable } from '@angular/core';

@Component({
    selector: 'spinner',
    template: require('./spinner.component.html'),
    styles: [require('./spinner.component.css')]
})

export class SpinnerComponent {
    @Input() public showSpinner: boolean = false;

    public show() {
        this.showSpinner = true;
    }

    public hide() {
        this.showSpinner = false;
    }
}