using FundTrack.DAL.Entities;
using FundTrack.Infrastructure.ViewModel;
using FundTrack.Infrastructure.ViewModel.RequestedItemModel;
using System.Collections.Generic;

namespace FundTrack.BLL.Abstract
{
    /// <summary>
    /// Requested item service
    /// </summary>
    public interface IRequestedItemService
    {
        /// <summary>
        /// Gets all requested items from database
        /// </summary>
        /// <returns>List of items</returns>
        List<RequestedItemViewModel> GetOrganizationRequestedId(int organizationId);

        /// <summary>
        /// Gets requested item from database
        /// </summary>
        /// <param name="id">Requested item id</param>
        /// <returns>Requested item view model</returns>
        RequestedItemViewModel GetItemById(int id);

        /// <summary>
        /// Creates requested item in database
        /// </summary>
        /// <param name="requestedItemViewModel">Requested item view model</param>
        /// <returns>Requested item view model</returns>
        RequestedItemViewModel CreateRequestedItem(RequestedItemViewModel requestedItemViewModel);

        /// <summary>
        /// Updates requested item in database
        /// </summary>
        /// <param name="requestedItemViewModel">Requested item view model</param>
        /// <returns>Requested item view model</returns>
        RequestedItemViewModel UpdateRequestedItem(RequestedItemViewModel requestedItemViewModel);

        /// <summary>
        /// Delete requested item from database
        /// </summary>
        /// <param name="id">Id of requested item</param>
        void DeleteRequestedItem(int id);

        /// <summary>
        /// Gets the requested item detail.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns> Requested View Model</returns>
        RequestedItemDetailViewModel GetRequestedItemDetail(int id);

        /// <summary>
        /// Gets all goods type from databse
        /// </summary>
        /// <returns></returns>
        IEnumerable<GoodsTypeViewModel> GetAllGoodTypes();

        /// <summary>
        /// Gets requested items of all organizations with additional information.
        /// </summary>
        /// <returns>Collection of ShowAllRequestedItemModel</returns>
        IEnumerable<ShowAllRequestedItemModel> GetRequestedItemToShow();

        /// <summary>
        /// Gets Initial data for RequestedItem pagination
        /// </summary>
        /// <returns>Pagination Initial data</returns>
        RequestedItemPaginationInitViewModel GetRequestedItemPaginationData();

        /// <summary>
        /// Gets specific count of this instance for pagination.
        /// </summary>
        /// <returns>Collections of instances</returns>
        IEnumerable<ShowAllRequestedItemModel> GetRequestedItemToShowPerPage(FilterPaginationViewModel filter);

        /// <summary>
        /// Creates the user response on requested item.
        /// </summary>
        /// <param name="userResponse">The user response.</param>
        /// <returns>Requested View Model</returns>
        UserResponseViewModel CreateUserResponse(UserResponseViewModel userResponse);

        /// <summary>
        /// Delete current image from database
        /// </summary>
        /// <param name="currentId"></param>
        void DeleteCurrentImage(int currentId);

        /// <summary>
        /// Gets requested item per page
        /// </summary>
        IEnumerable<RequestedItemViewModel> GetRequestedItemPerPageByorganizationId(int id, int currentPage, int pageSize);

        /// <summary>
        /// Gets requested item initialize data
        /// </summary>
        /// <param name="organizationId">Id of organization</param>
        /// <returns>List of requested items</returns>
        RequestedItemPaginationViewModel GetRequestedItemsInitData(int organizationId);

        /// <summary>
        /// Filters the requested items.
        /// </summary>
        /// <param name="filters">The filters.</param>
        /// <returns></returns>
        RequestedItemPaginationInitViewModel GetFilterRequestedItemPaginationData(FilterRequstedViewModel filters);

        /// <summary>
        /// Gets all organizations for filtering of RequestedItem 
        /// </summary>
        /// <returns>Organizations data</returns>
        IEnumerable<OrganizationForFilteringViewModel> GetOrganizations();

        /// <summary>
        /// Gets all categories for filtering of RequestedItem 
        /// </summary>
        /// <returns>Categories data</returns>
        IEnumerable<GoodsCategoryForFilteringViewModel> GetCategories();

        /// <summary>
        /// Gets all types for filtering of RequestedItem 
        /// </summary>
        /// <returns>Types data</returns>
        IEnumerable<GoodsTypeForFilteringViewModel> GetTypes();

        /// <summary>
        /// Gets all statuses for filtering of RequestedItem 
        /// </summary>
        /// <returns>Statuses data</returns>
        IEnumerable<StatusForFilteringViewModel> GetStatuses();
    }
}
