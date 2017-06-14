using System.Collections.Generic;

namespace FundTrack.DAL.Entities
{
    /// <summary>
    /// BankAccount entity
    /// </summary>
    public class BankAccount
    {
        /// <summary>
        /// Id of BankAccount
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id of Organization
        /// </summary>
        public int OrgId { get; set; }

        /// <summary>
        /// AccNumber of BankAccount
        /// </summary>
        public string AccNumber { get; set; }

        /// <summary>
        /// MFO of BankAccount
        /// </summary>
        public string MFO { get; set; }

        /// <summary>
        /// EDRPOU of BankAccount
        /// </summary>
        public string EDRPOU { get; set; }

        /// <summary>
        /// Name of the Bank
        /// </summary>
        public string BankName { get; set; }

        /// <summary>
        /// Organization navigation property
        /// </summary>
        public virtual Organization Organization { get; set; }

        /// <summary>
        /// OrgAccount navigation property
        /// </summary>
        public virtual ICollection<OrgAccount> OrgAccounts { get; set; }
    }
}
