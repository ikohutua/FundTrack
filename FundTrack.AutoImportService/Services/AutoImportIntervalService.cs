using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.OleDb;
using System.Data.SqlClient;
using FundTrack.AutoImportService.ViewModels;
using System.Threading;
using System.Data;
using System.Data.Common;

namespace FundTrack.AutoImportService.Services
{
    public class AutoImportIntervalService
    {
        private readonly string _connectionString;
        private List<TimerWithIntervalViewModel> timers;
        TimerService timerService;
        Timer timerForCheckChanges;

        public AutoImportIntervalService()
        {
            _connectionString = ConfigurationManager.ConnectionStrings[Constants.ConnectionStringName].ConnectionString;
            timers = new List<TimerWithIntervalViewModel>();
            timerService = new TimerService();
        }

        public void InitializeTimers()
        {
            timerForCheckChanges = new Timer(CheckForChanges, null, Constants.FiveMinutesInMilliseconds, Constants.FiveMinutesInMilliseconds);
            var intervals = GetIntervals();
            foreach (var interval in intervals)
            {
                timers.Add(timerService.CreateTimer(interval));
            }
        }

        

        public IEnumerable<AutoImportIntervalViewModel> GetIntervals()
        {
            SqlConnection connection = new SqlConnection(_connectionString);
            SqlCommand command = new SqlCommand(Constants.SelectAllIntervalsQuery, connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            AutoImportIntervalViewModel model;
            List<AutoImportIntervalViewModel> intervals = new List<AutoImportIntervalViewModel>();
            while (reader.Read())
            {
                model = new AutoImportIntervalViewModel();
                model.Id = (int)reader[Constants.IdColumn];
                model.Interval = ConvertToMiliseconds((int)reader[Constants.IntervalColumn]);
                model.OrganizationId = (int)reader[Constants.OrgIdColumn];
                //model.LastUpdateDate = (DateTime?)reader[Constants.LastUpdateColumn];
                intervals.Add(model);
            }
            connection.Close();
            return intervals;
        }

        private long ConvertToMiliseconds(int minutes)
        {
            return (long)minutes * 60000;
        }

        private void CheckForChanges(object o)
        {
            var intervals = GetIntervals();
            foreach (var interval in intervals)
            {
                var timer = timers.FirstOrDefault(x => x.IntervalViewModel.Id == interval.Id);
                // If timer not found that it is new interval in db.
                if(timer == null)
                {
                    timerService.CreateTimer(interval);
                    return;
                }

                if (timer.IntervalViewModel.Interval != interval.Interval)
                {
                    timer.IntervalViewModel.Interval = interval.Interval;
                    timer.Timer.Change(interval.Interval, interval.Interval);
                }
            }
        }

    }
}

