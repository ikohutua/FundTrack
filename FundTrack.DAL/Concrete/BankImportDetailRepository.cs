using FundTrack.DAL.Abstract;
using FundTrack.DAL.Entities;
using FundTrack.Infrastructure.ViewModel.FinanceViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

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

        public BankImportDetail Create(BankImportDetail bankImport)
        {
            this._context.BankImportDetails.Add(bankImport);
            return bankImport;
        }

        public BankImportDetail GetBankImportDetail(int appcode)
        {
            return this._context.BankImportDetails
                                .FirstOrDefault(bid => bid.AppCode == appcode);
        }

        public IEnumerable<BankImportDetail> GetBankImportsDetail()
        {
            return this._context.BankImportDetails;
        }

        public IEnumerable<BankImportDetail> FilterBankImportDetail(BankImportSearchViewModel bankImportSearch)
        {
            return this._context.BankImportDetails
                .Where(bid => bid.Card == (bankImportSearch.Card != "" ? bankImportSearch.Card : bid.Card))
                .Where(bid => bid.Trandate >= (bankImportSearch.DataFrom.HasValue ? bankImportSearch.DataFrom : bid.Trandate))
                .Where(bid => bid.Trandate <= (bankImportSearch.DataTo.HasValue ? bankImportSearch.DataTo : bid.Trandate))
                .Where(bid => bid.IsLooked == (bankImportSearch.State.HasValue ? bankImportSearch.State : bid.IsLooked));
        }

        public IEnumerable<BankImportDetail> GetBankImportDetailsOneCard(string card)
        {
            return this._context.BankImportDetails
                .Where(bid => bid.Card == card);
        }

        /// <summary>
        /// Gets the by identifier.
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
            this._context.BankImportDetails.Update(bankImportDetail);
            return bankImportDetail;
        }
    }
}
