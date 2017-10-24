using System;

namespace FundTrack.Infrastructure.ViewModel.FinanceViewModels
{
    public class BankImportSearchViewModel
    {
        /// <summary>
        /// Gets or sets the data from.
        /// </summary>
        /// <value>
        /// The data from.
        /// </value>
        public DateTime? DataFrom { get; set; }

        /// <summary>
        /// Gets or sets the data to.
        /// </summary>
        /// <value>
        /// The data to.
        /// </value>
        public DateTime? DataTo { get; set; }

        /// <summary>
        /// Gets or sets the card.
        /// </summary>
        /// <value>
        /// The card.
        /// </value>
        public string Card { get; set; }

        /// <summary>
        /// Gets or sets the state.
        /// </summary>
        /// <value>
        /// The state.
        /// </value>
        public bool? State { get; set; }
    }
}
