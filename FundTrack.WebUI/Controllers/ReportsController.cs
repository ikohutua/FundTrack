using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using FundTrack.BLL.Abstract;
using FundTrack.Infrastructure.ViewModel;
using FundTrack.Infrastructure.ViewModel.FinanceViewModels;
using FundTrack.Infrastructure.ViewModel.FinanceViewModels.DonateViewModels;
using Microsoft.AspNetCore.Authorization;
using System.Threading.Tasks;
using FundTrack.BLL.Concrete;
using System.Diagnostics;
using System.Linq;
using FundTrack.Infrastructure;

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
        [HttpGet("{orgId}/{finOpType}")]
        public IEnumerable<TargetReportViewModel> GetTargetsReport(int orgId, int finOpType, DateTime dateFrom, DateTime dateTo)
        {
            return _organizationStatisticsService.GetReportForIncomeFinopsByTargets(orgId, finOpType, dateFrom, dateTo);
        }

        [Authorize(Roles = "admin, moderator")]
        [HttpGet("GetSubTargets/{orgId}/{finOpType}/{baseTargetId}")]
        public IEnumerable<TargetReportViewModel> GetSubTargets(int orgId, int finOpType, int baseTargetId, DateTime dateFrom, DateTime dateTo)
        {
            return _organizationStatisticsService.GetSubTargets(orgId, finOpType, baseTargetId, dateFrom, dateTo);
        }

        [Authorize(Roles = "admin, moderator")]
        [HttpGet("GetFinOpsByTargetId/{finOpType}/{targetId}")]
        public IEnumerable<FinOpViewModel> GetFinOpsByTargetId(int finOpType, int? targetId, DateTime dateFrom, DateTime dateTo)
        {
            return _organizationStatisticsService.GetFinOpsByTargetId(finOpType, targetId, dateFrom, dateTo);
        }


        [HttpGet("UsersDonationsPaginatedReport")]
        public async Task<IActionResult> UsersDonationsPaginatedReport(int? orgId, DateTime? dateFrom, DateTime? dateTo, int? pageIndex, int? pageSize, string filterValue)
        {
            if (!IsDataNotNull(dateFrom, dateTo, orgId, pageIndex, pageSize))
            {
                return new BadRequestObjectResult(ErrorMessages.InvalidData);
            }

            try
            {
                var list = string.IsNullOrEmpty(filterValue)
                    ? await _service.GetUsersDonationsPaginatedReportAsync(orgId.Value, dateFrom.Value, dateTo.Value, pageIndex.Value, pageSize.Value)
                    : await _service.GetUsersDonationsPaginatedReportByUserLoginAsync(orgId.Value, dateFrom.Value, dateTo.Value, pageIndex.Value, pageSize.Value, filterValue);

                return Ok(list);
            }
            catch (BusinessLogicException ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpGet("CountOfUsersDonationsReport")]
        public async Task<IActionResult> CountOfUsersDonationsReport(int? orgId, DateTime? dateFrom, DateTime? dateTo, string filterValue)
        {
            if (!IsDataNotNull(dateFrom, dateTo, orgId))
            {
                return new BadRequestObjectResult(ErrorMessages.InvalidData);
            }
            try
            {
                var count = string.IsNullOrEmpty(filterValue)
                   ? await _service.GetCountOfUsersDonationsReportAsync(orgId.Value, dateFrom.Value, dateTo.Value)
                   : await _service.GetFilteredCountOfUsersDonationsReportAsync(orgId.Value, dateFrom.Value, dateTo.Value, filterValue);

                return Ok(count);
            }
            catch (BusinessLogicException ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        [HttpGet("CountOfCommonUsersDonationsReport")]
        public async Task<IActionResult> CountOfCommonUsersDonationsReport(int? orgId, DateTime? dateFrom, DateTime? dateTo)
        {
            if (!IsDataNotNull(dateFrom, dateTo, orgId))
            {
                return new BadRequestObjectResult(ErrorMessages.InvalidData);
            }

            try
            {
                var count = await _service.GetCountOfCommonUsersDonationsReportAsync(orgId.Value, dateFrom.Value, dateTo.Value);
                return Ok(count);
            }
            catch (BusinessLogicException ex)
            {
                return new NotFoundObjectResult(ex.Message);
            }
        }

        [HttpGet("CommonUsersDonationsPaginatedReport")]
        public async Task<IActionResult> CommonUsersDonationsPaginatedReport(int? orgId, DateTime? dateFrom, DateTime? dateTo, int? pageIndex, int? pageSize)
        {
            if (!IsDataNotNull(dateFrom, dateTo, orgId, pageIndex, pageSize))
            {
                return new BadRequestObjectResult(ErrorMessages.InvalidData);
            }

            try
            {
                var list = await _service.GetCommonUsersDonationsPaginatedReportAsync(orgId.Value, dateFrom.Value, dateTo.Value, pageIndex.Value, pageSize.Value);
                return Ok(list);
            }
            catch (BusinessLogicException ex)
            {
                return new NotFoundObjectResult(ex.Message);
            }
        }

        [HttpGet("DonationsValueReportPerDay")]
        public async Task<IActionResult> DonationsValueReportPerDay(int? orgId, DateTime? dateFrom, DateTime? dateTo, int filterValue)
        {
            if (!IsDataNotNull(dateFrom, dateTo, orgId))
            {
                return new BadRequestObjectResult(ErrorMessages.InvalidData);
            }
            try
            {
                var dataSet = filterValue <= 0
                    ? await _service.GetDonationsReportPerDayAsync(orgId.Value, dateFrom.Value, dateTo.Value)
                    : await _service.GetDonationsReportPerDayByTargetAsync(orgId.Value, dateFrom.Value, dateTo.Value, filterValue);

                return Ok(dataSet);
            }
            catch (BusinessLogicException ex)
            {
                return new BadRequestObjectResult(ex.Message);
            }
        }

        private static bool IsDataNotNull(DateTime? dateFrom, DateTime? dateTo, params int?[] parametrs)
        {
            var res = dateTo.HasValue && dateTo <= DateTime.Now.Date && dateFrom.HasValue && dateFrom.Value <= dateTo;
            return parametrs.Aggregate(res, (current, item) => current & item.HasValue);
        }
    }
}