using System;
using FundTrack.Infrastructure.ViewModel;
using FundTrack.Infrastructure.ViewModel.FinanceViewModels;
using System.Collections.Generic;

namespace FundTrack.BLL.Abstract
{
    public interface IFixingBalanceService
    {
        /// <summary>
        /// Get data for filtering on fixing balance component
        /// </summary>
        /// <param name="accountId">Id of account</param>
        /// <returns>Filtering for fixing balance</returns>
        FixingBalanceFilteringViewModel GetFilteringByAccId(int accountId);

        /// <summary>
        /// Fixing balance
        /// </summary>
        /// <param name="balance">Instance with info ablout fixing.</param>
        /// <returns>Added data</returns>
        BalanceViewModel AddNewBalance(BalanceViewModel balance);
        IEnumerable<BalanceViewModel> AddNewRangeOfBalances(IEnumerable< BalanceViewModel> balances);
    }
}
