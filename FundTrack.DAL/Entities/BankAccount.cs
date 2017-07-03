using System.Collections.Generic;

namespace FundTrack.DAL.Entities
{
    /// <summary>
    /// BankAccount entity
    /// </summary>
    public class BankAccount
    {
        /// <summary>
        /// Gets or Sets Id of BankAccount
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets Id of Organization
        /// </summary>
        public int OrgId { get; set; }

        /// <summary>
        /// Gets or Sets AccNumber of BankAccount
        /// </summary>
        public string AccNumber { get; set; }

        /// <summary>
        /// Gets or Sets MFO of BankAccount
        /// </summary>
        public string MFO { get; set; }

        /// <summary>
        /// Gets or Sets EDRPOU of BankAccount
        /// </summary>
        public string EDRPOU { get; set; }

        /// <summary>
        /// Gets or Sets Name of the Bank
        /// </summary>
        public string BankName { get; set; }

        /// <summary>
        /// Gets or Sets Organization navigation property
        /// </summary>
        public virtual Organization Organization { get; set; }

        /// <summary>
        /// Gets or Sets OrgAccount navigation property
        /// </summary>
        public virtual ICollection<OrgAccount> OrgAccounts { get; set; }
    }
}
