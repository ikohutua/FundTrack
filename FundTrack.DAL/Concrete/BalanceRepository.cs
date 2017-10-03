using FundTrack.DAL.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using FundTrack.DAL.Entities;
using FundTrack.Infrastructure.ViewModel.FinanceViewModels;
using System.Linq;

namespace FundTrack.DAL.Concrete
{
    public class BalanceRepository : IBalanceRepository
    {
        private readonly FundTrackContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="BalanceRepository"/> class.
        /// </summary>
        /// <param name="contextParam">The context parameter.</param>
        public BalanceRepository(FundTrackContext contextParam)
        {
            this._context = contextParam;
        }

        public Balance Add(BalanceViewModel balance)
        {
            var result = _context.Balances.Add(new Balance
            {
                Amount = balance.Amount,
                OrgAccountId = balance.OrgAccountId,
                BalanceDate = balance.BalanceDate
            });
            return result.Entity;
        }

        public IEnumerable<Balance> GetAllBalancesByAccountId(int accountId)
        {
            return _context.Balances.Where(x => x.OrgAccountId == accountId);
        }
    }
}
