using System;
using Microsoft.AspNetCore.Mvc;
using FundTrack.BLL.Abstract;
using FundTrack.Infrastructure;
using Microsoft.AspNetCore.Authorization;

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
    }
}