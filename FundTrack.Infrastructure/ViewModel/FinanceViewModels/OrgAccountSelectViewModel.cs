using System;
using System.Collections.Generic;
using System.Text;

namespace FundTrack.Infrastructure.ViewModel.FinanceViewModels
{
    /// <summary>
    /// model for display orgacoount when bank-import convert to finOp
    /// </summary>
    public class OrgAccountSelectViewModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name of the org account.
        /// </summary>
        /// <value>
        /// The name of the org account.
        /// </value>
        public string OrgAccountName { get; set; }

        /// <summary>
        /// Gets or sets the org account number.
        /// </summary>
        /// <value>
        /// The org account number.
        /// </value>
        public string OrgAccountNumber { get; set; }

        public int? TargetId { get; set; }
    }
}
