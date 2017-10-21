using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using FundTrack.BLL.Abstract;
using FundTrack.Infrastructure.ViewModel;
using System.Threading.Tasks;

namespace FundTrack.WebUI.Controllers
{
    [Route("api/[controller]")]
    public class ReportsController : Controller
    {
        private readonly IReportService _service;

        public ReportsController(IReportService service)
        {
            _service = service;
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

        [HttpGet("UsersDonationsPaginatedReport")]
        public IActionResult UsersDonationsReport(int? orgId, DateTime? dateFrom, DateTime? dateTo, int? pageIndex, int? pageSize)
        {
            if (orgId > 0 && dateTo <= DateTime.Now.Date && dateFrom <= dateTo)
            {
                return Ok(_service.GetUsersDonationsPaginatedReportn(orgId.Value, dateFrom.Value, dateTo.Value, pageIndex.Value, pageSize.Value));
            }

            return new BadRequestObjectResult($"Invalid data!");
        }

        [HttpGet("CountOfUsersDonationsReport")]
        public IActionResult CountOfUsersDonationsReport(int? orgId, DateTime? dateFrom, DateTime? dateTo)
        {
            if (orgId > 0 && dateTo <= DateTime.Now.Date && dateFrom <= dateTo)
            {
                return Ok(_service.GetCountOfUsersDonationsReport(orgId.Value, dateFrom.Value, dateTo.Value));
            }

            return new BadRequestObjectResult($"Invalid data!");
        }

    }
}