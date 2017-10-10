namespace FundTrack.Infrastructure.ViewModel.FinanceViewModels.DonateViewModels
{
    public class CheckoutViewModel
    {
        public int payment_id { get; set; }
        public string response_status { get; set; }
        public string checkout_url { get; set; }
    }
}
