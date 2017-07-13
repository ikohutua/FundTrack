using FundTrack.DAL.Abstract;
using FundTrack.DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
namespace FundTrack.DAL.Concrete
{
    /// <summary>
    /// Class for work with status repository
    /// </summary>
    public class StatusRepository : IStatusRepository
    {
        private readonly FundTrackContext _context;

        /// <summary>
        /// Initialize new instance if StatusRepository class
        /// </summary>
        /// <param name="context"></param>
        public StatusRepository(FundTrackContext context)
        {
            this._context = context;
        }

        /// <summary>
        /// Create status in database
        /// </summary>
        /// <param name="item">Item to add</param>
        /// <returns>Status entity</returns>
        public Status Create(Status item)
        {
            var createdStatus = this._context.Statuses.Add(item);

            return createdStatus.Entity;            
        }

        /// <summary>
        /// Delete status from database
        /// </summary>
        /// <param name="id">Id of status</param>
        public void Delete(int id)
        {
            var statusToDelete = this.Get(id);

            this._context.Statuses.Remove(statusToDelete);
        }

        /// <summary>
        /// Gets status from database
        /// </summary>
        /// <param name="id">Id of status</param>
        /// <returns>Status entity</returns>
        public Status Get(int id)
        {
            var status = this._context.Statuses
                .Include(s => s.OfferedItems)
                .Include(s => s.RequestedItems)
                .FirstOrDefault(s => s.Id == id);

            return status;
        }        

        /// <summary>
        /// Gets all statuses from dtabase
        /// </summary>
        /// <returns>List of statuses</returns>
        public IEnumerable<Status> Read()
        {
            var statuses = this._context.Statuses
                .Include(s => s.OfferedItems)
                .Include(s => s.RequestedItems);

            return statuses;
        }

        /// <summary>
        /// Update status in database
        /// </summary>
        /// <param name="item">Status to update</param>
        /// <returns>Updated status</returns>
        public Status Update(Status item)
        {
            var statusToUpdate = this._context.Statuses.Update(item);

            return statusToUpdate.Entity;
        }

        /// <summary>
        /// Gets status by its name
        /// </summary>
        /// <param name="name">Name of the status</param>
        /// <returns>Status entity</returns>
        public Status GetStatusByName(string name)
        {
            return this._context.Statuses.FirstOrDefault(s => s.StatusName == name);
        }

        /// <summary>
        /// Gets status by its id
        /// </summary>
        /// <param name="id">Id of the status</param>
        /// <returns>Status entity</returns>
        public Status GetStatusById(int id)
        {
            return this._context.Statuses.FirstOrDefault(s => s.Id == id);
        }
    }
}
