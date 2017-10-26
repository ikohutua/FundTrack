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
        public const string UpdateQuerySet = "update AutoImportInterval set LastUpdateDate = @dateParam where OrgId = @orgId";
        public const string UpdateQueryWhere = " where OrgId = " ;
        public const string IntervalColumn = "Interval";
        public const string OrgIdColumn = "OrgId";
        public const string IdColumn = "Id";
        public const string LastUpdateColumn = "LastUpdateDate";

        public const string ConnectionStringName = "fundtrackss1";
        public const long FiveMinutesInMilliseconds = 300000;
    }
}
