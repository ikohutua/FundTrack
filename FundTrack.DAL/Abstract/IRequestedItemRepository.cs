using FundTrack.DAL.Entities;
using System.Collections.Generic;

namespace FundTrack.DAL.Abstract
{
    /// <summary>
    /// Interface for work with Requested items
    /// </summary>
    public interface IRequestedItemRepository : IRepository<RequestedItem>
    {
        /// <summary>
        /// Gets the requested item status.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Status</returns>
        Status GetRequestedItemStatus(int id);

        /// <summary>
        /// Gets the requested item with virtual field.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Requested Item</returns>
        RequestedItem GetRequestedItemsWithVirtualFields(int id);
        IEnumerable<RequestedItem> GetOrganizationRequestedItems(int organizationId);
        IEnumerable<GoodsType> GetAllGoodTypes();
        IEnumerable<RequestedItemImage> GetAllImages(int requestedItemId);
    }
}
