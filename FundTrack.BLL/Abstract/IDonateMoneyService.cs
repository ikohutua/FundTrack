using FundTrack.Infrastructure.ViewModel.FinanceViewModels.DonateViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundTrack.BLL.Abstract
{
    public interface IDonateMoneyService
    {
        OrganizationDonateAccountsViewModel GetAccountForDonation(int organizationId);
        string GetOrderId();
        IEnumerable<TargetViewModel> GetTargets(int id);
        IEnumerable<CurrencyViewModel> GetCurrencies();
        DonateViewModel AddDonation(DonateViewModel item);
        IEnumerable<UserDonationsViewModel> GetUserDonations(int userid);
        IEnumerable<UserDonationsViewModel> GetUserDonationsByDate(int userid, DateTime dateFrom, DateTime dateTo);
    }
}
