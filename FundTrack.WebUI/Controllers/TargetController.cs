using System.Collections.Generic;
using FundTrack.BLL.Abstract;
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

        [HttpGet("{id}")]
        public IEnumerable<TargetViewModel> GetTargetsByOrganizationIdWithEditableField(int id)
        {
            return _targetService.GetTargetsByOrganizationIdWithEditableField(id);
        }

        [HttpPost("CreateTarget")]
        public TargetViewModel AddTarget([FromBody] TargetViewModel target)
        {
            return _targetService.CreateTarget(target);
        }

        [HttpDelete("DeleteTarget/{id}")]
        public void DeleteTarget(int id)
        {
            _targetService.DeleteTarget(id);
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