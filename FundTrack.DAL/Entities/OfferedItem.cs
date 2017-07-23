using FundTrack.Infrastructure.ViewModel;
using System;
using System.Collections.Generic;

namespace FundTrack.DAL.Entities
{
    public class OfferedItem
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the goods category identifier.
        /// </summary>
        /// <value>
        /// The goods category identifier.
        /// </value>
        public int GoodsCategoryId { get; set; }

        /// <summary>
        /// Gets or sets the user identifier.
        /// </summary>
        /// <value>
        /// The user identifier.
        /// </value>
        public int UserId { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is actual.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is actual; otherwise, <c>false</c>.
        /// </value>
        public int StatusId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the goods category.
        /// </summary>
        /// <value>
        /// The goods category.
        /// </value>
        public virtual GoodsCategory GoodsCategory { get; set; }

        /// <summary>
        /// Gets or sets the status.
        /// </summary>
        /// <value>
        /// The status.
        /// </value>
        public virtual Status Status { get; set; }

        /// <summary>
        /// Gets or sets the user.
        /// </summary>
        /// <value>
        /// The user.
        /// </value>
        public virtual User User { get; set; }

        /// <summary>
        /// EventImages navigation property
        /// </summary>
        public virtual ICollection<OfferedItemImage> OfferedItemImages { get; set; }

        /// <summary>
        /// EventImages navigation property
        /// </summary>
        public virtual ICollection<OrganizationResponse> OrganizationResponses { get; set; }
        /// <summary>
        /// UserResponse navigation property
        /// </summary>
        public virtual UserResponse UserResponse { get; set; }

        public static implicit operator OfferedItem(OfferedItemViewModel model)
        {
            return new OfferedItem
            {
                Description = model.Description,
                Name = model.Name,
                UserId = model.UserId
            };
        }
    }
}
