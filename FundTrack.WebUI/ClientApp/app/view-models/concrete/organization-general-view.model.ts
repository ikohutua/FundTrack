import { IOrganizationForFiltering } from "../abstract/organization-for-filtering.interface";

export class OrganizationGeneralViewModel implements IOrganizationForFiltering {
    id: number;
    name: string; 
    description: string;
    isBanned: boolean;
    logoUrl: string;
}