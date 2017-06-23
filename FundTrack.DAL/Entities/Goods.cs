using System.Collections.Generic;

namespace FundTrack.DAL.Entities
{
    public class Goods
    {
        /// <summary>
        /// Gets or sets the identifier.
        /// </summary>
        /// <value>
        /// The identifier.
        /// </value>
        public int Id { get; set; }

        /// <summary>
        /// Gets or sets the goods type identifier.
        /// </summary>
        /// <value>
        /// The goods type identifier.
        /// </value>
        public int GoodsTypeId { get; set; }

        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the type of the goods.
        /// </summary>
        /// <value>
        /// The type of the goods.
        /// </value>
        public virtual GoodsType GoodsType { get; set; }

        /// <summary>
        /// Gets or sets the offered items.
        /// </summary>
        /// <value>
        /// The offered items.
        /// </value>
        public virtual ICollection<OfferedItem> OfferedItems { get; set; }

        /// <summary>
        /// Gets or sets the requested items.
        /// </summary>
        /// <value>
        /// The requested items.
        /// </value>
        public virtual ICollection<RequestedItem> RequestedItems { get; set; }
    }
}
