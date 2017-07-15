using FundTrack.BLL.Abstract;
using FundTrack.DAL.Abstract;
using FundTrack.DAL.Entities;
using FundTrack.Infrastructure.ViewModel;
using FundTrack.Infrastructure.ViewModel.EventViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using FundTrack.Infrastructure.ViewModel.SuperAdminViewModels;

namespace FundTrack.BLL.Concrete
{
    /// <summary>
    /// Service for event management
    /// </summary>
    /// <seealso cref="FundTrack.BLL.Abstract.IEventManagementService" />
    public sealed class EventManagementService : BaseService, IEventManagementService
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventManagementService"/> class.
        /// </summary>
        /// <param name="unitOfWorkParam">The unit of work parameter.</param>
        public EventManagementService(IUnitOfWork unitOfWorkParam) : base()
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
            this.InsertImagesInDataBase(newEvent.Images, createdEvent.Id);
            this._unitOfWork.SaveChanges();
            return this.GetOneEventById(createdEvent.Id);
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
            for (int i = 0; i < images.Count(); i++)
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
        public IEnumerable<EventManagementViewModel> GetEventsPerPageByOrganizationId(int id, int currentPage, int pageSize)
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
                }).OrderByDescending(e => e.CreateDate)
                .Skip((currentPage - 1) * pageSize)
                .Take(pageSize);

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
            }
            this._unitOfWork.SaveChanges();
        }

        /// <summary>
        /// Updates the images.
        /// </summary>
        /// <param name="imagesParam">The collection of images for update.</param>
        /// <param name="eventId">The event identifier.</param>
        /// <returns> IEnumerable<ImageViewModel> </returns>
        public IEnumerable<ImageViewModel> UpdateImages(IEnumerable<ImageViewModel> imagesParam, int eventId)
        {
            var images = imagesParam.ToList();
            var updatedImages = new List<ImageViewModel>();
            for (int i = 0; i < images.Count; i++)
            {
                var updatedImage = this._unitOfWork.EventImageRepository.Update(new EventImage()
                {
                    Id = images[i].Id,
                    EventId = eventId,
                    ImageUrl = images[i].ImageUrl,
                });

                this._unitOfWork.SaveChanges();

                updatedImages.Add(new ImageViewModel()
                {
                    Id = updatedImage.Id,
                    ImageUrl = updatedImage.ImageUrl
                });
            }

            return updatedImages;
        }

        /// <summary>
        /// Updates the event.
        /// </summary>
        /// <param name="updatedEvent">The updated event.</param>
        /// <returns> EventManagementViewModel </returns>
        public EventManagementViewModel UpdateEvent(EventManagementViewModel updatedEvent)
        {
            var updatedEventFromDB = this._unitOfWork.EventRepository.Update(new Event()
            {
                Id = updatedEvent.Id,
                CreateDate = updatedEvent.CreateDate,
                Description = updatedEvent.Description,
                OrganizationId = updatedEvent.OrganizationId
            });

            this._unitOfWork.SaveChanges();

            return new EventManagementViewModel()
            {
                Id = updatedEventFromDB.Id,
                CreateDate = updatedEventFromDB.CreateDate,
                Description = updatedEventFromDB.Description,
                OrganizationId = updatedEventFromDB.OrganizationId,
                Images = this.UpdateImages(updatedEvent.Images, updatedEvent.Id)
            };
        }

        /// <summary>
        /// Gets the events initialize data.
        /// </summary>
        /// <param name="organizationId">The organization identifier.</param>
        /// <returns>
        /// PaginationInitViewModel
        /// </returns>
        public PaginationInitViewModel GetEventsInitData(int organizationId)
        {
            return new PaginationInitViewModel()
            {
                ItemsPerPage = 8,
                TotalItemsCount = this._unitOfWork.EventRepository.Read().Where(ev => ev.OrganizationId == organizationId).Count()
            };
        }
    }
}
