using System;
using System.Collections.Generic;
using System.Text;

namespace FundTrack.Infrastructure.ViewModel.FinanceViewModels.DonateViewModels
{
    public class DonateViewModel
    {
        public int Id { get; set; }
        public string OrderId { get; set; }
        public int? UserId { get; set; }
        public int CurrencyId { get; set; }
        public int TargetId { get; set; }
        public int BankAccountId { get; set; }
        public double Amount { get; set; }
        public string Description { get; set; }
        public string DonatorEmail { get; set; }
        public DateTime DonationDate { get; set; }
    }
}
