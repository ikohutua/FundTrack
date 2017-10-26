using System;
using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using FundTrack.BLL.Abstract;
using FundTrack.Infrastructure.ViewModel;
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
                return BadRequest("Invalid data: organization ID='"+orgId+ "'; BeginDate='"+dateFrom+ "'; EndDate='"+dateTo+"';");     
        }

        [HttpGet("OutcomeReport")]
        public ActionResult GetOutcomeReports(int orgId, DateTime? dateFrom, DateTime? dateTo)
        {
            if (isDateValid(dateFrom, dateTo) && isIdValid(orgId))
            {
                return Ok(_service.GetOutcomeReports(orgId, dateFrom, dateTo));
            }
            return BadRequest("Invalid data: organization ID='" + orgId + "'; BeginDate='" + dateFrom + "'; EndDate='" + dateTo + "';");
        }

        [HttpGet("FinOpImages")]
        public ActionResult GetFinOpImages(int finOpId)
        {
            if (isIdValid(finOpId))
            {
                return Ok(_service.GetImagesById(finOpId));
            }
            return BadRequest("Invalid finOpId: finOpId='" + finOpId + "'; Must be grater than 0;");
            
        }

        [HttpGet("InvoiceDeclaration")]
        //[Authorize(Roles = "admin, moderator")]
        public ActionResult GetInvoiceDeclarationReport(int orgId, DateTime? dateFrom, DateTime? dateTo)
        {
            if (isDateValid(dateFrom, dateTo) && isIdValid(orgId))
            {
                return Ok(_service.GetInvoiceDeclarationReport(orgId,dateFrom,dateTo));
            }
            return BadRequest("Invalid orgId: orgId='" + orgId + "'; Must be grater than 0;");
        }

        private bool isDateValid(DateTime? dateFrom, DateTime? dateTo)
        {
            if (dateTo == null)
            {        
                return false;
            }
            if (dateFrom == null)
            {
                return false;
            }
            return true;
        }
        private bool isIdValid(int Id)
        {
            if (Id > 0)
            {
                return true;
            }
           return false;
        }
    }
}