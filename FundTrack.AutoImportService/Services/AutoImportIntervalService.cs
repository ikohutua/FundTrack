using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.OleDb;
using System.Data.SqlClient;
using FundTrack.AutoImportService.ViewModels;

namespace FundTrack.AutoImportService.Services
{
    public class AutoImportIntervalService
    {
        private string connectionString; 
        private SqlConnection connection;
        private List<TimerWithIntervalViewModel> timers;
        TimerService timerService;

        public AutoImportIntervalService()
        {
            connectionString = ConfigurationManager.ConnectionStrings[Constants.ConnectionStringName].ConnectionString;
            connection = new SqlConnection(connectionString);
            timers = new List<TimerWithIntervalViewModel>();
            timerService = new TimerService();
        }
        
        public void InitializeTimers()
        {
            SqlCommand command = new SqlCommand(Constants.SelectAllIntervalsQuery,connection);
            connection.Open();
            SqlDataReader reader = command.ExecuteReader();
            long interval;
            while(reader.Read())
            {
                interval = ConvertToMiliseconds((int)reader[Constants.IntervalColumn]);
                timers.Add(timerService.CreateTimer((int)reader[Constants.OrgIdColumn], interval));
            }
            connection.Close();
        }

        private long ConvertToMiliseconds(int minutes)
        {
            return (long)minutes * 60000;
        }
    }
}
