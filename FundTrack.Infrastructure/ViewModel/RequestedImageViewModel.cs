namespace FundTrack.Infrastructure.ViewModel
{
    public class RequestedImageViewModel
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; }
        public int RequestedItemId { get; set; }
        public bool IsMain { get; set; }
        public string Base64Data { get; set; }
        public string ImageExtension { get; set; }
    }
}
