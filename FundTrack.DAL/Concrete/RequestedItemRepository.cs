using FundTrack.DAL.Abstract;
using System.Collections.Generic;
using FundTrack.DAL.Entities;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace FundTrack.DAL.Concrete
{
    /// <summary>
    /// Class with CRUD operations for RequestedItem entity
    /// </summary>
    public class RequestedItemRepository : IRequestedItemRepository
    {
        private readonly FundTrackContext _dbContext;

        /// <summary>
        /// Initializes a new instance of the <see cref="RequestedItem"/> class.
        /// </summary>
        /// <param name="dbContext"></param>
        public RequestedItemRepository(FundTrackContext dbContext)
        {
            this._dbContext = dbContext;
        }

        /// <summary>
        /// Create requested item in database
        /// </summary>
        /// <param name="item">Requested item model</param>
        /// <returns>Created requested item</returns>
        public RequestedItem Create(RequestedItem item)
        {
            var requestedItem = this._dbContext.RequestedItems.Add(item);

            return requestedItem.Entity;
        }

        /// <summary>
        /// Delete requested item from database
        /// </summary>
        /// <param name="id">Id of requested item entity</param>
        public void Delete(int id)
        {
            var requestedItem = Get(id);
            this._dbContext.RequestedItems.Remove(requestedItem);
        }

        /// <summary>
        /// Get requested item from database
        /// </summary>
        /// <param name="id">Id of requested item</param>
        /// <returns>Requested item entity</returns>
        public RequestedItem Get(int id)
        {
            return this._dbContext.RequestedItems
                .FirstOrDefault(i => i.Id == id);
        }

        /// <summary>
        /// Get all requested items from database
        /// </summary>
        /// <returns>List of requested items</returns>
        public IEnumerable<RequestedItem> Read()
        {
            return this._dbContext.RequestedItems;
        }

        /// <summary>
        /// Update requested item in database
        /// </summary>
        /// <param name="item">Requested item model</param>
        /// <returns>Updated requested item entity</returns>
        public RequestedItem Update(RequestedItem item)
        {
            var updatedRequetedItem = this._dbContext.RequestedItems.Update(item);

            return updatedRequetedItem.Entity;
        }

        /// <summary>
        /// Gets the requested item status.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Status</returns>
        public Status GetRequestedItemStatus(int id)
        {
            return this._dbContext.RequestedItems
                                  .Include(r => r.Status)
                                  .FirstOrDefault(r => r.Id == id)
                                  .Status;
        }

        /// <summary>
        /// Gets the requested items with virtual fields.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Requested Item</returns>
        public RequestedItem GetRequestedItemsWithVirtualFields(int id)
        {
            return this._dbContext.RequestedItems
                                  .Include(i => i.Organization)
                                  .Include(i => i.Status)
                                  .Include(i => i.RequestedItemImages)
                                  .Include(i => i.GoodsCategory)
                                  .FirstOrDefault(i => i.Id == id);
        }
    }
}
