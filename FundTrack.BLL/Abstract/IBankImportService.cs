using FundTrack.Infrastructure.ViewModel;
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
        ImportPrivatViewModel RegisterBankExtracts(ImportPrivatViewModel privatViewModel);
    }
}
