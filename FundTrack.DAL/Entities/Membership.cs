namespace FundTrack.DAL.Entities
{
    /// <summary>
    /// Membership entity
    /// </summary>
    public class Membership
    {
        /// <summary>
        /// Id of Membership
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id of User
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Id of Role
        /// </summary>
        public int RoleId { get; set; }

        /// <summary>
        /// Id of Organization
        /// </summary>
        public int OrgId { get; set; }

        /// <summary>
        /// User navigation property
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// Role navigation property
        /// </summary>
        public virtual Role Role { get; set; }

        /// <summary>
        /// Organization navigation property
        /// </summary>
        public virtual Organization Organization { get; set; }
    }
}
