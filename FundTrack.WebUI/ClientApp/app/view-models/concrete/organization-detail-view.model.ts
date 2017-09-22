import { OrganizationGeneralViewModel } from "./organization-general-view.model";
import { OrgAccountDetailViewModel } from "./finance/org-account-detail-view.model";

export class OrganizationDetailViewModel {
    public organization: OrganizationGeneralViewModel;
    public adminFirstName: string;
    public adminLastName: string;
    public adminPhoneNumber: string;
    public orgAccountsList: OrgAccountDetailViewModel[];
}