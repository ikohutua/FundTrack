using FundTrack.DAL.Abstract;
using FundTrack.DAL.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

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
        /// </summary>
        /// <param name="item"></param>
        /// <returns>Event</returns>
        /// <exception cref="DataAccessException"></exception>
        public Event Create(Event item)
        {
            try
            {
                var created = _context.Events.Add(item);
                if (created == null)
                {
                    throw new DataAccessException("Подія не буда додана до бази даних");
                }
                return created.Entity;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex.Message);
            }
        }

        /// <summary>
        /// Deletes entry from data base
        /// </summary>
        /// <param name="id">Recives id of entry</param>
        /// <exception cref="DataAccessException"></exception>
        public void Delete(int id)
        {
            try
            {
                var _event = _context.Events.FirstOrDefault(c => c.Id == id);
                if (_event == null)
                {
                    throw new DataAccessException($"Події з таким ідентифікатором {id} в базі не існує");
                }
                _context.Events.Remove(_event);
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex.Message);
            }
        }

        /// <summary>Gets one event by identifier</summary>
        /// <param name="id"></param>
        /// <returns>Event</returns>
        /// <exception cref="DataAccessException"></exception>
        public Event Get(int id)
        {
            try
            {
                var result = _context.Events.FirstOrDefault(e => e.Id == id);
                if (result == null)
                {
                    throw new DataAccessException($"В базі даних немає події з ідентифікатором {id}");
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex.Message);
            }
        }

        /// <summary>
        /// Gets all entries in database
        /// </summary>
        /// <returns>
        /// Collection all entries
        /// </returns>
        /// <exception cref="DataAccessException"></exception>
        public IEnumerable<Event> Read()
        {
            try
            {
                var result = _context.Events.Include(e => e.Organization)
                 .Include(e => e.Organization.BannedOrganization)
                 .Where(e => e.Organization.BannedOrganization == null)
                 .Include(i => i.EventImages);
                if (result == null)
                {
                    throw new DataAccessException("В базі даних немає жодної події");
                }
                return result;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex.Message);
            }
        }

        /// <summary>Updates the concrete event</summary>
        /// <param name="item"></param>
        /// <returns>Event</returns>
        /// <exception cref="DataAccessException"></exception>
        public Event Update(Event item)
        {
            try
            {
                var result = _context.Update(item);
                if (result == null)
                {
                    throw new DataAccessException($"Не вдалось оновити подію з ідентифікатором {item.Id}");
                }
                return result.Entity;
            }
            catch (Exception ex)
            {
                throw new DataAccessException(ex.Message);
            }
        }

        /// <summary>
        /// Gets events by organization identifier for current page
        /// </summary>
        /// <param name="organizationId"></param>
        /// <param name="currentPage"></param>
        /// <param name="itemsPerPage"></param>
        /// <returns>IEnumerable<Event></returns>
        /// <exception cref="DataAccessException"></exception>
        public IEnumerable<Event> GetEventsByOrganizationIdForPage(int organizationId, int currentPage, int itemsPerPage)
        {
            try
            {
                return this._context.Events
                    .Include(im => im.EventImages)
                    .Where(ev => ev.OrganizationId == organizationId)
                    .OrderByDescending(ev => ev.CreateDate)
                    .Skip((currentPage - 1) * itemsPerPage)
                    .Take(itemsPerPage);
            }
            catch(Exception ex)
            {
                throw new DataAccessException(ex.Message);
            }
        }

        /// <summary>
        /// Gets the event by ID
        /// Join with event images
        /// </summary>
        /// <param name="eventId"></param>
        /// <returns>Event</returns>
        /// <exception cref="DataAccessException"></exception>
        public Event GetOneEventById(int eventId)
        {
            try
            {
                return this._context.Events
                    .Include(im => im.EventImages)
                    .Where(ev => ev.Id == eventId)
                    .FirstOrDefault();
            }
            catch(Exception ex)
            {
                throw new DataAccessException(ex.Message);
            }
        }
    }
}
