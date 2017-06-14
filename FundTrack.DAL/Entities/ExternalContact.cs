namespace FundTrack.DAL.Entities
{
    /// <summary>
    /// ExternalContact entity
    /// </summary>
    public class ExternalContact
    {
        /// <summary>
        /// Id of ExternalContact
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id of ExternalService
        /// </summary>
        public int ServiceId { get; set; }

        /// <summary>
        /// Id of User
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Service Login
        /// </summary>    
        public string ServiceLogin { get; set; }

        /// <summary>
        /// ExternalService navigation property
        /// </summary>
        public virtual ExternalService ExternalService { get; set; }

        /// <summary>
        /// User navigation property
        /// </summary>
        public virtual User User { get; set; }
    }
}
