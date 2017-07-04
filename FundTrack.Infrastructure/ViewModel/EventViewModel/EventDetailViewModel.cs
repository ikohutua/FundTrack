using System;
using System.Collections.Generic;

namespace FundTrack.Infrastructure.ViewModel.EventViewModel
{
    /// <summary>
    /// Class which describe model of detail information about Event
    /// </summary>
    public sealed class EventDetailViewModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the organization identifier.
        /// </summary>
        /// <value>
        /// The organization identifier.
        /// </value>
        public int OrganizationId { get; set; }

        /// <summary>
        /// Gets or sets the organization name.
        /// </summary>
        /// <value>
        /// The organization name.
        /// </value>
        public string OrganizationName { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the create date.
        /// </summary>
        /// <value>
        /// The create date.
        /// </value>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Gets or sets the images URL.
        /// </summary>
        /// <value>
        /// The images URL.
        /// </value>
        public List<string> ImageUrl { get; set; }
    }
}
