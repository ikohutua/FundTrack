using System;
using System.Collections.Generic;

namespace FundTrack.DAL.Entities
{
    /// <summary>
    /// FinOp entity
    /// </summary>
    public class FinOp
    {
        /// <summary>
        /// Id of FinOp
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Id of Target
        /// </summary>
        public int TargetId { get; set; }

        /// <summary>
        /// Id of Account that made the FinOp
        /// </summary>
        public int? AccFromId { get; set; }

        /// <summary>
        /// Id of Account that reseived the FinOp
        /// </summary>
        public int? AccToId { get; set; }

        /// <summary>
        /// Id of User
        /// </summary>
        public int? UserId { get; set; }

        /// <summary>
        /// Amount of money in FinOp 
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Description of FinOp
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Date of FinOp
        /// </summary>
        public DateTime FinOpDate { get; set; }

        /// <summary>
        /// Target navigation property
        /// </summary>
        public virtual Target Target { get; set; }

        /// <summary>
        /// OrgAccountFrom navigation property
        /// </summary>
        public virtual OrgAccount OrgAccountFrom { get; set; }

        /// <summary>
        /// OrgAccountTo navigation property
        /// </summary>
        public virtual OrgAccount OrgAccountTo { get; set; }

        /// <summary>
        /// User navigation property
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// TagFinOp navigation property
        /// </summary>
        public virtual ICollection<TagFinOp> TagFinOps { get; set; }
    }
}
