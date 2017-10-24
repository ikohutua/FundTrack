namespace FundTrack.Infrastructure.ViewModel
{
    public class OfferItemChangeStatusViewModel
    {
        public int OfferItemId { get; set; }
        public string OfferItemStatus { get; set; }
        public int UserId { get; set; }
        public string ErrorMessage { get; set; }
    }
}
