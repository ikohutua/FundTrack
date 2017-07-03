namespace FundTrack.DAL.Entities
{
    /// <summary>
    /// UserAddress entity
    /// </summary>
    public class UserAddress
    {
        /// <summary>
        /// Gets or Sets Id of UserAddress
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets Id of User
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or Sets Id of Address
        /// </summary>
        public int AddressId { get; set; }

        /// <summary>
        /// Gets or Sets User navigation property
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// Gets or Sets Address navigation property
        /// </summary>
        public virtual Address Address { get; set; }
    }
}
