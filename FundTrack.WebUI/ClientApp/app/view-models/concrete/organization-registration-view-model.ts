import { IOrganizationRegistrationViewModel } from '../abstract/organization-registration-view-model.interface';
export class OrganizationRegistrationViewModel implements IOrganizationRegistrationViewModel{
    id: number;
    name: string;
    description: string;
    city: string;
    country: string;
    street: string;
    house: string; 
    administratorLogin: string;
    userError: string;
    nameError: string;
}