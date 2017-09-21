using System;
using System.Collections.Generic;
using System.Text;

namespace FundTrack.DAL.Entities
{
    public class FinOpImage
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets FinOp identifier.
        /// </summary>
        /// <value>
        /// The organization identifier.
        /// </value>
        public int FinOpId { get; set; }

        /// <summary>
        /// Gets or sets FinOp identifier.
        /// </summary>
        /// <value>
        /// The images identifier.
        /// </value>
        public virtual FinOp FinOp { get; set; }

        /// <summary>
        /// Gets or sets the image URL.
        /// </summary>
        /// <value>
        /// The image URL.
        /// </value>
        public string ImageUrl { get; set; }
    }
}
