namespace FundTrack.DAL.Entities
{
    /// <summary>
    /// OrgAddress entity
    /// </summary>
    public class OrgAddress
    {
        /// <summary>
        /// Id of OrgAddress
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id of Organization
        /// </summary>
        public int OrgId { get; set; }

        /// <summary>
        /// Id of Address
        /// </summary>
        public int AddressId { get; set; }

        /// <summary>
        /// Organization navigation property
        /// </summary>
        public virtual Organization Organization { get; set; }

        /// <summary>
        /// Address navigation property
        /// </summary>
        public virtual Address Address { get; set; }
    }
}
