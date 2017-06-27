namespace FundTrack.Infrastructure.ViewModel.SuperAdminViewModels
{
    public class PagingItemsViewModel
    {
        /// <summary>
        /// Id for User
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// User Login
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Indicates if the User is banned
        /// </summary>
        public bool IsBanned { get; set; }

        /// <summary>
        /// Gets of Sets BannDescription
        /// </summary>
        public string BannDescription { get; set; }
    }
}
