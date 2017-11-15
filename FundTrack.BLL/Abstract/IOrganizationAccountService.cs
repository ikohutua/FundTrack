using FundTrack.DAL.Entities;
using FundTrack.Infrastructure.ViewModel;
using FundTrack.Infrastructure.ViewModel.FinanceViewModels;
using System.Collections.Generic;

namespace FundTrack.BLL.Abstract
{
    public interface IOrganizationAccountService
    {
        OrgAccountViewModel GetOrganizationAccountById(int organizationAccountId);
        IEnumerable<OrgAccountViewModel> GetAccountsByOrganizationId(int organizationId);
        OrgAccountViewModel UpdateOrganizationAccount(OrgAccountViewModel model);
        DeleteOrgAccountViewModel DeleteOrganizationAccount(DeleteOrgAccountViewModel model);
        OrgAccountViewModel CreateOrganizationAccount(OrgAccountViewModel model);
        bool IsBankAccountsWithImportAvailable(int organizationId);
        /// <summary>
        /// Gets the account for select.
        /// </summary>
        /// <param name="organizationId">The organization identifier.</param>
        /// <param name="card">The card.</param>
        /// <returns></returns>
        OrgAccountSelectViewModel GetAccountForSelect(int organizationId, string card);

        /// <summary>
        /// Checks if donation function is enabled
        /// </summary>
        /// <param name="bankAccountId"></param>
        /// <returns></returns>
        bool IsDonationConnected(int orgAccountId);

        bool IsDonationEnabled(int orgAccountId);

        BankAccountDonateViewModel GetDonateCredentials(int orgAccountId);

        bool ToggleDonateFunction(int orgAccountId);

        BankAccountDonateViewModel ConnectDonateFunction(BankAccountDonateViewModel info);

        int GetBankAccountIdByOrgAccountId(int orgAccountId);

        BankAccountDonateViewModel DisableDonateFunction(int bankAccountId);

        BankAccountDonateViewModel ExtractCredentials(int orgAccountId);
        /// <summary>
        /// Check if data can be extracted
        /// </summary>
        /// <param name="orgAccountId"></param>
        /// <returns></returns>
        bool IsExtractsEnabled(int orgAccountId);

        /// <summary>
        /// Set access to get extracts
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        BankAccountDonateViewModel ConnectExtractsFunction(BankAccountDonateViewModel info);

        /// <summary>
        /// Disable the opportunity to get the extracts
        /// </summary>
        /// <param name="orgAccountId"></param>
        /// <returns></returns>
        bool ToggleExtractsFunction(int orgAccountId);

        /// <summary>
        /// Exclude the opportunity to get the extracts
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        BankAccountDonateViewModel DisableExtractsFunction(int bankAccountId);

        bool IsExtractsConnected(int orgAccountId);

    }
}
