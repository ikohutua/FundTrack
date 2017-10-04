using System;

namespace FundTrack.Infrastructure.ViewModel.FinanceViewModels.DonateViewModels
{
    public class UserDonationsViewModel
    {
        public int Id { get; set; }
        public string Organization { get; set; }
        public double Amount { get; set; }
        public DateTime Date { get; set; }
        public string Target { get; set; }
        public string Description { get; set; }
    }
}
