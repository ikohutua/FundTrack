import { Component, Injectable, Input } from '@angular/core';
import { MapService } from "../../../services/concrete/map.service";
import { GoogleGeoCodeResponse } from "../../../models/map/google-geo-code-response.model";
import { IMarker } from "../../../models/map/marker.interface";

//https://angular-maps.com/api-docs/agm-core/components/AgmInfoWindow.html documentation
//https://developers.google.com/maps/documentation/geocoding/intro

@Component({
    selector: 'map-component',
    templateUrl: './map.component.html',
    styleUrls: ['./map.component.css'],
    providers: [MapService]
})

@Injectable()
export class MapComponent {
    constructor(private _service: MapService) { }

    //latitude and longitude can be initialize through attributes in html
    @Input()
    public lat: number = 49.832562;
    @Input()
    public lng: number = 23.999131;
    //The scale of map
    @Input()
    public zoom: number = 10;

    private _googleResponse: GoogleGeoCodeResponse;
    private _key: string = '&key=AIzaSyD7ERhdsJHPHRAkxeRuBm4e0pekX1H2lZ8&language=uk';
    private _showMap: boolean = false;
    private marker: IMarker =
    {
        lat: this.lat,
        lng: this.lng,
        draggable: true
    };

    /**
     * Gets the formatted address by coordinates from the marker
     * @returns string
     */
    public getFormattingAddress(/*callback: (string) => any*/): void {
        this._service.getOne('latlng=' + this.marker.lat.toString() + ',' + this.marker.lng.toString() + this._key)
            .subscribe(result => {
                this._googleResponse = result;
                this.marker.lat = this._googleResponse.results[0].geometry.location.lat;
                this.marker.lng = this._googleResponse.results[0].geometry.location.lng;
                console.log(this._googleResponse.results[0].formatted_address);
                //callback(this._googleResponse.results[0].formatted_address);
            });
    }

    /**
     * Sets the marker on address
     * @param city
     * @param street
     * @param house
     * @returns boolean
     */
    public setMarker(city: string, street: string, house: string): void {
        let address = city + '+' + street + '+' + house;
        this._service.getOne('address=' + address + this._key)
            .subscribe(result => {
                this._googleResponse = result;
                this.marker.lat = this._googleResponse.results[0].geometry.location.lat;
                this.marker.lng = this._googleResponse.results[0].geometry.location.lng;
            });
    }

    testMarker(): void {
        this.setMarker('Lviv', 'Mykolaychuke', '32');
    }

    testAddress(): void {
        this.getFormattingAddress();
    }

    /**
     * Show coordinates in console
     * @param marker
     */
    private clickedMarker(marker: IMarker): void {
        console.log(marker);
    }

    /**
     * Moves marker to place where was click
     * @param $event
     */
    private mapClicked($event: any): void {
        this.refreshMarkerPosition($event);
    }

    /**
     * Changing coordinates when marker was moved
     * @param $event
     */
    private markerDragEnd($event: any): void {
        this.refreshMarkerPosition($event);
    }

    /**
     * Changing coordinates in local marker
     * @param $event
     */
    private refreshMarkerPosition($event: any): void {
        this.marker.lat = $event.coords.lat;
        this.marker.lng = $event.coords.lng;
        this.marker.draggable = true;
    }

    private ShowMap(): void {
        this._showMap = !this._showMap;
    }
}


