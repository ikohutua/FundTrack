using System;
using System.Collections.Generic;
using System.Text;

namespace FundTrack.DAL.Entities
{
    public class EventImage
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the event identifier.
        /// </summary>
        /// <value>
        /// The organization identifier.
        /// </value>
        public int EventId { get; set; }

        /// <summary>
        /// Gets or sets the image URL.
        /// </summary>
        /// <value>
        /// The image URL.
        /// </value>
        public string ImageUrl { get; set; }
      
        /// <summary>
        /// Gets or sets if the image is main image of event.
        /// </summary>
        /// <value>
        /// The image URL.
        /// </value>
        public bool IsMain { get; set; }

        /// <summary>
        /// Event navigation property
        /// </summary>
        public virtual Event Event { get; set; }
    }
}
