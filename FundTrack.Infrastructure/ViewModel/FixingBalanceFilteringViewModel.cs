using FundTrack.Infrastructure.ViewModel.FinanceViewModels;
using System;
using System.Collections.Generic;
using System.Text;

namespace FundTrack.Infrastructure.ViewModel
{
    public class FixingBalanceFilteringViewModel
    {
        /// <summary>
        /// Gets or sets the date of last fin op.
        /// </summary>
        /// <value>
        /// Date.
        /// </value>
        public DateTime? FirstDayForFixingBalance { get; set; }

        /// <summary>
        /// Gets or sets the server date.
        /// </summary>
        /// <value>
        /// Date.
        /// </value>
        public DateTime? ServerDate { get; set; }

        /// <summary>
        /// Gets or sets the info about last fixing for current account.
        /// </summary>
        /// <value>
        /// Date.
        /// </value>
        public BalanceViewModel LastFixing { get; set; }

        /// <summary>
        /// Gets or sets info about fin ops after last fixing
        /// </summary>
        /// <value>
        /// True or false.
        /// </value>
        public bool HasFinOpsAfterLastFixing { get; set; }
    }
}
