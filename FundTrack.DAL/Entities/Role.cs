using System.Collections.Generic;

namespace FundTrack.DAL.Entities
{
    /// <summary>
    /// Role entity
    /// </summary>
    public class Role
    {
        /// <summary>
        /// Id of Role
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Role Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Membership navigation property
        /// </summary>
        public virtual ICollection<Membership> Memberships { get; set; }
    }
}
