using FundTrack.BLL.Abstract;
using FundTrack.Infrastructure.ViewModel;
using FundTrack.Infrastructure.ViewModel.EventViewModel;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace FundTrack.WebUI.Controllers
{
    /// <summary>
    /// Authorize controller for work with event management
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    //[Authorize]
    [Route("api/[controller]")]
    public sealed class EventManagementController : Controller
    {
        private readonly IEventManagementService _service;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventManagementController"/> class.
        /// </summary>
        /// <param name="_serviceParam">The service parameter.</param>
        public EventManagementController(IEventManagementService serviceParam)
        {
            this._service = serviceParam;
        }

        /// <summary>
        /// Gets all events by organization identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>IEnumerable<EventManagementViewModel></returns>
        [HttpGet("GetAllEventsByOrganizationId/{id}")]
        public IEnumerable<EventManagementViewModel> GetAllEventsByOrganizationId(int id)
        {
            return this._service.GetAllEventsByOrganizationId(id);
        }
    }
}
