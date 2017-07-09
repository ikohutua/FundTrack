using FundTrack.DAL.Abstract;
using FundTrack.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundTrack.DAL.Concrete
{
    public class StatusRepository:IStatusRepository
    {
        private readonly FundTrackContext _context;

        public StatusRepository(FundTrackContext context)
        {
            _context = context;
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
