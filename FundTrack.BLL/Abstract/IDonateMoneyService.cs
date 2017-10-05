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
        IEnumerable<CurrencyViewModel> GetCurrencies();
        DonateViewModel AddDonation(DonateViewModel item);
        IEnumerable<DonateViewModel> GetAllDonatons();
        DonateViewModel GetDonationById(int id);
        /// <summary>
        /// return suggested donation to finOpOperation
        /// </summary>
        /// <param name="finOpId"></param>
        /// <returns></returns>
        IEnumerable<DonateViewModel> GetSuggestedDonations(int finOpId);
        IEnumerable<UserDonationsViewModel> GetUserDonations(int userid);
        IEnumerable<UserDonationsViewModel> GetUserDonationsByDate(int userid, DateTime dateFrom, DateTime dateTo);
    }
}
