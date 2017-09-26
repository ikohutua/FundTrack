using FundTrack.DAL.Entities;
using FundTrack.Infrastructure.ViewModel;
using FundTrack.Infrastructure.ViewModel.FinanceViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundTrack.BLL.Abstract
{
    public interface IOrganizationAccountService
    {
        OrgAccountViewModel GetOrganizationAccountById(int organizationAccountId);
        IEnumerable<OrgAccountViewModel> GetAccountsByOrganizationId(int organizationId);
        OrgAccountViewModel UpdateOrganizationAccount(OrgAccountViewModel model);
        DeleteOrgAccountViewModel DeleteOrganizationAccount(DeleteOrgAccountViewModel model);
        OrgAccountViewModel CreateOrganizationAccount(OrgAccountViewModel model);

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
        bool IsExtractEnabled(int orgAccountId);
        BankAccountDonateViewModel ConnectExtractsFunction(BankAccountDonateViewModel info);
    }
}
