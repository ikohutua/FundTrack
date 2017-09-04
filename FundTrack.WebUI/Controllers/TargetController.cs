using System.Collections.Generic;
using FundTrack.BLL.Abstract;
using FundTrack.Infrastructure.ViewModel.EditOrganizationViewModels;
using Microsoft.AspNetCore.Mvc;


namespace FundTrack.WebUI.Controllers
{
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

        [HttpPost("AddTarget")]
        public TargetViewModel AddTarget([FromBody] TargetViewModel target)
        {
            return _targetService.AddTarget(target);
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
    }
}