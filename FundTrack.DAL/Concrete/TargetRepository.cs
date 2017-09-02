using FundTrack.DAL.Abstract;
using FundTrack.DAL.Entities;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace FundTrack.DAL.Concrete
{
    public class TargetRepository : ITargetRepository
    {
        private readonly FundTrackContext _context;

        /// <summary>
        /// Initializes a new instance of the <see cref="TargetRepository"/> class.
        /// </summary>
        /// <param name="context">The context.</param>
        public TargetRepository(FundTrackContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Get all targets.
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Target> Read()
        {
            return _context.Targets;
        }

        /// <summary>
        /// Gets the target by name.
        /// </summary>
        /// <param name="name">The name.</param>
        /// <returns></returns>
        public Target GetTargetByName(string name)
        {
            return _context.Targets
                           .FirstOrDefault(t => t.TargetName == name);
        }

        public IEnumerable<Target> GetTargetsByOrganizationId(int id)
        {
            return _context.Targets.Where(t => t.OrganizationId == id);
        }
    }
}
