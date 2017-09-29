import { OrganizationGeneralViewModel } from "./organization-general-view.model";
import { OrgAccountDetailViewModel } from "./finance/org-account-detail-view.model";
import { PersonViewModel } from "./person-view.model";

export class OrganizationDetailViewModel {
    public organization: OrganizationGeneralViewModel;
    public admin: PersonViewModel;
    public orgAccountsList: OrgAccountDetailViewModel[];
}