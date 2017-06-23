using System.Collections.Generic;

namespace FundTrack.DAL.Entities
{
    public class BannedUser
    {
        /// <summary>
        /// Gets or sets the banned identifier
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets user id
        /// </summary>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets the banned user description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Users navigation property
        /// </summary>
        public virtual User User { get; set; }
    }
}
