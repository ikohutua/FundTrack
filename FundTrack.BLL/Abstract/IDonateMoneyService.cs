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
        /// <summary>
        /// Returns all donations of user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        IEnumerable<UserDonationsViewModel> GetUserDonations(int userId);
        /// <summary>
        /// Returns all donations of user in date period
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="dateFrom"></param>
        /// <param name="dateTo"></param>
        /// <returns></returns>
        IEnumerable<UserDonationsViewModel> GetUserDonationsByDate(int userId, DateTime dateFrom, DateTime dateTo);
    }
}
