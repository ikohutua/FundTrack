using FundTrack.DAL.Abstract;
using FundTrack.DAL.Entities;
using FundTrack.Infrastructure.ViewModel.FinanceViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FundTrack.Infrastructure;

namespace FundTrack.DAL.Concrete
{
    public class BankImportDetailRepository : IBankImportDetailRepository
    {
        private readonly FundTrackContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="BankImportDetailRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public BankImportDetailRepository(FundTrackContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// Creates the specified bank import.
        /// </summary>
        /// <param name="bankImport">The bank import.</param>
        /// <returns></returns>
        public BankImportDetail Create(BankImportDetail bankImport)
        {
            this._context.BankImportDetails.Add(bankImport);
            return bankImport;
        }

        /// <summary>
        /// Gets the bank import detail by appcode .
        /// </summary>
        /// <param name="appcode">The appcode.</param>
        /// <returns></returns>
        public BankImportDetail GetBankImportDetail(int appcode)
        {
            return this._context.BankImportDetails
                                .FirstOrDefault(bid => bid.AppCode == appcode);
        }

        /// <summary>
        /// Gets the bank imports detail.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<BankImportDetail> GetBankImportsDetail()
        {
            return this._context.BankImportDetails;
        }

        /// <summary>
        /// Filters the bank import detail.
        /// </summary>
        /// <param name="bankImportSearch">The bank import search.</param>
        /// <returns></returns>
        public IEnumerable<BankImportDetail> FilterBankImportDetail(BankImportSearchViewModel bankImportSearch)
        {
            return this._context.BankImportDetails
                .Where(bid => bid.Card == (bankImportSearch.Card != "" ? bankImportSearch.Card : bid.Card))
                .Where(bid => bid.Trandate >= (bankImportSearch.DataFrom.HasValue ? bankImportSearch.DataFrom : bid.Trandate))
                .Where(bid => bid.Trandate <= (bankImportSearch.DataTo.HasValue ? bankImportSearch.DataTo : bid.Trandate))
                .Where(bid => bid.IsLooked == (bankImportSearch.State.HasValue ? bankImportSearch.State : bid.IsLooked));
        }

        /// <summary>
        /// Gets the bank import details in one card.
        /// </summary>
        /// <param name="card">The card.</param>
        /// <returns></returns>
        public IEnumerable<BankImportDetail> GetBankImportDetailsOneCard(string card)
        {
            return this._context.BankImportDetails
                .Where(bid => bid.Card == card);
        }

        /// <summary>
        /// Gets the bankImportDetail by identifier.
        /// </summary>
        /// <param name="bankImportDetailId">The bank import detail identifier.</param>
        /// <returns></returns>
        public BankImportDetail GetById(int bankImportDetailId)
        {
            return this._context.BankImportDetails.FirstOrDefault(bid => bid.Id == bankImportDetailId);
        }

        /// <summary>
        /// Changes the state of the bank import.
        /// </summary>
        /// <param name="bankImportDetailId">The bank import detail identifier.</param>
        /// <param name="state">if set to <c>true</c> [state].</param>
        /// <returns></returns>
        public BankImportDetail ChangeBankImportState(BankImportDetail bankImportDetail)
        {
            try
            {
                var updatedItem = this._context.BankImportDetails.Update(bankImportDetail);
                return updatedItem.Entity;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ErrorMessages.UpdateDataError,ex);
            }
            
        }
    }
}
