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
        IEnumerable<InvoiceDeclarationReportViewModel> GetInvoiceDeclarationReport(int orgId, DateTime? dateFrom, DateTime? dateTo);
        Task<IEnumerable<UsersDonationsReportViewModel>> GetUsersDonationsPaginatedReportAsync(int orgId, DateTime dateFrom, DateTime dateTo, int pageIndex, int pageSize);
        Task<int> GetCountOfUsersDonationsReportAsync(int orgId, DateTime dateFrom, DateTime dateTo);
        Task<IEnumerable<UsersDonationsReportViewModel>> GetUsersDonationsPaginatedReportByUserLoginAsync(int orgId, DateTime dateFrom, DateTime dateTo, int pageIndex, int pageSize, string filterValue);
        Task<int> GetFilteredCountOfUsersDonationsReportAsync(int orgId, DateTime dateFrom, DateTime dateTo, string filterValue);
        Task<IEnumerable<UsersDonationsReportViewModel>> GetCommonUsersDonationsPaginatedReportAsync(int orgId, DateTime dateFrom, DateTime dateTo, int pageIndex, int pageSize);
        Task<int> GetCountOfCommonUsersDonationsReportAsync(int orgId, DateTime dateFrom, DateTime dateTo);
        Task<IEnumerable<DataSetViewModel>> GetDonationsReportPerDayAsync(int orgId, DateTime dateFrom, DateTime dateTo);
        Task<IEnumerable<DataSetViewModel>> GetDonationsReportPerDayByTargetAsync(int orgId, DateTime dateFrom, DateTime dateTo, int targetId);

    }
}
