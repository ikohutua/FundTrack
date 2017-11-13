using System;
using System.Collections.Generic;
using FundTrack.BLL.Abstract;
using FundTrack.BLL.Concrete;
using FundTrack.Infrastructure;
using Microsoft.AspNetCore.Mvc;
using FundTrack.Infrastructure.ViewModel.FinanceViewModels.DonateViewModels;

namespace FundTrack.WebUI.Controllers
{
    [Route("api/Target")]
    public class TargetController : Controller
    {
        private readonly ITargetService _targetService;

        public TargetController(ITargetService targetService)
        {
            _targetService = targetService;
        }

        [HttpGet("GetTarget/{id}")]
        public TargetViewModel GetTarget(int id)
        {
            return _targetService.GetTargetById(id);
        }

        [HttpGet("GetAllTargetsOfOrganization/{id}")]
        public IEnumerable<TargetViewModel> GetTargetsByOrganizationId(int id)
        {
            return _targetService.GetTargetsByOrganizationId(id);
        }

        [HttpGet("withDeletable/{id}")]
        public IActionResult GetTargetsByOrganizationIdWithEditableField(int id)
        {
            try
            {
                var res = Ok(_targetService.GetTargetsByOrganizationIdWithEditableField(id));
                return res;
            }
            catch (BusinessLogicException e)
            {
                return BadRequest(e.Message);
            }
        }

        [HttpPost("CreateTarget")]
        public IActionResult AddTarget([FromBody] TargetViewModel target)
        {
            return Ok(_targetService.CreateTarget(target));
        }

        [HttpDelete("DeleteTarget/{id}")]
        public IActionResult DeleteTarget(int id)
        {
            var res = _targetService.DeleteTarget(id);
            if (res)
            {
                return Ok(res);
            }
            else
            {
                return BadRequest(ErrorMessages.DeleteDataError);
            }
            
        }

        [HttpPut("EditTarget")]
        public TargetViewModel EditTarget([FromBody] TargetViewModel target)
        {
            return _targetService.EditTarget(target);
        }

        [HttpGet("{orgId}/{parentId}")]
        public IEnumerable<TargetViewModel> GetTargets(int orgId, int parentId)
        {
            return _targetService.GetTargets(orgId, parentId);
        }

        [HttpGet("{orgId}")]
        public IEnumerable<TargetViewModel> GetParentTargets(int orgId)
        {
            return _targetService.GetTargets(orgId);
        }
    }
}