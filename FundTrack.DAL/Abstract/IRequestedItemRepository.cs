using FundTrack.DAL.Entities;
using FundTrack.Infrastructure.ViewModel;
using System.Collections.Generic;
using System.Linq;

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
        
        /// <summary>
        /// Gets requested item from database by organization id
        /// </summary>
        /// <param name="organizationId">Organization id</param>
        /// <returns>List of requested items</returns>
        IEnumerable<RequestedItem> GetOrganizationRequestedItems(int organizationId);

        /// <summary>
        /// Reads as queryable.
        /// </summary>
        /// <returns></returns>
        IQueryable<RequestedItem> ReadAsQueryable();

        /// <summary>
        /// Reads for pagination.
        /// </summary>
        /// <param name="CurrentPage">The current page.</param>
        /// <param name="ItemsPerage">The items perage.</param>
        /// <returns></returns>
        IQueryable<RequestedItem> ReadForPagination(int ItemsPerPage, int CurrentPage);


        /// <summary>
        /// Gets all goodstype
        /// </summary>
        /// <returns>List of goodstype</returns>
        IEnumerable<GoodsType> GetAllGoodTypes();

        /// <summary>
        /// Filters the requested item.
        /// </summary>
        /// <param name="filters">The filters.</param>
        /// <returns>List of requested item</returns>
        IQueryable<RequestedItem> FilterRequestedItem(FilterRequstedViewModel filters);

        /// <summary>
        /// Filters the requested item.
        /// </summary>
        /// <param name="filters">The filters.</param>
        /// <returns>List of requested item</returns>
        IQueryable<RequestedItem> FilterRequestedItem(FilterPaginationViewModel filters);

        /// <summary>
        /// Gets requested item per page
        /// </summary>
        /// <param name="organizationId">Organization id</param>
        /// <param name="currentPage">Current page number</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>List of requested items</returns>
        IEnumerable<RequestedItem> GetRequestedItemPerPageByorganizationId(int organizationId, int currentPage, int pageSize);
    }
}
