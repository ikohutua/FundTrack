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

        AutoImportIntervals GetByOrgId(int autoImportIntervalsId);

        AutoImportIntervals Create(AutoImportIntervals item);

        AutoImportIntervals Update(AutoImportIntervals autoImportIntervals);

        void Delete(int autoImportIntervalsId);
    }
}