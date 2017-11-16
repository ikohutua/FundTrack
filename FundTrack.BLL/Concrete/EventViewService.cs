using FundTrack.BLL.Abstract;
using FundTrack.DAL.Abstract;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using FundTrack.DAL.Entities;
using FundTrack.Infrastructure.ViewModel.EventViewModel;
using System;
using FundTrack.Infrastructure;

namespace FundTrack.BLL.Concrete
{
    /// <summary>
    /// Class for work with Events
    /// </summary>
    /// <seealso cref="FundTrack.BLL.Abstract.IEventService{FundTrack.Infrastructure.ViewModel.EventViewModel}" />
    public sealed class EventViewService : BaseService, IEventService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly int _pageSize = 6;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventViewService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public EventViewService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Gets  events of all organizations per scrollng.
        /// </summary>
        /// <returns>Collection of EventViewModel</returns>
        public IEnumerable<EventViewModel> GetAllEventsByScroll(int countOfEventsToLoad, int koefToLoadEvent)
        {
            try
            {
                var events = _unitOfWork.EventRepository.Read()
                
                 .Select(c => new EventViewModel()
                 {
                     Id = c.Id,
                     OrganizationId = c.OrganizationId,
                     OrganizationName = c.Organization.Name,
                     Description = c.Description,
                     CreateDate = c.CreateDate,
                     ImageUrl = AzureStorageConfiguration.GetImageUrl(c.EventImages.Single(r => r.IsMain == true).ImageUrl)
                 }).OrderByDescending(e => e.CreateDate).ToList();
               
                return GetPageItems(events, countOfEventsToLoad, koefToLoadEvent);
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex.Message);
            }

        }

        /// <summary>
        /// Gets  events of all organizations.
        /// </summary>
        /// <returns>Collection of EventViewModel</returns>
        public IEnumerable<EventViewModel> GetAllEvents()
        {
            try
            {
                var events = ((DbSet<Event>)_unitOfWork.EventRepository.Read())
                 .Include(e => e.Organization)
                 .Include(i => i.EventImages)
                 .Include(e => e.Organization.BannedOrganization)
                 .Where(e => e.Organization.BannedOrganization == null)
                 .Select(c => new EventViewModel()
                 {
                     Id = c.Id,
                     OrganizationId = c.OrganizationId,
                     OrganizationName = c.Organization.Name,
                     Description = c.Description,
                     CreateDate = c.CreateDate,
                     ImageUrl = AzureStorageConfiguration.GetImageUrl(c.EventImages.Single(r => r.IsMain == true).ImageUrl)
                 }).OrderByDescending(e => e.CreateDate);

        
                return events;
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex.Message);
            }

        }

        /// <summary>
        /// Gets some number of events by specific organization.
        /// </summary>
        /// <returns>Collection of EventViewModels for specific organization</returns>
        /// ----------------------------------------------------------------------------------------------------------------------------------------
        public IEnumerable<EventViewModel> GetAllEventsById(int id)
        {
            try
            {
                IEnumerable<EventViewModel> events = _unitOfWork.EventRepository.Read()
                    .Where(e => e.OrganizationId == id)
                    .Select(c => new EventViewModel()
                    {
                        Id = c.Id,
                        OrganizationId = c.OrganizationId,
                        OrganizationName = c.Organization.Name,
                        Description = c.Description,
                        CreateDate = c.CreateDate,
                        ImageUrl = AzureStorageConfiguration.GetImageUrl(c.EventImages.Single(r => r.IsMain == true).ImageUrl)
                    }).OrderByDescending(e => e.CreateDate);

                return events;
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex.Message);
            }

        }

        /// <summary>
        /// Gets Initial data for event pagination
        /// </summary>
        /// <returns>Event Initial data</returns>
        public EventPaginationInitViewModel GetEventPaginationData()
        {
            return new EventPaginationInitViewModel
            {
                TotalItemsCount = _unitOfWork.EventRepository.Read().Count(),
                ItemsPerPage = _pageSize
            };
        }
    }
}