using System.Collections.Generic;

namespace FundTrack.DAL.Entities
{
    /// <summary>
    /// ExternalService entity
    /// </summary>
    public class ExternalService
    {
        /// <summary>
        /// Gets or Sets Id of ExternalService
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets Name of ExternalService
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or Sets ExtContact navigation property
        /// </summary>
        public virtual ICollection<ExternalContact> ExtContacts { get; set; }
    }
}
