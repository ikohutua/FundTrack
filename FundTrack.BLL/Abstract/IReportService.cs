using System;
using System.Collections.Generic;
using FundTrack.Infrastructure.ViewModel;
using System.Linq;

namespace FundTrack.BLL.Abstract
{
    public interface IReportService
    {
        IEnumerable<ReportIncomeViewModel> GetIncomeReports(int orgId, DateTime? dateFrom, DateTime? dateTo);
        IEnumerable<ReportOutcomeViewModel> GetOutcomeReports(int orgId, DateTime? dateFrom, DateTime? dateTo);
        IEnumerable<String> GetImagesById(int finOpId);
        IEnumerable<UsersDonationsReportViewModel> GetUsersDonationsPaginatedReportn(int orgId, DateTime dateFrom, DateTime dateTo, int pageIndex, int pageSize);
        int GetCountOfUsersDonationsReport(int orgId, DateTime dateFrom, DateTime dateTo);
    }
}
