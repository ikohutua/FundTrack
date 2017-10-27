using System;
using Microsoft.AspNetCore.Mvc;
using FundTrack.BLL.Abstract;
using FundTrack.Infrastructure;
using Microsoft.AspNetCore.Authorization;
using FundTrack.Infrastructure.ViewModel;
using FundTrack.Infrastructure.ViewModel.FinanceViewModels;
using FundTrack.Infrastructure.ViewModel.FinanceViewModels.DonateViewModels;

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
        public ActionResult GetIncomeReports(int orgId, DateTime? dateFrom, DateTime? dateTo)
        { 
            if (isDateValid(dateFrom, dateTo) && isIdValid(orgId))
            {
                return Ok(_service.GetIncomeReports(orgId, dateFrom, dateTo));
            }           
                return BadRequest(string.Format(ErrorMessages.IncomeReportErrorMessage, orgId, dateFrom, dateTo));     
        }

        [HttpGet("OutcomeReport")]
        public ActionResult GetOutcomeReports(int orgId, DateTime? dateFrom, DateTime? dateTo)
        {
            if (isDateValid(dateFrom, dateTo) && isIdValid(orgId))
            {
                return Ok(_service.GetOutcomeReports(orgId, dateFrom, dateTo));
            }
            return BadRequest(string.Format(ErrorMessages.OutcomeReportErrorMessage, orgId, dateFrom, dateTo));
        }

        [HttpGet("FinOpImages")]
        public ActionResult GetFinOpImages(int finOpId)
        {
            if (isIdValid(finOpId))
            {
                return Ok(_service.GetImagesById(finOpId));
            }
            return BadRequest(string.Format(ErrorMessages.FinopImagesErrorMessage, finOpId));
            
        }

        [HttpGet("InvoiceDeclaration")]
        [Authorize(Roles = "admin, moderator")]
        public ActionResult GetInvoiceDeclarationReport(int orgId, DateTime? dateFrom, DateTime? dateTo)
        {
            if (isDateValid(dateFrom, dateTo) && isIdValid(orgId))
            {
                return Ok(_service.GetInvoiceDeclarationReport(orgId,dateFrom,dateTo));
            }
            return BadRequest(string.Format(ErrorMessages.InvoiceDeclarationReportErrorMessage, orgId, dateFrom, dateTo));
        }

        private bool isDateValid(DateTime? dateFrom, DateTime? dateTo)
        {
            if (dateTo == null)
            {        
                return false;
            }
            return dateFrom != null;
        }
        private bool isIdValid(int id)
        {
            return id > 0;
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
        public IEnumerable<FinOpViewModel> GetFinOpsByTargetId(int finOpType, int targetId, DateTime dateFrom, DateTime dateTo)
        {
            return _organizationStatisticsService.GetFinOpsByTargetId(finOpType, targetId, dateFrom, dateTo);
        }

    }
}