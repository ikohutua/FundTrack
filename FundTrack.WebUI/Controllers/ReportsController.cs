using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using FundTrack.BLL.Abstract;
using FundTrack.Infrastructure.ViewModel;
using FundTrack.Infrastructure.ViewModel.FinanceViewModels;
using FundTrack.Infrastructure.ViewModel.FinanceViewModels.DonateViewModels;
using Microsoft.AspNetCore.Authorization;

namespace FundTrack.WebUI.Controllers
{
    [Route("api/[controller]")]
    public class ReportsController : Controller
    {
        private readonly IReportService _service;
        private readonly IOrganizationStatisticsService _organizationStatisticsService;

        public ReportsController(IReportService service, IOrganizationStatisticsService organizationStatisticsService)
        {
            _service = service;
            _organizationStatisticsService = organizationStatisticsService;
        }

        [HttpGet("IncomeReport")]
        public IEnumerable<ReportIncomeViewModel> GetIncomeReports(int orgId, DateTime? dateFrom, DateTime? dateTo)
        {
            return _service.GetIncomeReports(orgId, dateFrom, dateTo);
        }

        [HttpGet("OutcomeReport")]
        public IEnumerable<ReportOutcomeViewModel> GetOutcomeReports(int orgId, DateTime? dateFrom, DateTime? dateTo)
        {
            return _service.GetOutcomeReports(orgId, dateFrom, dateTo);
        }

        [HttpGet("FinOpImages")]
        public IEnumerable<String> GetFinOpImages(int finOpId)
        {
            return _service.GetImagesById(finOpId);
        }

        [Authorize(Roles = "admin, moderator")]
        [HttpGet("{orgId}")]
        public IEnumerable<TargetReportViewModel> GetTargetsReport(int orgId, DateTime dateFrom, DateTime dateTo)
        {
            return _organizationStatisticsService.GetReportForIncomeFinopsByTargets(orgId, dateFrom, dateTo);
        }

        [Authorize(Roles = "admin, moderator")]
        [HttpGet("GetSubTargets/{orgId}/{baseTargetId}")]
        public IEnumerable<TargetReportViewModel> GetSubTargets(int orgId, int baseTargetId, DateTime dateFrom, DateTime dateTo)
        {
            return _organizationStatisticsService.GetSubTargets(orgId, baseTargetId, dateFrom, dateTo);
        }

        [Authorize(Roles = "admin, moderator")]
        [HttpGet("GetFinOpsByTargetId/{targetId}")]
        public IEnumerable<FinOpViewModel> GetFinOpsByTargetId(int targetId, DateTime dateFrom, DateTime dateTo)
        {
            return _organizationStatisticsService.GetFinOpsByTargetId(targetId, dateFrom, dateTo);
        }

    }
}