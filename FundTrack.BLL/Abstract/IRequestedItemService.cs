using FundTrack.Infrastructure.ViewModel;
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
        List<RequestedItemViewModel> GetAllItems();

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


    }
}
