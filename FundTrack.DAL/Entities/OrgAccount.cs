using System.Collections.Generic;

namespace FundTrack.DAL.Entities
{
    /// <summary>
    /// OrgAccount entity
    /// </summary>
    public class OrgAccount
    {
        /// <summary>
        /// Id of OrgAccount
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id of Organization
        /// </summary>
        public int OrgId { get; set; }

        /// <summary>
        /// Id of BankAccount
        /// </summary>
        public int? BankAccId { get; set; }

        /// <summary>
        /// Id of Currency
        /// </summary>
        public int CurrencyId { get; set; }

        /// <summary>
        /// Name of OrgAccount
        /// </summary>
        public string OrgAccountName { get; set; }

        /// <summary>
        /// Type of OrgAccount
        /// </summary>
        public string AccountType { get; set; }

        /// <summary>
        /// Description of OrgAccount
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// CurrentBalance on OrgAccount
        /// </summary>
        public decimal CurrentBalance { get; set; }

        /// <summary>
        /// Organization navigation property
        /// </summary>
        public virtual Organization Organization { get; set; }

        /// <summary>
        /// BankAccount navigation property
        /// </summary>
        public virtual BankAccount BankAccount { get; set; }

        /// <summary>
        /// Currency navigation property
        /// </summary>
        public virtual Currency Currency { get; set; }

        /// <summary>
        /// Balance navigation property
        /// </summary>
        public virtual ICollection<Balance> Balances { get; set; }

        /// <summary>
        /// FinOpFrom navigation property
        /// </summary>
        public virtual ICollection<FinOp> FinOpsFrom { get; set; }

        /// <summary>
        /// FinOpTo navigation property
        /// </summary>
        public virtual ICollection<FinOp> FinOpsTo { get; set; }
    }
}
