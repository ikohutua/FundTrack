using System.Collections.Generic;
using FundTrack.DAL.Entities;
using FundTrack.Infrastructure.ViewModel;

namespace FundTrack.BLL.Abstract
{
    public interface IBankService
    {
        /// <summary>
        /// return all banks
        /// </summary>
        IEnumerable<BankViewModel> GetAllBanks();

        /// <summary>
        /// return bank by Id
        /// </summary>
        Bank GetBankById(int bankId);
    }
}
