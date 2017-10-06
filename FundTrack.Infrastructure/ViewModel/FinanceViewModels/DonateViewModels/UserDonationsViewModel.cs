using System;

namespace FundTrack.Infrastructure.ViewModel.FinanceViewModels.DonateViewModels
{
    public class UserDonationsViewModel
    {
        /// <summary>
        /// Gets or sets the donation id.
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the organization of donation.
        /// </summary>
        public string Organization { get; set; }
        /// <summary>
        /// Gets or sets the amount of donation.
        /// </summary>
        public double Amount { get; set; }
        /// <summary>
        /// Gets or sets the donation date.
        /// </summary>
        public DateTime Date { get; set; }
        /// <summary>
        /// Gets or sets the target donation.
        /// </summary>
        public string Target { get; set; }
        /// <summary>
        /// Gets or sets the donation description.
        /// </summary>
        public string Description { get; set; }
    }
}
