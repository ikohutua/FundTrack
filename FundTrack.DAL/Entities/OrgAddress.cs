namespace FundTrack.DAL.Entities
{
    /// <summary>
    /// OrgAddress entity
    /// </summary>
    public class OrgAddress
    {
        /// <summary>
        /// Gets or Sets Id of OrgAddress
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets Id of Organization
        /// </summary>
        public int OrgId { get; set; }

        /// <summary>
        /// Gets or Sets Id of Address
        /// </summary>
        public int AddressId { get; set; }

        /// <summary>
        /// Gets or Sets Organization navigation property
        /// </summary>
        public virtual Organization Organization { get; set; }

        /// <summary>
        /// Gets or Sets Address navigation property
        /// </summary>
        public virtual Address Address { get; set; }
    }
}
