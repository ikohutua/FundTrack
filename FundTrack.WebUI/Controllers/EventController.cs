using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using FundTrack.BLL.Abstract;
using FundTrack.Infrastructure.ViewModel;

namespace FundTrack.WebUI.Controllers
{
    /// <summary>
    /// Controller witch works with list of events
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("api/[controller]")]
    public sealed class EventController
    {
        private readonly IViewService<EventViewModel> _service;

        /// <summary>
        /// Initializes a new instance of the "EventController" class
        /// </summary>
        /// <param name="service">The instance of service.</param>
        public EventController(IViewService<EventViewModel> service)
        {
            _service = service;
        }

        /// <summary>
        /// Returns to WEB info about all events
        /// </summary>
        [HttpGet("[action]")]
        public IEnumerable<EventViewModel> AllEvents()
        {
            return _service.Get();
        }

        /// <summary>
        /// Alls the events of organization.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Returns to WEB info about all events of specific organization</returns>
        [HttpGet("AllEventsOfOrganization/{id}")]
        public IEnumerable<EventViewModel> AllEventsOfOrganization(int id)
        {
            return _service.Get(id);
        }
    }
}
