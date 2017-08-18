export class DonateAccountViewModel{
    name: string;
    description: string;
    bankAccountId: number;
    merchantId: number;
    merchantPassword: string;
}

export class OrganizationDonateAccountsViewModel {
    organizationId: number;
    orgName: string;
    accounts: Array<DonateAccountViewModel>
}

