using System;
using System.Collections.Generic;
using System.Text;

namespace FundTrack.Infrastructure.ViewModel.FinanceViewModels.DonateViewModels
{
    public class PaymentStatusViewModel
    {
        public string order_id { get; set; }
        public int merchant_id { get; set; }
        public string signature { get; set; }
    }
}
