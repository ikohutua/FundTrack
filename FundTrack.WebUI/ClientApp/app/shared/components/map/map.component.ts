import { Component, Injectable, Input, ElementRef, ViewChild, NgZone, OnInit } from '@angular/core';
import { IMarker } from "../../../models/map/marker.interface";
import { FormControl } from "@angular/forms";
import { MapsAPILoader, LatLngLiteral } from "@agm/core";
import { } from '@types/googlemaps';
import { Observable } from "rxjs/Observable";

//https://angular-maps.com/api-docs/agm-core/components/AgmInfoWindow.html documentation
//https://developers.google.com/maps/documentation/geocoding/intro

@Component({
    selector: 'map-component',
    templateUrl: './map.component.html',
    styleUrls: ['./map.component.css']
})

export class MapComponent implements OnInit {
    //latitude can be initialize through attributes in html
    @Input()
    public lat: number = 49.832562; longitude

    //longitude can be initialize through attributes in html
    @Input()
    public lng: number = 23.999131;

    //The scale of map
    @Input()
    public zoom: number = 10;

    //attribute for permission to use map for many markers
    @Input()
    public allowManyMarkers: boolean = true;

    //for autocomplete
    @Input()
    public searchControl: FormControl;

    //for autocomplete
    @ViewChild("search")
    public searchElementRef: ElementRef;

    private _temporaryAddressForAutocomplete: string;
    private _formattedAdresses: string[] = [];
    private _markers: IMarker[] = [];

    constructor(private _mapsAPILoader: MapsAPILoader, private _ngZone: NgZone) { }

    ngOnInit(): void {
        //create search FormControl
        this.searchControl = new FormControl();

        //load Places Autocomplete
        this._mapsAPILoader.load().then(() => {
            let autocomplete = new google.maps.places.Autocomplete(this.searchElementRef.nativeElement,
                {
                    types: ["address"]
                });
            autocomplete.addListener("place_changed", () => {
                this._ngZone.run(() => {
                    //get the place result
                    let place: google.maps.places.PlaceResult = autocomplete.getPlace();
                    let geo = google.maps.Geocoder;
                    //verify result
                    if (place.geometry === undefined || place.geometry === null) {
                        return;
                    }
                    //set latitude, longitude and zoom
                    this.lat = place.geometry.location.lat();
                    this.lng = place.geometry.location.lng();
                    this.zoom = 12;
                    this._temporaryAddressForAutocomplete = place.formatted_address;
                });
            });
        });
    }

    /**
     * Saves address in _formattingAdresses from autocomplete form, saves new marker if allowed many markers
     * Updates address if no allowed many markers
     */
    private saveMarkerAndAddress(): void {
        if (this._temporaryAddressForAutocomplete) {
            if (this.allowManyMarkers) {
                var contain = this._formattedAdresses.find(a => a == this._temporaryAddressForAutocomplete);
                if (!contain) {
                    this._formattedAdresses.push(this._temporaryAddressForAutocomplete);
                    this._markers.push({
                        name: this._temporaryAddressForAutocomplete,
                        lat: this.lat,
                        lng: this.lng,
                        draggable: true
                    });
                    alert('Адреса збережена');
                }
            } else {
                this._formattedAdresses[0] = this._temporaryAddressForAutocomplete;
                this._markers[0].draggable = true;
                this._markers[0].lat = this.lat;
                this._markers[0].lng = this.lng;
                this._markers[0].name = this._temporaryAddressForAutocomplete;
            }
        }
    }

    /**
     * Display all adresses on the map.
     * @param addresses: string[]
     */
    public showMarkers(addresses: string[]): void {
        this._formattedAdresses = addresses;
        this.setMarkersByAddresses();
    }

    /**
     * Save all addresses by markers which setted on the map
     */
    public saveAllAddressesByMarkers(): void {
        this.getFormattedAddresses();
    }

    /**
     * Gets Array of formatted addresses
     * @returns string[]
     */
    public getAllAddresses(): string[] {
        return this._formattedAdresses;
    }

    /**
     * Gets the formatted addresses by coordinates from the _markers
     * @returns string
     */
    private getFormattedAddresses(): void {
        this._formattedAdresses = [];
        if (this.allowManyMarkers) {
            var enumerator: number = (this._markers.length > 5) ? 5 : this._markers.length;
        } else {
            var enumerator: number = 1;
        }
        for (let i = 0; i < enumerator; i++) {
            this._mapsAPILoader.load().then(() => {
                var location: LatLngLiteral = {
                    lat: this._markers[i].lat,
                    lng: this._markers[i].lng
                };
                var geocoder = new google.maps.Geocoder();
                geocoder.geocode({ 'location': location }, (results, status) => {
                    if (this.allowManyMarkers) {
                        var contain = this._formattedAdresses.find(a => a == results[0].formatted_address);
                        if (!contain) {
                            this._formattedAdresses.push(results[0].formatted_address);
                        }
                    }
                    else {
                        this._formattedAdresses[0] = results[0].formatted_address;
                    }
                });
            });
        }
    }

    /**
     * Sets all markers on map if allowed many markers
     * Sets first marker in _formattingAdresses
     * @param addresses: string[]
     */
    private setMarkersByAddresses(): void {
        var enumerator: number;
        if (this.allowManyMarkers) {
            enumerator = (this._formattedAdresses.length > 5) ? 5 : this._formattedAdresses.length;
        }
        else {
            enumerator = 1;
        }
        for (let i = 0; i < enumerator; i++) {
            this._mapsAPILoader.load().then(() => {
                var geocoder = new google.maps.Geocoder();
                geocoder.geocode({ 'address': this._formattedAdresses[i] }, (results, status) => {
                    let newMarker: IMarker = {
                        name: results[0].formatted_address,
                        lat: results[0].geometry.location.lat(),
                        lng: results[0].geometry.location.lng(),
                        draggable: false
                    };
                    this._markers.push(newMarker);
                });
            });
        }
    }

    /**
     * Show coordinates in console
     * @param marker
     */
    private clickedMarker(marker: IMarker): void {
        console.log(marker);
    }

    /**
     * Moves marker to place where was click if no allowed many markers
     * Add new marker in _markers if allowed many markers
     * @param $event
     */
    private mapClicked($event: any): void {
        if (this.allowManyMarkers) {
            var newMarker: IMarker = {
                name: "Я тут",
                lat: $event.coords.lat,
                lng: $event.coords.lng,
                draggable: true
            };
            this._markers.push(newMarker);
        } else {
            this.lat = $event.coords.lat;
            this.lng = $event.coords.lng;
            this._markers[0].draggable = true;
        }
    }

    /**
     * Changing coordinates when marker was moved
     * @param $event
     */
    private markerDragEnd(marker: any, $event: any): void {
        var updatedMarker: IMarker = {
            name: "Я тут",
            draggable: true,
            lat: parseFloat(marker.lat),
            lng: parseFloat(marker.lng)
        };

        var newLat = $event.coords.lat;
        var newLng = $event.coords.lng;

        for (var i = 0; i < this._markers.length; i++) {
            if (updatedMarker.lat == this._markers[i].lat && updatedMarker.lng == this._markers[i].lng) {
                this._markers[i].lat = newLat;
                this._markers[i].lng = newLng;
            }
        }
    }

    /**
     * Removed marker from map
     * @param marker
     */
    private removeMarker(marker: any): void {
        this._markers.splice(this._markers.findIndex(m => m.lat == parseFloat(marker.lat)), 1);
    }

    /**
     * Delete all markers from the map
     */
    private clearMapFromMarkers(): void {
        this._markers = [];
    }
}


