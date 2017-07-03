using System.Collections.Generic;

namespace FundTrack.DAL.Entities
{
    /// <summary>
    /// Role entity
    /// </summary>
    public class Role
    {
        /// <summary>
        /// Gets or Sets Id of Role
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets Role Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets Membership navigation property
        /// </summary>
        public virtual ICollection<Membership> Memberships { get; set; }
    }
}
