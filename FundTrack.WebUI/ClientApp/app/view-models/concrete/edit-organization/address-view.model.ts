import { IAddressViewModel } from '../../abstract/address-model.interface';
export class AddressViewModel implements IAddressViewModel{
    lat: number;
    lng: number;
    id: number;
    country: string; 
    city: string;
    street: string; 
    house: string;
}