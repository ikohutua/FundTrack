using FundTrack.DAL.Entities;
using System.Collections.Generic;

namespace FundTrack.DAL.Abstract
{
    public interface IEventManagementRepository : IRepository<Event>
    {
        /// <summary>
        /// Gets the amount of events for the page by the organization ID
        /// Join with event images
        /// </summary>
        /// <param name="organizationId">The organization identifier.</param>
        /// <returns> IEnumerable<Event> </returns>
        IEnumerable<Event> GetEventsByOrganizationIdForPage(int organizationId, int currentPage, int itemsPerPage);

        /// <summary>
        /// Gets the event by ID
        /// Join with event images
        /// </summary>
        /// <param name="organizationId">The event identifier.</param>
        /// <returns> Event </returns>
        Event GetOneEventById(int eventId);
    }
}
