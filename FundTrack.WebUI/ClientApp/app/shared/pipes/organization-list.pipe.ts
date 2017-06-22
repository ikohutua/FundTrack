import { Pipe, PipeTransform } from "@angular/core";
import { IOrganizationForFiltering } from "../../view-models/abstract/organization-for-filtering.interface";

@Pipe({
    name: 'organizationsListPipe'
})

/** filtering whole list of organizaions by keyword. */
export class DropdownOrganizationFilterPipe implements PipeTransform {
    /**
     * filters list of organizations by name
     * @param value : IOrganizationsForLayout[]
     * @param filterBy : string
     * @returns filtered IOrganizationsForLayout[] by string
     */
    transform(value: IOrganizationForFiltering[], filterBy: string): IOrganizationForFiltering[] {
        filterBy = filterBy ? filterBy.toLocaleLowerCase() : null;
        return filterBy ? value.filter((carModel: IOrganizationForFiltering) =>
            carModel.name.toLocaleLowerCase().indexOf(filterBy) !== -1) : value;
    }
}