using FundTrack.BLL.Abstract;
using FundTrack.DAL.Abstract;
using FundTrack.DAL.Entities;
using FundTrack.Infrastructure.ViewModel;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FundTrack.BLL.Concrete
{
    /// <summary>
    /// Class for work with details of Event 
    /// </summary>
    /// <seealso cref="FundTrack.BLL.Abstract.IViewService{FundTrack.Infrastructure.ViewModel.EventDetailViewModel}" />
    public sealed class EventDetailViewService : IViewService<EventDetailViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventDetailViewService" /> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public EventDetailViewService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Adds the specified new item.
        /// </summary>
        /// <param name="newItem">The new item.</param>
        /// <exception cref="NotImplementedException"></exception>
        public void Add(EventDetailViewModel newItem)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <exception cref="NotImplementedException"></exception>
        public void Delete(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>Collection of EventDetailViewModel</returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<EventDetailViewModel> Get()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Gets this instance by id.
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Collection of EventDetailViewModel for specific organization</returns>
        /// <exception cref="NotImplementedException"></exception>
        public IEnumerable<EventDetailViewModel> Get(int id)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Reads detail information about event by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>
        /// the specified item
        /// </returns>
        public EventDetailViewModel ReadById(int id)
        {
            EventDetailViewModel eventDetailViewModel = new EventDetailViewModel();

            var eventDetail = ((DbSet<Event>)_unitOfWork.EventRepository.Read())
                                .Include(e => e.Organization)
                                .Include(im => im.EventImages)
                                .Select(c => new
                                {
                                    Id = c.Id,
                                    OrganizationId = c.OrganizationId,
                                    OrganizationName = c.Organization.Name,
                                    Description = c.Description,
                                    CreateDate = c.CreateDate,
                                    ImageUrl = c.EventImages.Select(i => i.ImageUrl).ToList<string>()
                                }).FirstOrDefault(i => i.Id == id);

            eventDetailViewModel.Id = eventDetail.Id;
            eventDetailViewModel.OrganizationId = eventDetail.OrganizationId;
            eventDetailViewModel.OrganizationName = eventDetail.OrganizationName;
            eventDetailViewModel.Description = eventDetail.Description;
            eventDetailViewModel.CreateDate = eventDetail.CreateDate;
            eventDetailViewModel.ImageUrl = eventDetail.ImageUrl;

            return eventDetailViewModel;
        }

        /// <summary>
        /// Updates the specified update t.
        /// </summary>
        /// <param name="updateT">The update t.</param>
        /// <exception cref="NotImplementedException"></exception>
        public void Update(EventDetailViewModel updateT)
        {
            throw new NotImplementedException();
        }
    }
}
