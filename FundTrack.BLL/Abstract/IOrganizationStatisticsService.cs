using System;
using System.Collections.Generic;
using System.Text;
using FundTrack.Infrastructure.ViewModel.FinanceViewModels;
using FundTrack.Infrastructure.ViewModel.FinanceViewModels.DonateViewModels;

namespace FundTrack.BLL.Abstract
{
    public interface IOrganizationStatisticsService
    {
        IEnumerable<TargetReportViewModel> GetReportForFinopsByTargets(int orgId, int finOpType, DateTime dateFrom,
            DateTime dateTo);

        IEnumerable<TargetReportViewModel> GetSubTargets(int orgId, int finOpType, int baseTargetId, DateTime dateFrom,
            DateTime dateTo);

        IEnumerable<FinOpViewModel> GetFinOpsByTargetId(int finOpType, int? targetId, DateTime dateFrom, DateTime dateTo);
    }
}