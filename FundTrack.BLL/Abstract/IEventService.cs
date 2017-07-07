using FundTrack.Infrastructure.ViewModel.EventViewModel;
using System.Collections.Generic;

namespace FundTrack.BLL.Abstract
{
    /// <summary>
    /// Interface to work with Events
    /// </summary>
    public interface IEventService
    {
        /// <summary>
        /// Gets specific count of this instance.
        /// </summary>
        /// <returns>Collections of instances</returns>
        IEnumerable<EventViewModel> GetAllEventsByScroll(int countOfEventsToLoad, int koefToLoadEvent);

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>Collections of instances</returns>
        IEnumerable<EventViewModel> GetAllEvents();

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>Collections of specific instances</returns>
        IEnumerable<EventViewModel> GetAllEventsById(int id);

        /// <summary>
        /// Gets Initial data for event pagination
        /// </summary>
        /// <returns>Pagination Initial data</returns>
        EventPaginationInitViewModel GetEventPaginationData();
    }
}