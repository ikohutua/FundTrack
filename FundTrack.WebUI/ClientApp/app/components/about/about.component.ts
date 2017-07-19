import { Component, OnInit, OnDestroy } from "@angular/core";
import { StorageService } from "../../shared/item-storage-service";

@Component({
    templateUrl: './about.component.html',
    styleUrls:['./about.component.css']
})

export class AboutComponent implements OnInit,OnDestroy {
    pageTitle: string;

    constructor(private _storageService: StorageService) { }

    ngOnInit(): void {
        this._storageService.showDropDown = false;
    }

    ngOnDestroy(): void {
        this._storageService.showDropDown = true;
    }
}