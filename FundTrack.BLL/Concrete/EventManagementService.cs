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

        /// <summary>Adds the new event.</summary>
        /// <param name="newEvent">The new event EventManagementViewModel.</param>
        /// <returns>EventManagementViewModel</returns>
        /// <exception cref="BusinessLogicException">
        /// Не вдалось створити нову подію
        /// or
        /// Exception.Message
        /// </exception>
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

        /// <summary>Deletes the event.</summary>
        /// <param name="eventId">The event identifier.</param>
        /// <exception cref="BusinessLogicException"></exception>
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

        /// <summary>Deletes images for concrete event.</summary>
        /// <param name="eventId">The event identifier.</param>
        /// <exception cref="BusinessLogicException">
        /// В базі даних немає події з ідентифікатором Id
        /// or
        /// Exception.Message
        /// </exception>
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

        /// <summary>Gets events by organization identifier for current page.</summary>
        /// <param name="organizationId">The organization identifier.</param>
        /// <param name="currentPage">The current page.</param>
        /// <param name="itemsPerPage">The items per page.</param>
        /// <returns> IEnumerable<EventManagementViewModel> </returns>
        /// <exception cref="BusinessLogicException"></exception>
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

        /// <summary>Gets the event by identifier.</summary>
        /// <param name="eventId">The event identifier.</param>
        /// <returns>EventManagementViewModel</returns>
        /// <exception cref="BusinessLogicException">
        /// Подія з ідентифікатором eventId не знайдена
        /// or
        /// Exception.Message
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

        /// <summary>Inserts the images in data base.</summary>
        /// <param name="imagesParam">The collection of images.</param>
        /// <param name="eventId">The event identifier.</param>
        /// <exception cref="BusinessLogicException">
        /// Зображення з адресом ImageUrl не збережено в базі даних
        /// or
        /// Exception.Message
        /// </exception>
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
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex.Message);
            }
        }

        /// <summary>Adds the new image in data base.</summary>
        /// <param name="image">The image.</param>
        /// <returns>EventImage</returns>
        /// <exception cref="BusinessLogicException">
        /// Не вдалось зберегти нове зображення в базі даних
        /// or 
        /// Exception
        /// </exception>
        public EventImage AddNewImageInDataBase(EventImage image)
        {
            try
            {
                var created = this._unitOfWork.EventImageRepository.Create(image);

                if (created == null)
                {
                    throw new BusinessLogicException($"Не вдалось зберегти нове зображення в базі даних");
                }

                this._unitOfWork.SaveChanges();

                return created;
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex.Message);
            }
        }

        /// <summary>Updates images for concrete event.</summary>
        /// <param name="imagesParam">The collection of images.</param>
        /// <param name="eventId">The event identifier.</param>
        /// <returns>IEnumerable<ImageViewModel></returns>
        /// <exception cref="BusinessLogicException">
        /// Не додано жодного зображення в базу даних
        /// or
        /// Exception.Message
        /// </exception>
        public IEnumerable<ImageViewModel> UpdateImages(IEnumerable<ImageViewModel> imagesParam, int eventId)
        {
            try
            {
                var images = imagesParam.ToList();
                for (int i = 0; i < images.Count; i++)
                {
                    //if image is new
                    if (images[i].Id == 0)
                    {
                        var created = this.AddNewImageInDataBase(new EventImage()
                        {
                            Id = 0,
                            EventId = eventId,
                            ImageUrl = images[i].ImageUrl
                        });

                        //creates view model for image
                        images[i] = new ImageViewModel()
                        {
                            Id = created.Id,
                            ImageUrl = created.ImageUrl
                        };
                    }
                    this._unitOfWork.SaveChanges();
                }

                this._unitOfWork.SaveChanges();
                return images;
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

        /// <summary>Updates the event.</summary>
        /// <param name="updatedEvent">The updated event.</param>
        /// <returns>EventManagementViewModel</returns>
        /// <exception cref="BusinessLogicException">
        /// Подію з ідентифікатором Id не вдалось оновити
        /// or
        /// Exception.Message
        /// </exception>
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

        /// <summary>Gets the events initialize data.</summary>
        /// <param name="organizationId">The organization identifier.</param>
        /// <returns>PaginationInitViewModel</returns>
        /// <exception cref="BusinessLogicException"></exception>
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

        /// <summary>Convert 'EventImage' to 'ImageViewModel'.</summary>
        /// <param name="imageList">The collection of images.</param>
        /// <returns>IEnumerable<ImageViewModel></returns>
        /// <exception cref="BusinessLogicException"></exception>
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

        /// <summary>
        /// Deletes the current image.
        /// </summary>
        /// <param name="idImage">The identifier for image.</param>
        /// <exception cref="BusinessLogicException"></exception>
        public void DeleteCurrentImage(int idImage)
        {
            try
            {
                this._unitOfWork.EventImageRepository.Delete(idImage);
                this._unitOfWork.SaveChanges();
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex.Message);
            }
        }
    }
}
