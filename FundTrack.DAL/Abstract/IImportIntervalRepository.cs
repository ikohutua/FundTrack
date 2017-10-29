using FundTrack.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace FundTrack.DAL.Abstract
{
    public interface IImportIntervalRepository
    {
        IQueryable<AutoImportIntervals> Read();

        AutoImportIntervals GetByOrgId(int AutoImportIntervalsId);

        AutoImportIntervals Create(AutoImportIntervals item);

        AutoImportIntervals Update(AutoImportIntervals AutoImportIntervals);

        void Delete(int AutoImportIntervalsId);
    }
}