using System;

namespace FundTrack.Infrastructure.ViewModel
{
    public class ReportOutcomeViewModel
    {
        public int Id { get; set; }
        public string Target { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public decimal MoneyAmount { get; set; }

    }

    public class UsersDonationsReportViewModel : ReportOutcomeViewModel, IComparable<UsersDonationsReportViewModel>
    {
        public string UserLogin { get; set; }
        public string UserFirstName { get; set; }
        public string UserLastName { get; set; }

        public int CompareTo(UsersDonationsReportViewModel other)
        {
            return Date > other.Date ? -1 : Date < other.Date ? 1 : 0;
        }
    }
}
