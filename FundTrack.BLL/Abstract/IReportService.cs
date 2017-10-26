using System;
using System.Collections.Generic;
using FundTrack.Infrastructure.ViewModel;
using System.Linq;
using System.Threading.Tasks;

namespace FundTrack.BLL.Abstract
{
    public interface IReportService
    {
        IEnumerable<ReportIncomeViewModel> GetIncomeReports(int orgId, DateTime? dateFrom, DateTime? dateTo);
        IEnumerable<ReportOutcomeViewModel> GetOutcomeReports(int orgId, DateTime? dateFrom, DateTime? dateTo);
        IEnumerable<String> GetImagesById(int finOpId);
        IEnumerable<UsersDonationsReportViewModel> GetUsersDonationsPaginatedReport(int orgId, DateTime dateFrom, DateTime dateTo, int pageIndex, int pageSize);
        int GetCountOfUsersDonationsReport(int orgId, DateTime dateFrom, DateTime dateTo);
        IEnumerable<UsersDonationsReportViewModel> GetFilteredUsersDonationsPaginatedReport(int orgId, DateTime dateFrom, DateTime dateTo, int pageIndex, int pageSize, string filterValue);
        int GetFilteredCountOfUsersDonationsReport(int orgId, DateTime dateFrom, DateTime dateTo, string filterValue);
        IEnumerable<UsersDonationsReportViewModel> GetCommonUsersDonationsPaginatedReport(int orgId, DateTime dateFrom, DateTime dateTo, int pageIndex, int pageSize);
        int GetCountOfCommonUsersDonationsReport(int orgId, DateTime dateFrom, DateTime dateTo);

    }
}
