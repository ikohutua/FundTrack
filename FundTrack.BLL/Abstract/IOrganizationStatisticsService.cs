using System;
using System.Collections.Generic;
using System.Text;
using FundTrack.Infrastructure.ViewModel.FinanceViewModels;
using FundTrack.Infrastructure.ViewModel.FinanceViewModels.DonateViewModels;

namespace FundTrack.BLL.Abstract
{
    public interface IOrganizationStatisticsService
    {
        IEnumerable<TargetReportViewModel> GetReportForIncomeFinopsByTargets(int orgId, DateTime dateFrom,
            DateTime dateTo);

        IEnumerable<TargetReportViewModel> GetSubTargets(int orgId, int baseTargetId, DateTime dateFrom,
            DateTime dateTo);

        IEnumerable<FinOpViewModel> GetFinOpsByTargetId(int targetId, DateTime dateFrom, DateTime dateTo);
    }
}