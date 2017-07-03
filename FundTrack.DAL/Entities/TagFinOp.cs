namespace FundTrack.DAL.Entities
{
    /// <summary>
    /// TagFinOp entity
    /// </summary>
    public class TagFinOp
    {
        /// <summary>
        /// Gets or Sets Id of TagFinOp
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or Sets Id of Tag
        /// </summary>
        public int TagId { get; set; }

        /// <summary>
        /// Gets or Sets Id of FinOp
        /// </summary>
        public int FinOpId { get; set; }

        /// <summary>
        /// Gets or Sets Tag navigation property
        /// </summary>
        public virtual Tag Tag { get; set; }

        /// <summary>
        /// Gets or Sets FinOp navigation property
        /// </summary>
        public virtual FinOp FinOp { get; set; }
    }
}
