using FundTrack.DAL.Abstract;
using FundTrack.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundTrack.DAL.Concrete
{
    public class BankImportRepository : IBankImportRepository
    {
        private readonly FundTrackContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="BankImportRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public BankImportRepository(FundTrackContext context)
        {
            this._context = context;
        }

        public BankImport Create(BankImport bankImport)
        {
            var createdBankImport  = this._context.BankImports.Add(bankImport);
            return bankImport;
        }
    }
}
