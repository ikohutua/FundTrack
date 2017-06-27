import { Component, Input, EventEmitter, Output } from "@angular/core";

@Component({
    selector: 'select-item',
    templateUrl: './select.component.html'
})

/**
 * Generic component for select box
*/
export class SelectComponent {

    /**
    *items to display in select box
    */
    @Input() items: any[];

    /**
    * event for selecting item in select box
    */
    @Output() onSelect = new EventEmitter();

    /**
     * function to emit select event
     * @param selectedValue
     */
    public onChange(selectedValue: any) {
        this.onSelect.emit(selectedValue);
    }
}