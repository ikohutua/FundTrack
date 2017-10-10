using FundTrack.DAL.Entities;
using FundTrack.Infrastructure.ViewModel.FinanceViewModels;
using System.Collections.Generic;

namespace FundTrack.DAL.Abstract
{
    public interface IBankImportDetailRepository
    {
        /// <summary>
        /// Creates the specified bank import.
        /// </summary>
        /// <param name="bankImport">The bank import.</param>
        /// <returns></returns>
        BankImportDetail Create(BankImportDetail bankImport);

        /// <summary>
        /// Gets the bank import detail.
        /// </summary>
        /// <param name="appcode">The appcode.</param>
        /// <returns></returns>
        BankImportDetail GetBankImportDetail(int appcode);

        /// <summary>
        /// Gets the bank imports detail.
        /// </summary>
        /// <returns></returns>
        IEnumerable<BankImportDetail> GetBankImportsDetail();

        /// <summary>
        /// Filters the bank import detail.
        /// </summary>
        /// <param name="bankImportSearch">The bank import search.</param>
        /// <returns></returns>
        IEnumerable<BankImportDetail> FilterBankImportDetail(BankImportSearchViewModel bankImportSearch);

        /// <summary>
        /// Gets the bank import details one card.
        /// </summary>
        /// <param name="card">The card.</param>
        /// <returns></returns>
        IEnumerable<BankImportDetail> GetBankImportDetailsOneCard(string card);

        /// <summary>
        /// Changes the state of the bank import.
        /// </summary>
        /// <param name="state">if set to <c>true</c> [state].</param>
        /// <returns></returns>
        BankImportDetail ChangeBankImportState(BankImportDetail bankImportDetail);

        /// <summary>
        /// Gets the by identifier.
        /// </summary>
        /// <param name="bankImportDetailId">The bank import detail identifier.</param>
        /// <returns></returns>
        BankImportDetail GetById(int bankImportDetailId);

    }
}
