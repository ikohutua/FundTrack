using System;

namespace FundTrack.Infrastructure.ViewModel.RequestedItemModel
{
    public class ShowAllRequestedItemModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the goods category.
        /// </summary>
        /// <value>
        /// The goods category identifier.
        /// </value>
        public string GoodsCategory { get; set; }

        /// <summary>
        /// Gets or sets a value that describe the GoodsType of the RequestedItem.
        /// </summary>
        /// <value>
        ///  The goods type identifier.
        /// </value>
        public string GoodsType { get; set; }

        /// <summary>
        /// Gets or sets a value that describe the CreateDate of the RequestedItem.
        /// </summary>
        /// <value>
        ///  The goods CreateDate identifier.
        /// </value>
        public DateTime CreateDate { get; set; }

        /// <summary>
        /// Gets or sets the organization.
        /// </summary>
        /// <value>
        /// The organization identifier.
        /// </value>
        public string Organization { get; set; }

        /// <summary>
        /// Gets or sets a value that describe the status of the RequestedItem.
        /// </summary>
        /// <value>
        ///   >.
        /// </value>
        public string Status { get; set; }

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
    }
}