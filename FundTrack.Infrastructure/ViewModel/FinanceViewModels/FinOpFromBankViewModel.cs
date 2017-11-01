using System;

namespace FundTrack.Infrastructure.ViewModel.FinanceViewModels
{
    public class FinOpFromBankViewModel
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
        public int? TargetId { get; set; }

        /// <summary>
        /// Gets or sets the name of the acc from.
        /// </summary>
        /// <value>
        /// The name of the acc from.
        /// </value>
        public int? CardFromId { get; set; }

        /// <summary>
        /// Gets or sets the name of the acc to.
        /// </summary>
        /// <value>
        /// The name of the acc to.
        /// </value>
        public int? CardToId { get; set; }

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

        /// <summary>
        /// Gets or sets the fin op type.
        /// </summary>
        /// <value>
        /// The fin op type identifier.
        /// </value>
        public int FinOpType { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the fin op date.
        /// </summary>
        /// <value>
        /// The fin op date.
        /// </value>
        public DateTime FinOpDate { get; set; }
    }
}