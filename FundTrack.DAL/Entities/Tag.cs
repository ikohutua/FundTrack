using System.Collections.Generic;

namespace FundTrack.DAL.Entities
{
    /// <summary>
    /// Tag entity
    /// </summary>
    public class Tag
    {
        /// <summary>
        /// Gets or Sets Id of Tag
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets Name of Tag
        /// </summary>
        public string TagName { get; set; }

        /// <summary>
        /// Gets or Sets TagFinOp navigation property
        /// </summary>
        public virtual ICollection<TagFinOp> TagFinOps { get; set; }
    }
}
