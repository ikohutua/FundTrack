import { Injectable } from "@angular/core";
import { OfferFilteringViewModel } from "../../../view-models/concrete/offer-filtering-view.model";
import { Observable } from "rxjs/Observable";
import { BehaviorSubject } from 'rxjs/BehaviorSubject';

@Injectable()
export class OfferFilteringService {
    private _sourceActive = new BehaviorSubject<boolean>(true);
    private _sourceInactive = new BehaviorSubject<boolean>(true);

    activeObs$ = this._sourceActive.asObservable();
    inactiveObs$ = this._sourceInactive.asObservable();

    /*
    Changes value of variable to received
    */
    changeActive(value: boolean) {
        this._sourceActive.next(value);
    }
     /*
    Changes value of variable to received
    */
    changeInactive(value: boolean) {
        this._sourceInactive.next(value);
    }
}