using FundTrack.DAL.Entities;
using FundTrack.Infrastructure.ViewModel.FinanceViewModels;
using System.Collections.Generic;
using System.Linq;

namespace FundTrack.DAL.Abstract
{
    public interface IBalanceRepository
    {
        /// <summary>
        /// Creates the balance.
        /// </summary>
        /// <param name="balance">The balance view model for creating</param>
        /// <returns></returns>
        Balance Add(BalanceViewModel balance);

        /// <summary>
        /// Get the all balances for account by id.
        /// </summary>
        /// <param name="accountId">The id of account</param>
        /// <returns></returns>
       IEnumerable <Balance> GetAllBalancesByAccountId(int accountId);

       IQueryable<Balance> GetAllBalances();

       bool Delete(int balanceId);
    } 
}
