using System;

namespace FundTrack.DAL.Entities
{
    /// <summary>
    /// Balance entity
    /// </summary>
    public class Balance
    {
        /// <summary>
        /// Gets or Sets Id of Balance
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets Id of OrgAccount
        /// </summary>
        public int OrgAccountId { get; set; }

        /// <summary>
        /// Gets or Sets Date of Balance
        /// </summary>
        public DateTime BalanceDate { get; set; }

        /// <summary>
        /// Gets or Sets Amount of money on the balance
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or Sets OrgAccount navigation property
        /// </summary>
        public virtual OrgAccount OrgAccount { get; set; }
    }
}
