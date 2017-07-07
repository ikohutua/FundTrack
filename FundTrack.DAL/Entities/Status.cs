using System;
using System.Collections.Generic;
using System.Text;

namespace FundTrack.DAL.Entities
{
    public class Status
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the status name for request or offer - новий, активний, завершено
        /// </summary>
        /// <value>
        /// The status name
        /// </value>
        public string StatusName { get; set; }

        /// <summary>
        /// Gets or sets the offered items.
        /// </summary>
        /// <value>
        /// The offered items.
        /// </value>
        public virtual ICollection<OfferedItem> OfferedItems { get; set; }

        /// <summary>
        /// Gets or sets the requested items.
        /// </summary>
        /// <value>
        /// The requested items.
        /// </value>
        public virtual ICollection<RequestedItem> RequestedItems { get; set; }
    }
}

