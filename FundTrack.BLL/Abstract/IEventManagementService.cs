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
        /// Gets events by organization identifier for page.
        /// </summary>
        /// <param name="eventId">The event identifier.</param>
        /// <param name="currentPage">The current page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>IEnumerable<EventManagementViewModel></returns>
        IEnumerable<EventManagementViewModel> GetEventsByOrganizationIdForPage(int eventId, int currentPage, int pageSize);

        /// <summary>
        /// Gets one event by identifier.
        /// </summary>
        /// <param name="eventId">The event identifier.</param>
        /// <returns>EventManagementViewModel</returns>
        EventManagementViewModel GetOneEventById(int eventId);

        /// <summary>
        /// Adds the new event.
        /// </summary>
        /// <param name="newEvent">The new event.</param>
        /// <returns>EventManagementViewModel</returns>
        EventManagementViewModel AddNewEvent(EventManagementViewModel newEvent);

        /// <summary>
        /// Updates the event.
        /// </summary>
        /// <param name="updatedEvent">The updated event.</param>
        /// <returns>EventManagementViewModel</returns>
        EventManagementViewModel UpdateEvent(EventManagementViewModel updatedEvent);

        /// <summary>
        /// Inserts the images in data base.
        /// </summary>
        /// <param name="imagesParam">The images parameter.</param>
        /// <param name="eventId">The event identifier.</param>
        void InsertImagesInDataBase(IEnumerable<ImageViewModel> imagesParam, int eventId);

        /// <summary>
        /// Updates the images for concrete event.
        /// </summary>
        /// <param name="imagesParam">The images parameter.</param>
        /// <param name="eventId">The event identifier.</param>
        /// <returns>IEnumerable<ImageViewModel></returns>
        IEnumerable<ImageViewModel> UpdateImages(IEnumerable<ImageViewModel> imagesParam, int eventId);

        /// <summary>
        /// Deletes the event.
        /// </summary>
        /// <param name="eventId">The event identifier.</param>
        void DeleteEvent(int eventId);

        /// <summary>
        /// Deletes the images for concrete event.
        /// </summary>
        /// <param name="eventId">The event identifier.</param>
        void DeleteImages(int eventId);

        /// <summary>
        /// Gets the events initialize data.
        /// </summary>
        /// <param name="organizationId">The organization identifier.</param>
        /// <returns>PaginationInitViewModel</returns>
        PaginationInitViewModel GetEventsInitData(int organizationId);

        /// <summary>
        /// Converts 'EventImage' to 'ImageViewModel'
        /// </summary>
        /// <param name="imageList">The image list.</param>
        /// <returns>IEnumerable<ImageViewModel></returns>
        IEnumerable<ImageViewModel> ConvertToImageViewModel(IEnumerable<EventImage> imageList);

        /// <summary>
        /// Deletes the current image.
        /// </summary>
        /// <param name="idImage">The identifier for image.</param>
        void DeleteCurrentImage(int idImage);

        /// <summary>
        /// Adds the new image in data base.
        /// </summary>
        /// <param name="image">The image.</param>
        /// <returns>EventImage</returns>
        EventImage AddNewImageInDataBase(EventImage image);
    }
}
