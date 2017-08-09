using FundTrack.Infrastructure.ViewModel;
using FundTrack.Infrastructure.ViewModel.FinanceViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundTrack.BLL.Abstract
{
    public interface IBankImportService
    {
        /// <summary>
        /// Registers the bank extracts.
        /// </summary>
        /// <param name="privatViewModel">The privat view model.</param>
        IEnumerable<ImportDetailPrivatViewModel> RegisterBankExtracts(ImportDetailPrivatViewModel[] privatViewModel);

        IEnumerable<ImportDetailPrivatViewModel> GetRawExtracts(BankImportSearchViewModel bankSearchModel);
    }
}
