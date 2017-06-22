using FundTrack.BLL.Abstract;
using FundTrack.DAL.Abstract;
using FundTrack.Infrastructure.ViewModel;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using FundTrack.DAL.Entities;

namespace FundTrack.BLL.Concrete
{
    /// <summary>
    /// Class for work with Events
    /// </summary>
    /// <seealso cref="FundTrack.BLL.Abstract.IViewService{FundTrack.Infrastructure.ViewModel.EventViewModel}" />
    public class EventViewService : IViewService<EventViewModel>
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventViewService"/> class.
        /// </summary>
        /// <param name="unitOfWork">The unit of work.</param>
        public EventViewService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        /// <summary>
        /// Adds the specified new event.
        /// </summary>
        /// <param name="newItem">The new event.</param>
        /// <exception cref="NotImplementedException"></exception>
        public void Add(EventViewModel newItem)
        {
            var newEvent = new Event();
            newEvent.Id = newItem.Id;
            newEvent.OrganizationId = newItem.OrganizationId;
            newEvent.Description = newItem.Description;
            newEvent.Description = newItem.Description;
            newEvent.ImageUrl = newItem.ImageUrl;

            _unitOfWork.EventRepository.Create(newEvent);
            _unitOfWork.SaveChanges();
        }

        /// <summary>
        /// Deletes the specified event.
        /// </summary>
        /// <param name="id">The event.</param>
        public void Delete(int id)
        {
            _unitOfWork.EventRepository.Delete(id);
        }

        /// <summary>
        /// Gets this events.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<EventViewModel> Get()
        {
            var events = ((DbSet<Event>)_unitOfWork.EventRepository.Read()).Include(e => e.Organization)
             .Select(c => new EventViewModel()
             {
                 Id = c.Id,
                 OrganizationId = c.OrganizationId,
                 OrganizationName = c.Organization.Name,
                 Description = c.Description,
                 CreateDate = c.CreateDate,
                 ImageUrl = c.ImageUrl
             }).OrderBy(e => e.CreateDate).Take(5);

            return events;
        }

        /// <summary>
        /// Reads the event by identifier.
        /// </summary>
        /// <param name="id">The event.</param>
        /// <returns></returns>
        public EventViewModel ReadById(int id)
        {
            return Get().FirstOrDefault(c => c.Id == id);
        }

        /// <summary>
        /// Updates the specified event
        /// </summary>
        /// <param name="updateItem">The update event.</param>
        public void Update(EventViewModel updateItem)
        {
            var newEvent = new Event();
            newEvent.Id = updateItem.Id;
            newEvent.OrganizationId = updateItem.OrganizationId;
            newEvent.Description = updateItem.Description;
            newEvent.Description = updateItem.Description;
            newEvent.ImageUrl = updateItem.ImageUrl;

            _unitOfWork.EventRepository.Update(newEvent);
            _unitOfWork.SaveChanges();
        }
    }
}