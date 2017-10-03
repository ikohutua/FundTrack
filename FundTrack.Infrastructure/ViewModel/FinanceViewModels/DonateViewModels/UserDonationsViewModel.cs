using System;

namespace FundTrack.Infrastructure.ViewModel.FinanceViewModels.DonateViewModels
{
    public class UserDonationsViewModel
    {
        public string Organization { get; set; }
        public double Sum { get; set; }
        public DateTime Date { get; set; }
        public string Target { get; set; }
        public string Description { get; set; }
    }
}
