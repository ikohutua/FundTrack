using System.Collections.Generic;

namespace FundTrack.Infrastructure.ViewModel
{
    /// <summary>
    /// GoodsTypeView model class
    /// </summary>
    public class GoodsTypeViewModel
    {
        /// <summary>
        /// Gets or sets id of goods type view model
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Gets or sets name of goods type view model
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets list of type categories
        /// </summary>
        public IEnumerable<GoodsCategoryViewModel> TypeCategories { get; set; }
    }
}
