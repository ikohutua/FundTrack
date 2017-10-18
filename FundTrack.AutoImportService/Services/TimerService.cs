using FundTrack.AutoImportService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using pPrivatLib2;


namespace FundTrack.AutoImportService.Services
{
    class TimerService
    {
        public TimerWithIntervalViewModel CreateTimer(int organizationId, long interval)
        {
            TimerWithIntervalViewModel timer = new TimerWithIntervalViewModel();
            timer.Interval = interval;
            timer.OrganizationId = organizationId;
            timer.Timer = new Timer(ImportDataFromBank, timer, 0, interval);
            return timer;
        }

        private void ImportDataFromBank(object o)
        {
            Console.WriteLine("Go");
            TimerWithIntervalViewModel thisTimer = (TimerWithIntervalViewModel)o;
            long intervalInMinutes = thisTimer.Interval / 60000;
            //PrivatImporter.Run(thisTimer.OrganizationId, (int)intervalInMinutes);
            Console.WriteLine(thisTimer.OrganizationId);
            Console.WriteLine(DateTime.Now);
        }
    }
}
