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
        /// Gets or sets the offer identifier.
        /// </summary>
        /// <value>
        /// The offer identifier.
        /// </value>
        public int OfferId { get; set; }

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
        /// Gets or sets the image URL.
        /// </summary>
        /// <value>
        /// The image URL.
        /// </value>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is actual.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is actual; otherwise, <c>false</c>.
        /// </value>
        public bool IsActual { get; set; }

        /// <summary>
        /// Gets or sets the goods category.
        /// </summary>
        /// <value>
        /// The goods category.
        /// </value>
        public virtual GoodsCategory GoodsCategory { get; set; }

        /// <summary>
        /// Gets or sets the offer.
        /// </summary>
        /// <value>
        /// The offer.
        /// </value>
        public virtual Offer Offer { get; set; }
    }
}
