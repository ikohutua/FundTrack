using System;
using System.Collections.Generic;
using System.Text;

namespace FundTrack.Infrastructure.ViewModel
{
    /// <summary>
    /// View Model which contains filter for requested items
    /// </summary>
    public class FilterRequstedViewModel
    {
        /// <summary>
        /// Gets or sets the organization filter.
        /// </summary>
        /// <value>
        /// The organization filter.
        /// </value>
        public string OrganizationFilter { get; set; }

        /// <summary>
        /// Gets or sets the category filter.
        /// </summary>
        /// <value>
        /// The category filter.
        /// </value>
        public string CategoryFilter { get; set; }

        /// <summary>
        /// Gets or sets the type filter.
        /// </summary>
        /// <value>
        /// The type filter.
        /// </value>
        public string TypeFilter { get; set; }

        /// <summary>
        /// Gets or sets the status filter.
        /// </summary>
        /// <value>
        /// The status filter.
        /// </value>
        public string StatusFilter { get; set; }
    }
}
