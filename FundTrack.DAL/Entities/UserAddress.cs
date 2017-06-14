namespace FundTrack.DAL.Entities
{
    /// <summary>
    /// UserAddress entity
    /// </summary>
    public class UserAddress
    {
        /// <summary>
        /// Id of UserAddress
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id of User
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Id of Address
        /// </summary>
        public int AddressId { get; set; }

        /// <summary>
        /// User navigation property
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// Address navigation property
        /// </summary>
        public virtual Address Address { get; set; }
    }
}
