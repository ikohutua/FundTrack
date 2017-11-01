namespace FundTrack.Infrastructure.ViewModel
{
    public sealed class OfferedItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int UserId { get; set; }
        public string[] Base64Images { get; set; }
        public string StatusName { get; set; }
        public string GoodsCategoryName { get; set; }
        public string GoodsTypeName { get; set; }
        public string Error { get; set; }
        public string ContactAddress { get; set; }
        public string ContactPhone { get; set; }
        public string ContactName { get; set; }
        public int GoodsCategoryId { get; set; }
        public int GoodsTypeId { get; set; }
        public OfferedItemImageViewModel[] Images { get; set; }

    }
}
