using FundTrack.DAL.Abstract;
using FundTrack.DAL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace FundTrack.DAL.Concrete
{
    public class TargetRepository : ITargetRepository
    {
        private readonly FundTrackContext _context;

        public TargetRepository(FundTrackContext context)
        {
            _context = context;
        }

        public IEnumerable<Target> Read()
        {
            return _context.Targets;
        }

        public Target GetTargetByName(string name)
        {
            return _context.Targets
                           .FirstOrDefault(t => t.TargetName == name);
        }
    }
}
