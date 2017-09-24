using System;
using System.Collections.Generic;
using System.Text;

namespace FundTrack.Infrastructure.ViewModel.FinanceViewModels
{
    public class FinOpListViewModel
    {
        /// <summary>
        /// Gets or sets the date.
        /// </summary>
        /// <value>
        /// The date.
        /// </value>
        public DateTime Date { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        /// <value>
        /// The amount.
        /// </value>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the target.
        /// </summary>
        /// <value>
        /// The Target.
        /// </value>
        public string Target { get; set; }

        /// <summary>
        /// Gets or sets the short name of the currency.
        /// </summary>
        /// <value>
        /// The short name of the currency.
        /// </value>
        public string CurrencyShortName { get; set; }

        /// <summary>
        /// Gets or sets the full name of the currency.
        /// </summary>
        /// <value>
        /// The full name of the currency.
        /// </value>
        public string CurrencyFullName { get; set; }

        /// <summary>
        /// Gets or sets the error.
        /// </summary>
        /// <value>
        /// The error.
        /// </value>
        public string Error { get; set; }

    }
}
