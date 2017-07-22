using FundTrack.Infrastructure.Attributes;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace FundTrack.Infrastructure.ViewModel
{
    /// <summary>
    /// Requested item view model class
    /// </summary>
    public class RequestedItemViewModel
    {
        /// <summary>
        ///Gets or sets id of requested item
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets name of requested item
        /// </summary>
        [Required]
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets goods category
        /// </summary>
        public string GoodsCategory { get; set; }

        /// <summary>
        /// Gets or sets status of goods categoty
        /// </summary>
        public string Status { get; set; }

        /// <summary>
        /// Gets or sets description of goods category
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets goods category id
        /// </summary>
        [MinValue(1)]
        public int GoodsCategoryId { get; set; }

        /// <summary>
        /// Gets or sets organization id
        /// </summary>
        //[MinValue(1)]
        public int OrganizationId { get; set; }

        /// <summary>
        /// Gets or sets error message 
        /// </summary>
        public string ErrorMessage { get; set; }

        /// <summary>
        /// Gets or seys goods type id
        /// </summary>
        [MinValue(1)]
        public int GoodsTypeId { get; set; }

        /// <summary>
        /// Gets or sets list of images
        /// </summary>
        public IEnumerable<RequestedImageViewModel> Images { get; set; }
    }
}
