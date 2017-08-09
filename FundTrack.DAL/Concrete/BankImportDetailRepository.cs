using FundTrack.DAL.Abstract;
using FundTrack.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FundTrack.DAL.Concrete
{
    public class BankImportDetailRepository:IBankImportDetailRepository
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
    }
}
