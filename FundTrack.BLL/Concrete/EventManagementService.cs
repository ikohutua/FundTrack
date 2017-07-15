using FundTrack.BLL.Abstract;
using FundTrack.DAL.Abstract;
using FundTrack.DAL.Entities;
using FundTrack.Infrastructure.ViewModel;
using FundTrack.Infrastructure.ViewModel.EventViewModel;
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
            try
            {
                var createdEvent = this._unitOfWork.EventRepository.Create(new Event()
                {
                    Id = 0,
                    Description = newEvent.Description,
                    OrganizationId = newEvent.OrganizationId,
                    CreateDate = DateTime.Now,
                });

                if (createdEvent == null)
                {
                    throw new BusinessLogicException("Не вдалось створити нову подію");
                }

                this.InsertImagesInDataBase(newEvent.Images, createdEvent.Id);
                this._unitOfWork.SaveChanges();
                return this.GetOneEventById(createdEvent.Id);
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex.Message);
            }
        }

        /// <summary>
        /// Deletes the event.
        /// </summary>
        /// <param name="id">The identifier.</param>
        public void DeleteEvent(int eventId)
        {
            try
            {
                this.DeleteImages(eventId);
                this._unitOfWork.EventRepository.Delete(eventId);
                this._unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex.Message);
            }
        }

        /// <summary>
        /// Deletes the images.
        /// </summary>
        /// <param name="eventId">The event identifier.</param>
        public void DeleteImages(int eventId)
        {
            try
            {
                var images = this._unitOfWork.EventImageRepository.Read().Where(i => i.EventId == eventId);

                if (images == null)
                {
                    throw new BusinessLogicException($"В базі даних немає події з ідентифікатором {eventId}");
                }

                for (int i = 0; i < images.Count(); i++)
                {
                    this._unitOfWork.EventImageRepository.Delete(images.ElementAt(i).Id);
                }
                this._unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex.Message);
            }
        }

        /// <summary>
        /// Gets all events by organization identifier.
        /// </summary>
        /// <param name="id">The identifier for organization</param>
        /// <returns>IEnumerable<EventManagementViewModel></returns>
        public IEnumerable<EventManagementViewModel> GetEventsByOrganizationIdForPage(int organizationId, int currentPage, int itemsPerPage)
        {
            try
            {
                return this._unitOfWork.EventRepository.GetEventsByOrganizationIdForPage(organizationId, currentPage, itemsPerPage).Select(
                    events => new EventManagementViewModel()
                    {
                        Id = events.Id,
                        Description = events.Description,
                        CreateDate = events.CreateDate,
                        OrganizationId = events.OrganizationId,
                        Images = this.ConvertToImageViewModel(events.EventImages.Where(i => i.EventId == events.Id))
                    });
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex.Message);
            }
        }

        /// <summary>
        /// Gets the one event by identifier.
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns>
        /// EventManagementViewModel
        /// </returns>
        /// <exception cref="BusinessLogicException">
        /// </exception>
        public EventManagementViewModel GetOneEventById(int eventId)
        {
            try
            {
                var ev = this._unitOfWork.EventRepository.GetOneEventById(eventId);

                if (ev == null)
                {
                    throw new BusinessLogicException($"Подія з ідентифікатором {eventId} не знайдена");
                }

                return new EventManagementViewModel()
                {
                    Id = ev.Id,
                    CreateDate = ev.CreateDate,
                    Description = ev.Description,
                    OrganizationId = ev.OrganizationId,
                    Images = this.ConvertToImageViewModel(ev.EventImages.Where(i => i.EventId == eventId))
                };
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex.Message);
            }
        }

        /// <summary>
        /// Inserts the images in data base.
        /// </summary>
        /// <param name="imagesParam">The collection of images.</param>
        /// <param name="eventId">The event identifier.</param>
        public void InsertImagesInDataBase(IEnumerable<ImageViewModel> imagesParam, int eventId)
        {
            try
            {
                var images = imagesParam.ToList();
                for (int i = 0; i < images.Count; i++)
                {
                    var createdImage = this._unitOfWork.EventImageRepository.Create(new EventImage()
                    {
                        EventId = eventId,
                        ImageUrl = images[i].ImageUrl
                    });
                    if (createdImage == null)
                    {
                        throw new BusinessLogicException($"Зображення з адресом {images[i].ImageUrl} не збережено в базі даних");
                    }
                }
                this._unitOfWork.SaveChanges();
            }
            catch(Exception ex)
            {
                throw new BusinessLogicException(ex.Message);
            }
        }

        /// <summary>
        /// Updates the images.
        /// </summary>
        /// <param name="imagesParam">The collection of images for update.</param>
        /// <param name="eventId">The event identifier.</param>
        /// <returns> IEnumerable<ImageViewModel> </returns>
        public IEnumerable<ImageViewModel> UpdateImages(IEnumerable<ImageViewModel> imagesParam, int eventId)
        {
            try
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

                    if (updatedImage == null)
                    {
                        throw new BusinessLogicException($"Зображення з адресом {images[i].ImageUrl} не оновлено в базі даних");
                    }

                    updatedImages.Add(new ImageViewModel()
                    {
                        Id = updatedImage.Id,
                        ImageUrl = updatedImage.ImageUrl
                    });
                }

                if (updatedImages.Count == 0)
                {
                    throw new BusinessLogicException("Не додано жлдного зображення в базу даних");
                }

                this._unitOfWork.SaveChanges();
                return updatedImages;
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex.Message);
            }
            finally
            {
                this._unitOfWork.SaveChanges();
            }
        }

        /// <summary>
        /// Updates the event.
        /// </summary>
        /// <param name="updatedEvent">The updated event.</param>
        /// <returns> EventManagementViewModel </returns>
        public EventManagementViewModel UpdateEvent(EventManagementViewModel updatedEvent)
        {
            try
            {
                var updatedEventFromDB = this._unitOfWork.EventRepository.Update(new Event()
                {
                    Id = updatedEvent.Id,
                    CreateDate = updatedEvent.CreateDate,
                    Description = updatedEvent.Description,
                    OrganizationId = updatedEvent.OrganizationId
                });

                this._unitOfWork.SaveChanges();

                if (updatedEventFromDB == null)
                {
                    throw new BusinessLogicException($"Подію з ідентифікатором {updatedEvent.Id} не вдалось оновити");
                }

                return new EventManagementViewModel()
                {
                    Id = updatedEventFromDB.Id,
                    CreateDate = updatedEventFromDB.CreateDate,
                    Description = updatedEventFromDB.Description,
                    OrganizationId = updatedEventFromDB.OrganizationId,
                    Images = this.UpdateImages(updatedEvent.Images, updatedEvent.Id)
                };
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex.Message);
            }
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
            try
            {
                return new PaginationInitViewModel()
                {
                    ItemsPerPage = 4,
                    TotalItemsCount = this._unitOfWork.EventRepository.Read().Where(ev => ev.OrganizationId == organizationId).Count()
                };
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex.Message);
            }
        }

        /// <summary>
        /// Converts 'EventImage' entity to image view model.
        /// </summary>
        /// <param name="imageList">The image list from database.</param>
        /// <returns> IEnumerable<ImageViewModel> </returns>
        public IEnumerable<ImageViewModel> ConvertToImageViewModel(IEnumerable<EventImage> imageList)
        {
            try
            {
                return imageList.Select(image => new ImageViewModel()
                {
                    Id = image.Id,
                    ImageUrl = image.ImageUrl
                });
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex.Message);
            }
        }
    }
}
