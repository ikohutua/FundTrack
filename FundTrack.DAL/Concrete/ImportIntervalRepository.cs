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

        public void Delete(int autoImportIntervalsId)
        {
            var import = _context.AutoImportInterval.FirstOrDefault(i => i.Id == autoImportIntervalsId);
            _context.AutoImportInterval.Remove(import);
        }

        public AutoImportIntervals GetByOrgId(int orgId)
        {
           return _context.AutoImportInterval.FirstOrDefault(i => i.OrgId == orgId);
        }

        public IQueryable<AutoImportIntervals> Read()
        {
            return _context.AutoImportInterval;
        }

        public AutoImportIntervals Update(AutoImportIntervals autoImportIntervals)
        {
            return _context.Update(autoImportIntervals).Entity;
        }

        public AutoImportIntervals Update(int orgId, DateTime date)
        {
            var interval = _context.AutoImportInterval.FirstOrDefault(i => i.OrgId == orgId);
            interval.LastUpdateDate = date;
            return _context.Update(interval).Entity;
        }
    }
}
