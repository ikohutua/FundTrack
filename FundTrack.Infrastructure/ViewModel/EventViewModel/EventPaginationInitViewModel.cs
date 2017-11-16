namespace FundTrack.Infrastructure.ViewModel.EventViewModel
{
    /// <summary>
    /// Class which describe model to init data for pagination
    /// </summary>
    public sealed class EventPaginationInitViewModel
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
