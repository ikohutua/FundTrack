using FundTrack.DAL.Abstract;
using System.Collections.Generic;
using FundTrack.DAL.Entities;
using System.Linq;
using System;
using Microsoft.EntityFrameworkCore;
using FundTrack.DAL.Extensions;
using FundTrack.Infrastructure.ViewModel;

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
            var requestedItem = this._dbContext.RequestedItems
                .Include(g => g.GoodsCategory)
                .Include(g => g.Status)
                .Include(g => g.RequestedItemImages)
                .Include(g => g.UserResponses)
                .FirstOrDefault(r => r.Id == id);

            return requestedItem;
        }

        /// <summary>
        /// Gets organization requested items
        /// </summary>
        /// <param name="organizationId">Id of organization</param>
        /// <returns>List of organization requested items</returns>
        public IEnumerable<RequestedItem> GetOrganizationRequestedItems(int organizationId)
        {
            var requestedItems = this._dbContext.RequestedItems
                .Include(g => g.GoodsCategory)
                .Include(g => g.Status)
                .Include(g => g.RequestedItemImages)
                .Where(r => r.OrganizationId == organizationId);

            return requestedItems;
        }

        /// <summary>
        /// Get all requested items from database
        /// </summary>
        /// <returns>List of requested items</returns>
        public IEnumerable<RequestedItem> Read()
        {
            return this._dbContext.RequestedItems.Include(x => x.RequestedItemImages);
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
        /// Reads as queryable.
        /// </summary>
        /// <returns>List of requested item</returns>
        public IQueryable<RequestedItem> ReadAsQueryable()
        {
            return this._dbContext.RequestedItems.Include(x => x.RequestedItemImages);
        }

        /// <summary>
        /// Reads as queryable for pagination.
        /// </summary>
        /// <returns>List of requested item</returns>
        public IQueryable<RequestedItem> ReadForPagination(int ItemsPerPage, int CurrentPage)
        {
            return this._dbContext.RequestedItems
                       .GetItemsPerPage(ItemsPerPage, CurrentPage);
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

        /// <summary>
        /// Gets all goodTypes from database
        /// </summary>
        /// <returns>List of goodsType</returns>
        public IEnumerable<GoodsType> GetAllGoodTypes()
        {
            var goodsTypes = this._dbContext.GoodsTypes
                .Include(c => c.GoodsCategories);

            return goodsTypes;
        }

        /// <summary>
        /// Gets requeste item per page
        /// </summary>
        /// <param name="organizationId">Organization id</param>
        /// <param name="currentPage">Current page</param>
        /// <param name="pageSize">Page size</param>
        /// <returns>Requested items list</returns>
        public IEnumerable<RequestedItem> GetRequestedItemPerPageByorganizationId(int organizationId, int currentPage, int pageSize)
        {
            var resultList = this._dbContext.RequestedItems
                .Include(r => r.RequestedItemImages)
                .Include(r => r.GoodsCategory)
                .Where(r => r.OrganizationId == organizationId)
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize);

            return resultList;
        }

        /// <summary>
        /// Filters the requested item.
        /// </summary>
        /// <param name="filters">The filters.</param>
        /// <returns>List filter requsted item</returns>
        public IQueryable<RequestedItem> FilterRequestedItem(FilterPaginationViewModel filters)
        {
            return this._dbContext.RequestedItems
                                  .Where(i => i.Organization.Name == (filters.filterOptions.OrganizationFilter != "" ? filters.filterOptions.OrganizationFilter : i.Organization.Name))
                                  .Where(i => i.GoodsCategory.Name == (filters.filterOptions.CategoryFilter != "" ? filters.filterOptions.CategoryFilter : i.GoodsCategory.Name))
                                  .Where(i => i.GoodsCategory.GoodsType.Name == (filters.filterOptions.TypeFilter != "" ? filters.filterOptions.TypeFilter : i.GoodsCategory.GoodsType.Name))
                                  .Where(i => i.Status.StatusName == (filters.filterOptions.StatusFilter != "" ? filters.filterOptions.StatusFilter : i.Status.StatusName))
                                  .GetItemsPerPage(filters.ItemsPerPage, filters.CurrentPage);
        }

        /// <summary>
        /// Filters the requested item.
        /// </summary>
        /// <param name="filters">The filters.</param>
        /// <returns>List filter requsted item</returns>
        public IQueryable<RequestedItem> FilterRequestedItem(FilterRequstedViewModel filters)
        {
            return this._dbContext.RequestedItems.Include(x => x.RequestedItemImages)
                                  .Where(i => i.Organization.Name == (filters.OrganizationFilter != "" ? filters.OrganizationFilter : i.Organization.Name))
                                  .Where(i => i.GoodsCategory.Name == (filters.CategoryFilter != "" ? filters.CategoryFilter : i.GoodsCategory.Name))
                                  .Where(i => i.GoodsCategory.GoodsType.Name == (filters.TypeFilter != "" ? filters.TypeFilter : i.GoodsCategory.GoodsType.Name))
                                  .Where(i => i.Status.StatusName == (filters.StatusFilter != "" ? filters.StatusFilter : i.Status.StatusName));
        }
    }
}
