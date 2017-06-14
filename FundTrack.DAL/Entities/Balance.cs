using System;

namespace FundTrack.DAL.Entities
{
    /// <summary>
    /// Balance entity
    /// </summary>
    public class Balance
    {
        /// <summary>
        /// Id of Balance
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id of OrgAccount
        /// </summary>
        public int OrgAccountId { get; set; }

        /// <summary>
        /// Date of Balance
        /// </summary>
        public DateTime BalanceDate { get; set; }

        /// <summary>
        /// Amount of money on the balance
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// OrgAccount navigation property
        /// </summary>
        public virtual OrgAccount OrgAccount { get; set; }
    }
}
