using FundTrack.DAL.Abstract;
using System;
using System.Collections.Generic;
using System.Text;
using FundTrack.DAL.Entities;
using System.Linq;

namespace FundTrack.DAL.Concrete
{
    public class ImportIntervalRepository : IImportIntervalRepository
    {
        private readonly FundTrackContext _context;

        public ImportIntervalRepository(FundTrackContext context)
        {
            _context = context;
        }
        public AutoImportIntervals Create(AutoImportIntervals item)
        {
            return _context.AutoImportInterval.Add(item).Entity;
        }

        public void Delete(int AutoImportIntervalsId)
        {
            var import = _context.AutoImportInterval.FirstOrDefault(i => i.Id == AutoImportIntervalsId);
            _context.AutoImportInterval.Remove(import);
        }

        public AutoImportIntervals GetByOrgId(int OrgId)
        {
            _context.AutoImportInterval.FirstOrDefault(i => i.OrgId == OrgId);
        }

        public IQueryable<AutoImportIntervals> Read()
        {
            return _context.AutoImportInterval;
        }

        public AutoImportIntervals Update(AutoImportIntervals AutoImportIntervals)
        {
            return _context.Update(AutoImportIntervals).Entity;
        }
    }
}
