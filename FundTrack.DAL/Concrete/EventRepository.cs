using FundTrack.DAL.Abstract;
using FundTrack.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;
using System;

namespace FundTrack.DAL.Concrete
{
    /// <summary>
    /// class for CRUD operation with entity - event
    /// </summary>
    /// <seealso cref="FundTrack.DAL.Abstract.IRepository{FundTrack.DAL.Entities.User}" />
    public sealed class EventRepository : IEventManagementRepository
    {
        private readonly FundTrackContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventRepsitory"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public EventRepository(FundTrackContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates the event.
        /// </summary>
        /// <param name="item">The event.</param>
        /// <returns></returns>
        public Event Create(Event item)
        {
            var created = _context.Events.Add(item);
            return created.Entity;
        }

        /// <summary>
        /// Deletes event from data base
        /// </summary>
        /// <param name="id">Recives id of event</param>
        public void Delete(int id)
        {
            Event _event = _context.Events.FirstOrDefault(c => c.Id == id);
            if (_event != null)
                _context.Events.Remove(_event);
        }

        /// <summary>
        /// Gets the event by id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public Event Get(int id)
        {
            return _context.Events.FirstOrDefault(e => e.Id == id);
        }

        /// <summary>
        /// Gets all events in database
        /// </summary>
        /// <returns>
        /// Collection all events
        /// </returns>
        public IEnumerable<Event> Read()
        {
            return _context.Events;
        }

        /// <summary>
        /// Updates the specified event.
        /// </summary>
        /// <param name="item">The event.</param>
        public Event Update(Event item)
        {
            _context.Update(item);
            return item;
        }

        /// <summary>
        /// Gets the amount of events for the page by the organization ID
        /// Join with event images
        /// </summary>
        /// <param name="organizationId">The organization identifier.</param>
        /// <returns> IEnumerable<Event> </returns>
        public IEnumerable<Event> GetEventsByOrganizationIdForPage(int organizationId, int currentPage, int itemsPerPage)
        {
            return this._context.Events
                .Include(im => im.EventImages)
                .Where(ev => ev.OrganizationId == organizationId)
                .Skip((currentPage - 1) * itemsPerPage)
                .Take(itemsPerPage);
        }

        /// <summary>
        /// Gets the event by ID
        /// Join with event images
        /// </summary>
        /// <param name="organizationId">The event identifier.</param>
        /// <returns> Event </returns>
        public Event GetOneEventById(int eventId)
        {
            return this._context.Events
                .Include(im => im.EventImages)
                .Where(ev => ev.Id == eventId)
                .FirstOrDefault();
        }
    }
}
