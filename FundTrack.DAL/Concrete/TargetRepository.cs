using FundTrack.DAL.Abstract;
using FundTrack.DAL.Entities;
using System.Collections.Generic;

namespace FundTrack.DAL.Concrete
{
    public class TargetRepository: ITargetRepository
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
    }
}
