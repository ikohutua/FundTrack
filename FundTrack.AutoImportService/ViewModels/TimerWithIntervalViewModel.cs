using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FundTrack.AutoImportService.ViewModels
{
    public class TimerWithIntervalViewModel
    {
        /// <summary>
        /// Gets or sets the timer.
        /// </summary>
        /// <value>
        /// The timer.
        /// </value>
        public Timer Timer { get; set; }

        /// <summary>
        /// Gets or sets the interval view model.
        /// </summary>
        /// <value>
        /// The information about interval.
        /// </value>
        public AutoImportIntervalViewModel IntervalViewModel { get; set; }
    }
}
