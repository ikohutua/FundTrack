namespace FundTrack.DAL.Entities
{
    public class RequestedItem
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
        public int GoodsId { get; set; }

        /// <summary>
        /// Gets or sets the request identifier.
        /// </summary>
        /// <value>
        /// The request identifier.
        /// </value>
        public int RequestId { get; set; }

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
        public virtual Goods Goods { get; set; }

        /// <summary>
        /// Gets or sets the request.
        /// </summary>
        /// <value>
        /// The request.
        /// </value>
        public virtual Request Request { get; set; }
    }
}
