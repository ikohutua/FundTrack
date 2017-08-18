using FundTrack.BLL.Abstract;
using FundTrack.Infrastructure.ViewModel;
using FundTrack.Infrastructure.ViewModel.FinanceViewModels;
using FundTrack.Infrastructure.ViewModel.FinanceViewModels.DonateViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace FundTrack.WebUI.Controllers
{
    [Route("api/[controller]")]
    public class FinOpController : Controller
    {

        /// <summary>
        /// The service
        /// </summary>
        private readonly IFinOpService _service;

        /// <summary>
        /// Initializes a new instance of the "EventController" class
        /// </summary>
        /// <param name="service">The instance of service.</param>
        public FinOpController(IFinOpService service)
        {
            _service = service;
        }

        /// <summary>
        /// Gets the targets.
        /// </summary>
        /// <returns></returns>
        [HttpGet("GetTargets")]
        [Authorize(Roles = "admin, moderator")]
        public IEnumerable<TargetViewModel> GetTargets()
        {
            return this._service.GetTargets();
        }

        /// <summary>
        /// Creates the fin op.
        /// </summary>
        /// <param name="finOpModel">The fin op model.</param>
        /// <returns></returns>
        [HttpPost("CreateFinOp")]
        [Authorize(Roles = "admin, moderator")]
        public FinOpViewModel CreateFinOp([FromBody] FinOpViewModel finOpModel)
        {
            return this._service.CreateFinOp(finOpModel);
        }
    }
}
