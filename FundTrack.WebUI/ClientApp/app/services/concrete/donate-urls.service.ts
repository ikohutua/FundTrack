import { Injectable, Inject, NgZone } from '@angular/core';

@Injectable()
export class DonateUrlsService {
    //donate page
    public static sendRequestFondy: string = "api/Donate/SendRequestFondy";
    public static checkPayment: string = "api/Donate/CheckPayment";
    public static accountsForDonate: string = "api/Donate/AccountsForDonate/"; 
    public static orderId: string = "api/Donate/OrderId"; 
    public static currencies: string = "api/Donate/Currencies";
    public static addDonation: string = "api/Donate/AddDonation";
}