using System;

namespace FundTrack.Infrastructure.ViewModel
{
    public class ImportDetailPrivatViewModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int id { get; set; }

        /// <summary>
        /// Gets or sets the card.
        /// </summary>
        /// <value>
        /// The card.
        /// </value>
        public string card { get; set; }

        /// <summary>
        /// Gets or sets the trandate.
        /// </summary>
        /// <value>
        /// The trandate.
        /// </value>
        public DateTime trandate { get; set; }

        /// <summary>
        /// Gets or sets the amount.
        /// </summary>
        /// <value>
        /// The amount.
        /// </value>
        public string amount { get; set; }

        /// <summary>
        /// Gets or sets the application code.
        /// </summary>
        /// <value>
        /// The application code.
        /// </value>
        public string appCode { get; set; }

        /// <summary>
        /// Gets or sets the card amount.
        /// </summary>
        /// <value>
        /// The card amount.
        /// </value>
        public string cardAmount { get; set; }

        /// <summary>
        /// Gets or sets the rest.
        /// </summary>
        /// <value>
        /// The rest.
        /// </value>
        public string rest { get; set; }

        /// <summary>
        /// Gets or sets the terminal.
        /// </summary>
        /// <value>
        /// The terminal.
        /// </value>
        public string terminal { get; set; }

        /// <summary>
        /// Gets or sets the description.
        /// </summary>
        /// <value>
        /// The description.
        /// </value>
        public string description { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether this instance is looked.
        /// </summary>
        /// <value>
        ///   <c>true</c> if this instance is looked; otherwise, <c>false</c>.
        /// </value>
        public bool isLooked { get; set; }
    }
}
