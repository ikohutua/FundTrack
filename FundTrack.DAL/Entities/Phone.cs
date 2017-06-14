namespace FundTrack.DAL.Entities
{
    /// <summary>
    /// Phone entity
    /// </summary>
    public class Phone
    {
        /// <summary>
        /// Id of Phone
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id of User
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Phone Number
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Phone Type
        /// </summary>     
        public string PhoneType { get; set; }
       
        /// <summary>
        /// User navigation property
        /// </summary>
        public virtual User User { get; set; }
    }
}
