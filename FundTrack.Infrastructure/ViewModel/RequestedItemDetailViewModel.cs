using System;
using System.Collections.Generic;
using System.Text;

namespace FundTrack.Infrastructure.ViewModel
{
    /// <summary>
    /// Class which describe  RequestedItem 
    /// </summary>
    public class RequestedItemDetailViewModel
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the descriprion.
        /// </summary>
        /// <value>
        /// The descriprion.
        /// </value>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the goods category identifier.
        /// </summary>
        /// <value>
        /// The goods category identifier.
        /// </value>
        public int GoodsCategoryId { get; set; }

        /// <summary>
        /// Gets or sets the name of the goods category.
        /// </summary>
        /// <value>
        /// The name of the goods category.
        /// </value>
        public string GoodsCategoryName { get; set; }

        /// <summary>
        /// Gets or sets the goods type identifier.
        /// </summary>
        /// <value>
        /// The goods type identifier.
        /// </value>
        public int GoodsTypeId { get; set; }

        /// <summary>
        /// Gets or sets the name of the goods type.
        /// </summary>
        /// <value>
        /// The name of the goods type.
        /// </value>
        public string GoodsTypeName { get; set; }

        /// <summary>
        /// Gets or sets the status identifier.
        /// </summary>
        /// <value>
        /// The status identifier.
        /// </value>
        public int StatusId { get; set; }

        /// <summary>
        /// Gets or sets the name of the status.
        /// </summary>
        /// <value>
        /// The name of the status.
        /// </value>
        public string StatusName { get; set; }

        /// <summary>
        /// Gets or sets the organization identifier.
        /// </summary>
        /// <value>
        /// The organization identifier.
        /// </value>
        public int OrganizationId { get; set; }

        /// <summary>
        /// Gets or sets the name of the organization.
        /// </summary>
        /// <value>
        /// The name of the organization.
        /// </value>
        public string OrganizationName { get; set; }

        /// <summary>
        /// Gets or sets the images URL.
        /// </summary>
        /// <value>
        /// The images URL.
        /// </value>
        public List<string> ImagesUrl { get; set; }

        /// <summary>
        /// Gets or sets the main image URL.
        /// </summary>
        /// <value>
        /// The main image URL.
        /// </value>
        public string MainImageUrl { get; set; }

    }
}
