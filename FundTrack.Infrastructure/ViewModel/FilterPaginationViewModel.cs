namespace FundTrack.Infrastructure.ViewModel
{
    public class FilterPaginationViewModel
    {
        /// <summary>
        /// Gets or sets the filter options.
        /// </summary>
        /// <value>
        /// The filter options.
        /// </value>
        public FilterRequstedViewModel filterOptions { get; set; }

        /// <summary>
        /// Gets or sets the current page.
        /// </summary>
        /// <value>
        /// The current page.
        /// </value>
        public int CurrentPage { get; set; }

        /// <summary>
        /// Gets or sets the items per page.
        /// </summary>
        /// <value>
        /// The items per page.
        /// </value>
        public int ItemsPerPage { get; set; }
    }
}
