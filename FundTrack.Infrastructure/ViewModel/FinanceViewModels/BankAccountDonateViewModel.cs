using System;
using System.Collections.Generic;
using System.Text;

namespace FundTrack.Infrastructure.ViewModel.FinanceViewModels
{
    public class BankAccountDonateViewModel
    {
        public int BankAccountId { get; set; }
        public int? MerchantId { get; set; }
        public string MerchantPassword { get; set; }
    }
}
