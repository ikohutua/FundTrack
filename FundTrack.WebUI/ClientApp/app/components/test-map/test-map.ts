import { Component, QueryList, ViewChildren, ViewChild } from "@angular/core";
import { IAddressViewModel } from "../../view-models/abstract/address-model.interface";
import { MapComponent } from "../../shared/components/map/map.component";

@Component({
    selector: 'map-test',
    templateUrl: './test-map.html'
})
export class TestMap {
    public cityTest: string = "";
    public streetTest: string = "";
    public houseTest: string = "";
    public id: number = 111;

    @ViewChild("map")
    private map: MapComponent;

    setMap() {
        this.map.setAllMarkersOnTheMap(this.allAddresses);
    }


    public allAddresses: any = [{
        id: 1,
        street: "Пасічна",
        city: "Львів",
        country: "Україна",
        house: "1",
        lat: 0,
        lng: 0
    },
    {
        id: 2,
        street: "Зелена",
        city: "Львів",
        country: "Україна",
        house: "11",
        lat: 0,
        lng: 0
    },
    {
        id: 3,
        street: "Стрийська",
        city: "Львів",
        country: "Україна",
        house: "5",
        lat: 0,
        lng: 0
    }];
    private clone: any = [];

    ngOnChange() {
        debugger;
        console.log(this.allAddresses);
    }

    public showAddress(addresses: IAddressViewModel[]): void {
        debugger;
        console.log(addresses);
        this.id = addresses[0].id;
        this.cityTest = addresses[0].city;
        this.streetTest = addresses[0].street;
        this.houseTest = addresses[0].house;
    }
}