namespace FundTrack.DAL.Entities
{
    /// <summary>
    /// ExternalContact entity
    /// </summary>
    public class ExternalContact
    {
        /// <summary>
        /// Gets or Sets Id of ExternalContact
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets Id of ExternalService
        /// </summary>
        public int ServiceId { get; set; }

        /// <summary>
        /// Gets or Sets Id of User
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or Sets Service Login
        /// </summary>    
        public string ServiceLogin { get; set; }

        /// <summary>
        /// Gets or Sets ExternalService navigation property
        /// </summary>
        public virtual ExternalService ExternalService { get; set; }

        /// <summary>
        /// Gets or Sets User navigation property
        /// </summary>
        public virtual User User { get; set; }
    }
}
