using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundTrack.Infrastructure.ViewModel
{
    /// <summary>
    /// GoodsCategoryViewModel class
    /// </summary>
    public class GoodsCategoryViewModel
    {
        /// <summary>
        /// Gets or sets id of goods category
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets goods type id
        /// </summary>
        public int GoodsTypeId { get; set; }

        /// <summary>
        /// Gets or sets goods category name
        /// </summary>
        public string Name { get; set; }
    }
}
