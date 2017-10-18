using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundTrack.AutoImportService
{
    class Constants
    {
        public const string SelectAllIntervalsQuery = "select * from AutoImportInterval";
        public const string IntervalColumn = "Interval";
        public const string OrgIdColumn = "OrgId";
        public const string ConnectionStringName = "fundtrackss1";
    }
}
