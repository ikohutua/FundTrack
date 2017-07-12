using FundTrack.BLL.Abstract;
using FundTrack.DAL.Abstract;
using FundTrack.DAL.Entities;
using FundTrack.Infrastructure.ViewModel;
using FundTrack.Infrastructure.ViewModel.EventViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FundTrack.BLL.Concrete
{
    /// <summary>
    /// Service for event management
    /// </summary>
    /// <seealso cref="FundTrack.BLL.Abstract.IEventManagementService" />
    public sealed class EventManagementService : IEventManagementService
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventManagementService"/> class.
        /// </summary>
        /// <param name="unitOfWorkParam">The unit of work parameter.</param>
        public EventManagementService(IUnitOfWork unitOfWorkParam)
        {
            this._unitOfWork = unitOfWorkParam;
        }

        /// <summary>
        /// Adds new event.
        /// </summary>
        /// <param name="newEvent">The new event view model</param>
        /// <returns> Event - entity </returns>
        public EventManagementViewModel AddNewEvent(EventManagementViewModel newEvent)
        {
            var createdEvent = this._unitOfWork.EventRepository.Create(new Event()
            {
                Id = 0,
                Description = newEvent.Description,
                OrganizationId = newEvent.OrganizationId,
                CreateDate = DateTime.Now,
            });
            this._unitOfWork.SaveChanges();
            var result = this._unitOfWork.EventRepository.Read().Where(e => e.Description == createdEvent.Description).FirstOrDefault();
            this.InsertImagesInDataBase(newEvent.Images, result.Id);
            return this.GetOneEventById(result.Id);
        }

        /// <summary>
        /// Deletes the event.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void DeleteEvent(int id)
        {
            this.DeleteImages(id);
            this._unitOfWork.EventRepository.Delete(id);
            this._unitOfWork.SaveChanges();
        }

        /// <summary>
        /// Deletes the images.
        /// </summary>
        /// <param name="eventId">The event identifier.</param>
        public void DeleteImages(int eventId)
        {
            var images = this._unitOfWork.EventImageRepository.Read().Where(i => i.EventId == eventId);
            for(int i = 0; i < images.Count(); i++)
            {
                this._unitOfWork.EventImageRepository.Delete(images.ElementAt(i).Id);
                this._unitOfWork.SaveChanges();
            }
        }

        /// <summary>
        /// Gets all events by organization identifier.
        /// </summary>
        /// <param name="id">The identifier for organization</param>
        /// <returns>IEnumerable<EventManagementViewModel></returns>
        public IEnumerable<EventManagementViewModel> GetAllEventsByOrganizationId(int id)
        {
            var result = ((DbSet<Event>)this._unitOfWork.EventRepository.Read())
                .Where(ev => ev.OrganizationId == id)
                .Include(ev => ev.EventImages)
                .Select(events => new EventManagementViewModel()
                {
                    Id = events.Id,
                    Description = events.Description,
                    CreateDate = events.CreateDate,
                    OrganizationId = events.OrganizationId,
                    Images = events.EventImages
                                   .Where(im => im.EventId == events.Id)
                                   .Select(images => new ImageViewModel()
                                   {
                                       Id = images.Id,
                                       ImageUrl = images.ImageUrl
                                   })
                }).OrderByDescending(e => e.CreateDate);
            return result;
        }

        /// <summary>
        /// Gets the one event by identifier.
        /// </summary>
        /// <param name="id">The identifier of event.</param>
        /// <returns>EventManagementViewModel</returns>
        public EventManagementViewModel GetOneEventById(int id)
        {
            var result = ((DbSet<Event>)this._unitOfWork.EventRepository.Read())
                .Where(ev => ev.Id == id)
                .Include(ev => ev.EventImages)
                .Select(ev => new EventManagementViewModel()
                {
                    Id = ev.Id,
                    Description = ev.Description,
                    CreateDate = ev.CreateDate,
                    OrganizationId = ev.OrganizationId,
                    Images = ev.EventImages
                               .Where(im => im.EventId == ev.Id)
                               .Select(images => new ImageViewModel()
                               {
                                   Id = images.Id,
                                   ImageUrl = images.ImageUrl
                               })
                }).FirstOrDefault();
            return result;
        }


        /// <summary>
        /// Inserts the images in data base.
        /// </summary>
        /// <param name="imagesParam">The collection of images.</param>
        /// <param name="eventId">The event identifier.</param>
        public void InsertImagesInDataBase(IEnumerable<ImageViewModel> imagesParam, int eventId)
        {
            var images = imagesParam.ToList();
            for (int i = 0; i < images.Count; i++)
            {
                this._unitOfWork.EventImageRepository.Create(new EventImage()
                {
                    EventId = eventId,
                    ImageUrl = images[i].ImageUrl
                });
                this._unitOfWork.SaveChanges();
            }
        }
    }
}
