using System.ComponentModel.DataAnnotations;

namespace FundTrack.DAL.Entities
{
    /// <summary>
    /// Target entity
    /// </summary>
    public class Target
    {
        /// <summary>
        /// Gets or Sets Id of Target
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets Name of Target
        /// </summary>
        public string TargetName { get; set; }

        /// <summary>
        /// Gets or Sets FinOp navigation property
        /// </summary>
        public virtual FinOp FinOp { get; set; }
    }
}
