using FundTrack.DAL.Entities;
using FundTrack.Infrastructure.ViewModel;
using FundTrack.Infrastructure.ViewModel.EventViewModel;
using FundTrack.Infrastructure.ViewModel.SuperAdminViewModels;
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
        IEnumerable<EventManagementViewModel> GetEventsByOrganizationIdForPage(int eventId, int currentPage, int pageSize);

        /// <summary>
        /// Gets the one event by identifier.
        /// </summary>
        /// <param name="id">The identifier of event</param>
        /// <returns> EventManagementViewModel </returns>
        EventManagementViewModel GetOneEventById(int eventId);

        /// <summary>
        /// Adds the new event.
        /// </summary>
        /// <param name="newEvent">The new event EventManagementViewModel</param>
        /// <returns> EventManagementViewModel </returns>
        EventManagementViewModel AddNewEvent(EventManagementViewModel newEvent);

        /// <summary>
        /// Updates the event.
        /// </summary>
        /// <param name="updatedEvent">The updated event.</param>
        /// <returns> EventManagementViewModel </returns>
        EventManagementViewModel UpdateEvent(EventManagementViewModel updatedEvent);

        /// <summary>
        /// Inserts the images in data base.
        /// </summary>
        /// <param name="imagesParam">The collection of images.</param>
        /// <param name="eventId">The event identifier.</param>
        void InsertImagesInDataBase(IEnumerable<ImageViewModel> imagesParam, int eventId);

        /// <summary>
        /// Updates the images.
        /// </summary>
        /// <param name="imagesParam">The collection of images for update.</param>
        /// <param name="eventId">The event identifier.</param>
        /// <returns> IEnumerable<ImageViewModel> </returns>
        IEnumerable<ImageViewModel> UpdateImages(IEnumerable<ImageViewModel> imagesParam, int eventId);

        /// <summary>
        /// Deletes the event.
        /// </summary>
        /// <param name="id">The event identifier.</param>
        void DeleteEvent(int eventId);

        /// <summary>
        /// Deletes the images.
        /// </summary>
        /// <param name="eventId">The event identifier.</param>
        void DeleteImages(int eventId);

        /// <summary>
        /// Gets the events initialize data.
        /// </summary>
        /// <param name="organizationId">The organization identifier.</param>
        /// <returns> PaginationInitViewModel </returns>
        PaginationInitViewModel GetEventsInitData(int organizationId);

        /// <summary>
        /// Converts to image view model.
        /// </summary>
        /// <param name="imageList">The image list from database.</param>
        /// <returns> IEnumerable<ImageViewModel> </returns>
        IEnumerable<ImageViewModel> ConvertToImageViewModel(IEnumerable<EventImage> imageList);
    }
}
