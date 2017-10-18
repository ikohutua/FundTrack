using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FundTrack.AutoImportService.ViewModels
{
    public class TimerWithIntervalViewModel
    {
        public Timer Timer { get; set; }
        public long Interval { get; set; }
        public int OrganizationId { get; set; }
    }
}
