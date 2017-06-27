namespace FundTrack.Infrastructure.ViewModel.SuperAdminViewModels
{
    public class PaginationInitViewModel
    {
        /// <summary>
        /// Total amount of users
        /// </summary>
        public int TotalItemsCount { get; set; }

        /// <summary>
        /// Number of Items on one page
        /// </summary>
        public int ItemsPerPage { get; set; }
    }
}
