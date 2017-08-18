using System;
using System.Collections.Generic;
using System.Text;

namespace FundTrack.Infrastructure.ViewModel.FinanceViewModels
{
    public class FinOpViewModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the bank import identifier.
        /// </summary>
        /// <value>
        /// The bank import identifier.
        /// </value>
        public int BankImportId { get; set; }

        /// <summary>
        /// Gets or sets the name of the target.
        /// </summary>
        /// <value>
        /// The name of the target.
        /// </value>
        public string TargetName { get; set; }

        /// <summary>
        /// Gets or sets the name of the acc from.
        /// </summary>
        /// <value>
        /// The name of the acc from.
        /// </value>
        public string AccFromName { get; set; }

        /// <summary>
        /// Gets or sets the name of the acc to.
        /// </summary>
        /// <value>
        /// The name of the acc to.
        /// </value>
        public string AccToName { get; set; }

        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        /// <value>
        /// The amount.
        /// </value>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or sets the absolute amount.
        /// </summary>
        /// <value>
        /// The absolute amount.
        /// </value>
        public decimal AbsoluteAmount { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the org identifier.
        /// </summary>
        /// <value>
        /// The org identifier.
        /// </value>
        public int OrgId { get; set; }
    }
}
