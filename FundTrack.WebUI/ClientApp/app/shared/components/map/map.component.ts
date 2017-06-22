import { Component, AfterContentChecked } from '@angular/core';

//https://github.com/SebastianM/angular-google-maps/blob/master/src/core/directives/map.ts

@Component({
    selector: 'map-comp',
    templateUrl: './map.component.html',
    styleUrls: ['./map.component.css'],
})
export class MapComponent implements AfterContentChecked {
    ngAfterContentChecked(): void {
        this.ready = true;
    }

    title: string = 'My first AGM project';
    lat: number = 51.678418;
    lng: number = 7.809007;
    ready: boolean = false;
}

