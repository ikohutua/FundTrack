namespace FundTrack.DAL.Entities
{
    /// <summary>
    /// TagFinOp entity
    /// </summary>
    public class TagFinOp
    {
        /// <summary>
        /// Id of TagFinOp
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id of Tag
        /// </summary>
        public int TagId { get; set; }

        /// <summary>
        /// Id of FinOp
        /// </summary>
        public int FinOpId { get; set; }

        /// <summary>
        /// Tag navigation property
        /// </summary>
        public virtual Tag Tag { get; set; }

        /// <summary>
        /// FinOp navigation property
        /// </summary>
        public virtual FinOp FinOp { get; set; }
    }
}
