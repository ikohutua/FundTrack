using FundTrack.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Text;

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
    }
}
