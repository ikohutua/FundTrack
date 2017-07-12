namespace FundTrack.Infrastructure.ViewModel.RequestedItemModel
{
    public sealed class RequestedItemPaginationInitViewModel
    {
        /// <summary>
        /// Total amount of users
        /// </summary>
        public int TotalItemsCount { get; set; }

        /// <summary>
        /// Number of Events on one page
        /// </summary>
        public int ItemsPerPage { get; set; }
    }
}
