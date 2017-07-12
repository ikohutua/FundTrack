using FundTrack.DAL.Entities;
using FundTrack.Infrastructure.ViewModel;
using FundTrack.Infrastructure.ViewModel.EventViewModel;
using System.Collections.Generic;

namespace FundTrack.BLL.Abstract
{
    /// <summary>
    /// Interface for EventManagementService
    /// </summary>
    public interface IEventManagementService
    {
        /// <summary>
        /// Gets all events by organization identifier.
        /// </summary>
        /// <param name="id">The identifier of organization.</param>
        /// <returns> IEnumerable<EventManagementViewModel> </returns>
        IEnumerable<EventManagementViewModel> GetAllEventsByOrganizationId(int id);

        /// <summary>
        /// Gets the one event by identifier.
        /// </summary>
        /// <param name="id">The identifier of event</param>
        /// <returns> EventManagementViewModel </returns>
        EventManagementViewModel GetOneEventById(int id);

        /// <summary>
        /// Adds the new event.
        /// </summary>
        /// <param name="newEvent">The new event EventManagementViewModel</param>
        /// <returns> EventManagementViewModel </returns>
        EventManagementViewModel AddNewEvent(EventManagementViewModel newEvent);


        /// <summary>
        /// Inserts the images in data base.
        /// </summary>
        /// <param name="imagesParam">The collection of images.</param>
        /// <param name="eventId">The event identifier.</param>
        void InsertImagesInDataBase(IEnumerable<ImageViewModel> imagesParam, int eventId);

        /// <summary>
        /// Deletes the event.
        /// </summary>
        /// <param name="id">The event identifier.</param>
        void DeleteEvent(int id);

        /// <summary>
        /// Deletes the images.
        /// </summary>
        /// <param name="eventId">The event identifier.</param>
        void DeleteImages(int eventId);
    }
}
