import { Component, OnChanges, Input, EventEmitter, Output } from "@angular/core";

@Component({
    selector: 'pagination-item',
    templateUrl: './page-item.component.html'
})

/**
  * Generic class for grid items
*/
export class PageItemComponent {
    @Input() arrayToDisplay: any[];
    @Input() headers: string[];

    /**
     * Gets object properties and values
     * @param obj
     */
    public getObjectData(obj) { return Object.keys(obj).map((key) => { return { key: key, value: obj[key] } }); }
}