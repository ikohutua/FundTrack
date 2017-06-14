using System.Collections.Generic;

namespace FundTrack.DAL.Entities
{
    /// <summary>
    /// Tag entity
    /// </summary>
    public class Tag
    {
        /// <summary>
        /// Id of Tag
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of Tag
        /// </summary>
        public string TagName { get; set; }

        /// <summary>
        /// TagFinOp navigation property
        /// </summary>
        public virtual ICollection<TagFinOp> TagFinOps { get; set; }
    }
}
