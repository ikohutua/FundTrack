import { Component, Injectable, Input, ElementRef, ViewChild, NgZone, OnInit, Output, EventEmitter, OnChanges } from '@angular/core';
import { IMarker } from "../../../models/map/marker.interface";
import { FormControl } from "@angular/forms";
import { MapsAPILoader, LatLngLiteral, GoogleMapsAPIWrapper } from "@agm/core";
import { } from '@types/googlemaps';
import { IAddressViewModel } from "../../../view-models/abstract/address-model.interface";
import { AddressViewModel } from "../../../view-models/concrete/edit-organization/address-view.model";

@Component({
    selector: 'map-component',
    templateUrl: './map.component.html',
    styleUrls: ['./map.component.css'],
    providers: [GoogleMapsAPIWrapper]
})

@Injectable()
export class MapComponent implements OnInit, OnChanges {
    private _maximumConcurentRequestsToGoogleMap: number;
    private _temporaryAddressForAutocomplete: string;
    private _addresses: string[] = [];
    private _markers: IMarker[] = [];
    private _addressResult: IAddressViewModel;

    //properties for one-marker mode.....................................................
    @Input()
    //identifier for address
    public idAddress: number;
    @Input()
    public city: string = "";
    @Input()
    public street: string = "";
    @Input()
    public house: string = "";
    @Output()
    //Works when the marker position changed 
    onChangeAddress: EventEmitter<IAddressViewModel[]> = new EventEmitter();
    //..................................................................................

    //Latitude can be initialize through attributes in html
    @Input()
    public mainPointerLatitude: number;
    //Longitude can be initialize through attributes in html
    @Input()
    public mainPointerLongitude: number;
    //The scale of the map
    @Input()
    public zoom: number;
    //Attribute for permission to use map in many markers mode
    @Input()
    public allowManyMarkers: boolean;
    //For autocomplete form
    @Input()
    public searchControl: FormControl;
    @Input()
    public showAutocomplete: boolean;
    @ViewChild("search")
    public searchElementRef: ElementRef;

    /**
     * @constructor
     * @param _mapsAPILoader
     * @param _ngZone
     */
    constructor(private _mapsAPILoader: MapsAPILoader, private _ngZone: NgZone, private _mapsApiWrapper: GoogleMapsAPIWrapper) { }

    /**
     * Works when the map in one-marker mode.
     */
    ngOnChanges() {
        if (!this.allowManyMarkers) {
            this.setMarkerFromInputAttributes(this.createAddressString(), this.idAddress);
            this._mapsApiWrapper.triggerMapEvent("resize");
        }
    }

    /**
     * Sets maximum concurent requests to the google maps.
     * Sets main pointer on the current location.
     * Initialize search control for autocomplete form.
     * Initialize autocomplete component.
     */
    ngOnInit(): void {
        this._maximumConcurentRequestsToGoogleMap = 5;
        this.setMainPointerOnCurrentLocation();
        this._mapsApiWrapper.triggerMapEvent("resize");
        //create search FormControl
        this.searchControl = new FormControl();
        if (this.showAutocomplete) {
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
                        this.setMainPointer(place.geometry.location.lat(), place.geometry.location.lng(), 18);
                        this._temporaryAddressForAutocomplete = place.formatted_address;
                    });
                });
            })
        }
        //load Places Autocomplete
    }

    /**
     * Creates the formatted address string, from attributes, for search in google.
     * @returns string with address
     */
    private createAddressString(): string {
        return this.city + " " + this.street + " " + this.house;
    }

    /**
     * Sets the marker, uses input properties.
     * @param address
     * @param id - identifier for address
     */
    private setMarkerFromInputAttributes(address: string, id: number): void {
        this._mapsAPILoader.load().then(() => {
            var geocoder = new google.maps.Geocoder();
            geocoder.geocode({ 'address': address }, (results, status) => {
                if (status.toString() == "OK") {
                    let newMarker = this.createNewMarker(results[0].geometry.location.lat(), results[0].geometry.location.lng(), id);
                    this._markers = [];
                    this.setMainPointer(newMarker.lat, newMarker.lng, 18);
                    this._markers.push(newMarker);
                }
            });
        });
    }

    /**
     * Moves marker to place where was clicked, works when the map in one-marker mode.
     * @param $event
     */
    private mapClicked($event: any): void {
        if (!this.allowManyMarkers) {
            this.setMainPointer($event.coords.lat, $event.coords.lng, 18);
            this._markers[0] = this.createNewMarker(this.mainPointerLatitude, this.mainPointerLongitude, 0);
            this.saveAddressByMarker();
        }
    }

    /**
     * Changing coordinates when the marker was moved. Works in one-marker mode.
     * @param marker
     * @param $event
     */
    private markerDragEnd(marker: any, $event: any): void {
        if (!this.allowManyMarkers) {
            this._markers[0].lat = $event.coords.lat;
            this._markers[0].lng = $event.coords.lng;
            this.setMainPointer(this._markers[0].lat, this._markers[0].lng, 18);
            this.saveAddressByMarker();
        }
    }

    /**
     * Sends the IAddressViewModel to the parent component
     * @param addresses
     */
    private sendAddress(addresses: IAddressViewModel[]): void {
        this.onChangeAddress.emit(addresses);
    }

    /**
     * Saves address by marker. And then emit this address to the parent component.
     * Works in one-marker mode
     */
    private saveAddressByMarker(): void {
        if (!this.allowManyMarkers) {
            this._mapsAPILoader.load().then(() => {
                var location: LatLngLiteral = {
                    lat: this._markers[0].lat,
                    lng: this._markers[0].lng
                };
                var geocoder = new google.maps.Geocoder();
                geocoder.geocode({ 'location': location }, (results, status) => {
                    let address = this.createAddressViewModel(results[0], this._markers[0].id);
                    this.sendAddress([address]);
                });
            });
        }
    }

    /**
     * Creates address view model from google.maps.GeocoderResult
     * @param googleResponse
     * @param id
     * @returns IAddressViewModel
     */
    private createAddressViewModel(googleResponse: google.maps.GeocoderResult, id: number): IAddressViewModel {
        let address = new AddressViewModel();
        address.id = id;
        for (let i = 0; i < googleResponse.address_components.length; i++) {
            address.lat = googleResponse.geometry.location.lat();
            address.lng = googleResponse.geometry.location.lng();
            switch (googleResponse.address_components[i].types.toString()) {
                case 'locality,political':
                    address.city = googleResponse.address_components[i].long_name;
                    break;
                case 'route':
                    address.street = googleResponse.address_components[i].long_name;
                    break;
                case 'street_number':
                    address.house = googleResponse.address_components[i].long_name;
                    break;
            }
        }
        return address;
    }

    /**
     * Sets current location on map.
     */
    private setMainPointerOnCurrentLocation(): void {
        if ("geolocation" in navigator) {
            navigator.geolocation.getCurrentPosition((position) => {
                this.setMainPointer(position.coords.latitude, position.coords.longitude, this.zoom);
            });
        }
    }

    /**
     * Sets the main pointed on coordinates. Sets 
     * @param latitude
     * @param longitude
     * @param zoom
     */
    private setMainPointer(latitude: number, longitude: number, zoom?: number): void {
        this.mainPointerLatitude = latitude;
        this.mainPointerLongitude = longitude;
        if (zoom) {
            this.zoom = zoom;
        }
    }

    /**
     * Saves address in _addresses from autocomplete form, saves new marker if allowed many markers
     * Updates address if no allowed many markers
     */
    private saveMarkerAndAddressFromDataInAutocompleteForm(): void {
        if (this._temporaryAddressForAutocomplete) {
            if (this.allowManyMarkers) {
                var addressContainsInArray = this._addresses.find(a => a == this._temporaryAddressForAutocomplete);
                if (!addressContainsInArray) {
                    this._addresses.push(this._temporaryAddressForAutocomplete);
                    this._markers.push(this.createNewMarker(this.mainPointerLatitude, this.mainPointerLongitude, 0));
                }
            } else {
                this._addresses[0] = this._temporaryAddressForAutocomplete;
                this._markers[0] = this.createNewMarker(this.mainPointerLatitude, this.mainPointerLongitude, 0);
            }
        }
    }

    /**
     * Gets how markers can be on map
     * @param lengthOfArray
     * @returns amount of markers whitch can be on the map
     */
    private getMaximumMarkersOnMap(lengthOfArray: number): number {
        if (this.allowManyMarkers) {
            return (lengthOfArray > this._maximumConcurentRequestsToGoogleMap) ? this._maximumConcurentRequestsToGoogleMap : lengthOfArray;
        } else {
            return 1;
        }
    }

    /**
     * Sets all markers on map if allowed many markers
     * Sets first marker in _addresses
     */
    private setMarkersFromAddresses(): void {
        var maximumMarkersOnMap = this.getMaximumMarkersOnMap(this._addresses.length);
        this._markers = [];
        for (let i = 0; i < maximumMarkersOnMap; i++) {
            this._mapsAPILoader.load().then(() => {
                var geocoder = new google.maps.Geocoder();
                geocoder.geocode({ 'address': this._addresses[i] }, (results, status) => {
                    let newMarker = this.createNewMarker(results[0].geometry.location.lat(), results[0].geometry.location.lng(), 0);
                    this.setMainPointer(newMarker.lat, newMarker.lng, this.zoom);
                    this._markers.push(newMarker);
                });
            });
        }
    }

    /**
     * Creates new instance of marker
     * @param latitude
     * @param longitude
     * @param idParam identifier for address
     * @returns new marker
     */
    private createNewMarker(latitude: number, longitude: number, idParam: number): IMarker {
        return {
            id: idParam,
            draggable: true,
            lat: latitude,
            lng: longitude
        };
    }

    /**
     * Converts IAddressViewModel to formatted address string, for search in the google maps
     * @param addresses 
     * @returns string
     */
    private convertAddressViewModelToFormattedAddressString(addresses: IAddressViewModel): string {
        return addresses.city + " " + addresses.street + " " + addresses.house;
    }

    /**
     * Sets markers on the map.
     * @param allAddresses IAddressViewModel[]
     */
    public setAllMarkersOnTheMap(allAddresses: IAddressViewModel[]): void {
        this._addresses = [];
        for (let i = 0; i < allAddresses.length; i++) {
            this._addresses.push(this.convertAddressViewModelToFormattedAddressString(allAddresses[i]));
        }
        this.setMarkersFromAddresses();
    }
}


