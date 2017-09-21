using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FundTrack.DAL.Entities
{
    /// <summary>
    /// Target entity
    /// </summary>
    public class Target
    {
        /// <summary>
        /// Gets or Sets Id of Target
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets Name of Target
        /// </summary>
        public string TargetName { get; set; }

        public int OrganizationId { get; set; }

        public int? ParentTargetId { get; set; }

        /// <summary>
        /// Gets or Sets FinOp navigation property
        /// </summary>
        public virtual ICollection<FinOp> FinOp { get; set; }

        public virtual ICollection<Donation> Donates { get; set; }

        public virtual ICollection<OrgAccount> OrgAccounts { get; set; }

        public  virtual Organization Organizations { get; set; }

        public virtual Target ParentTarget { get; set; }
    }
}
