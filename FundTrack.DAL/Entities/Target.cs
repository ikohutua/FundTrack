using System.ComponentModel.DataAnnotations;

namespace FundTrack.DAL.Entities
{
    /// <summary>
    /// Target entity
    /// </summary>
    public class Target
    {
        /// <summary>
        /// Id of Target
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Name of Target
        /// </summary>
        public string TargetName { get; set; }

        /// <summary>
        /// FinOp navigation property
        /// </summary>
        public virtual FinOp FinOp { get; set; }
    }
}
