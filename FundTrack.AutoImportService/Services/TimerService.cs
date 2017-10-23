using FundTrack.AutoImportService.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Threading;
using FundTrack.PrivatImport;


namespace FundTrack.AutoImportService.Services
{
    class TimerService
    {
        /// <summary>
        /// Create new timer
        /// </summary>
        /// <param name="intervalModel">View model with information about interval from db.</param>
        /// <returns>New timer with information about interval.</returns>
        public TimerWithIntervalViewModel CreateTimer(AutoImportIntervalViewModel intervalModel)
        {
            TimerWithIntervalViewModel timer = new TimerWithIntervalViewModel();
            timer.IntervalViewModel = intervalModel;
            timer.Timer = new Timer(ImportDataFromBank, timer, 0, intervalModel.Interval);
            return timer;
        }

        /// <summary>
        /// Import data from privat 24. Called when the timer is triggered.
        /// </summary>
        /// <param name="o">Object from timer.</param>
        private void ImportDataFromBank(object o)
        {
            TimerWithIntervalViewModel thisTimer = (TimerWithIntervalViewModel)o;
            int intervalInMinutes = (int)ConvertToMinutes(thisTimer.IntervalViewModel.Interval);
            PrivatImporter.Run(thisTimer.IntervalViewModel.OrganizationId, intervalInMinutes);
        }

        private long ConvertToMinutes(long milliseconds)
        {
            return milliseconds / 60000;
        }
    }
}
