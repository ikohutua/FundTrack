import { IAddressViewModel } from '../../abstract/address-model.interface';
export class AddressViewModel implements IAddressViewModel{
    id: number;
    country: string; 
    city: string;
    street: string; 
    house: string;
    lat: number; 
    lng: number;
}