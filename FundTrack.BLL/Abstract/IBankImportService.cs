using FundTrack.Infrastructure.ViewModel;
using FundTrack.Infrastructure.ViewModel.FinanceViewModels;
using System;
using System.Collections.Generic;

namespace FundTrack.BLL.Abstract
{
    public interface IBankImportService
    {
        /// <summary>
        /// Registers the bank extracts.
        /// </summary>
        /// <param name="privatViewModel">The privat view model.</param>
        IEnumerable<ImportDetailPrivatViewModel> RegisterBankExtracts(ImportDetailPrivatViewModel[] privatViewModel);

        /// <summary>
        /// Gets the  extracts with filter options.
        /// </summary>
        /// <param name="bankSearchModel">The bank search model.</param>
        /// <returns></returns>
        IEnumerable<ImportDetailPrivatViewModel> GetRawExtracts(BankImportSearchViewModel bankSearchModel);

        /// <summary>
        /// Gets all extracts in one org accounts
        /// </summary>
        /// <param name="card">The card.</param>
        /// <returns></returns>
        IEnumerable<ImportDetailPrivatViewModel> GetAllExtracts(string card);

        /// <summary>
        /// Gets the count extracts in one org accounts
        /// </summary>
        /// <param name="card">The card.</param>
        /// <returns></returns>
        int GetCountExtracts(string card);

        void ImportFromPrivat(int orgAccountId);

        DateTime GetLastPrivatUpdate(int orgId);
    }
}
