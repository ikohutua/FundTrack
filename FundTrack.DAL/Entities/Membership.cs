namespace FundTrack.DAL.Entities
{
    /// <summary>
    /// Membership entity
    /// </summary>
    public class Membership
    {
        /// <summary>
        /// Gets or Sets Id of Membership
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets Id of User
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or Sets Id of Role
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// Gets or Sets Id of Organization
        /// </summary>
        public int OrgId { get; set; }

        /// <summary>
        /// Gets or Sets User navigation property
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// Gets or Sets Role navigation property
        /// </summary>
        public virtual Role Role { get; set; }

        /// <summary>
        /// Gets or Sets Organization navigation property
        /// </summary>
        public virtual Organization Organization { get; set; }
    }
}
