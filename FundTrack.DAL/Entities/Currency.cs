using System.Collections.Generic;

namespace FundTrack.DAL.Entities
{
    /// <summary>
    /// Currency entity
    /// </summary>
    public class Currency
    {
        /// <summary>
        /// Id of Currency
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Currency Short Name
        /// </summary>
        public string ShortName { get; set; }

        /// <summary>
        /// Currency Full Name
        /// </summary>
        public string FullName { get; set; }

        public virtual ICollection<OrgAccount> OrgAccounts { get; set; }
    }
}
