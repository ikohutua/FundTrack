export class DonateAccountViewModel{
    name: string;
    description: string;
    bankAccountId: number;
    merchantId: number;
    merchantPassword: string;
    targetid: number;
    target: string;
}

export class OrganizationDonateAccountsViewModel {
    organizationId: number;
    orgName: string;
    accounts: Array<DonateAccountViewModel>
}

