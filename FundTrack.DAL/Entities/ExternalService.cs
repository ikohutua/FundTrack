using System.Collections.Generic;

namespace FundTrack.DAL.Entities
{
    /// <summary>
    /// ExternalService entity
    /// </summary>
    public class ExternalService
    {
        /// <summary>
        /// Id of ExternalService
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of ExternalService
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// ExtContact navigation property
        /// </summary>
        public virtual ICollection<ExternalContact> ExtContacts { get; set; }
    }
}
