using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundTrack.AutoImportService.ViewModels
{
    public class AutoImportIntervalViewModel
    {
        /// <summary>
        /// Gets or sets the id of interval.
        /// </summary>
        /// <value>
        /// The id
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the interval.
        /// </summary>
        /// <value>
        /// Interval in milliseconds.
        /// </value>
        public long Interval { get; set; }

        /// <summary>
        /// Gets or sets the id of organization.
        /// </summary>
        /// <value>
        /// Organization id.
        /// </value>
        public int OrganizationId { get; set; }
    }
}
