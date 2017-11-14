namespace FundTrack.Infrastructure.ViewModel
{
    /// <summary>
    /// Image view model
    /// </summary>
    public sealed  class ImageViewModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }
        /// <summary>
        /// Gets or sets the image URL.
        /// </summary>
        /// <value>
        /// The image URL.
        /// </value>
        public string ImageUrl { get; set; }

        /// <summary>
        /// Gets or sets base64code of image.
        /// </summary>
        /// <value>
        /// The image base64code.
        /// </value>
        public string Base64Data { get; set; }

        /// <summary>
        /// Gets or sets is main prorepty.
        /// </summary>
        /// <value>
        /// The image property.
        /// </value>
        public bool IsMain { get; set; }

        /// <summary>
        /// Gets or sets the image extension.
        /// </summary>
        /// <value>
        /// The image extension.
        /// </value>
        public string imageExtension { get; set; }
    }
}
