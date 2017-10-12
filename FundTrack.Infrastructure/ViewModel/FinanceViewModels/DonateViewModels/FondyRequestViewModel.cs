namespace FundTrack.Infrastructure.ViewModel.FinanceViewModels.DonateViewModels
{
    public class FondyRequestViewModel
    {
        public string order_id { get; set; }
        public int merchant_id { get; set; }
        public string order_desc { get; set; }
        public string signature { get; set; }
        public int amount { get; set; }
        public string currency { get; set; }
        public string server_callback_url { get; set; }
        public string response_url { get; set; }
    }
}
