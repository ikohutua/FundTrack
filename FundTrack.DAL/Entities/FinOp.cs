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
        /// Gets or Sets Id of FinOp
        /// </summary>
        public int Id { get; set; } 

        /// <summary>
        /// Gets or Sets Id of Target
        /// </summary>
        public int? TargetId { get; set; }

        /// <summary>
        /// Gets or Sets Id of Account that made the FinOp
        /// </summary>
        public int? AccFromId { get; set; }

        /// <summary>
        /// Gets or Sets Id of Account that reseived the FinOp
        /// </summary>
        public int? AccToId { get; set; }

        /// <summary>
        /// Gets or Sets Id of User
        /// </summary>
        public int? UserId { get; set; }
        public int? DonationId { get; set; }

        /// <summary>
        /// Gets or Sets Amount of money in FinOp 
        /// </summary>
        public decimal Amount { get; set; }

        /// <summary>
        /// Gets or Sets Description of FinOp
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or Sets Date of FinOp
        /// </summary>
        public DateTime FinOpDate { get; set; }

        /// <summary>
        /// Gets or Sets Target navigation property
        /// </summary>
        public virtual Target Target { get; set; }

        /// <summary>
        /// Gets or Sets OrgAccountFrom navigation property
        /// </summary>
        public virtual OrgAccount OrgAccountFrom { get; set; }

        /// <summary>
        /// Gets or Sets OrgAccountTo navigation property
        /// </summary>
        public virtual OrgAccount OrgAccountTo { get; set; }

        /// <summary>
        /// Gets or Sets User navigation property
        /// </summary>
        public virtual User User { get; set; }

        /// <summary>
        /// Gets or Sets TagFinOp navigation property
        /// </summary>
        public virtual ICollection<TagFinOp> TagFinOps { get; set; }
        public virtual Donation Donation { get; set; }

       
        /// <summary>
        /// Gets or Sets table for binding
        /// </summary>
        public virtual ICollection<FinOpImage> FinOpImage { get; set; }
    }
}
