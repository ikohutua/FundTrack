using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using FundTrack.BLL.Abstract;
using FundTrack.Infrastructure.ViewModel;

namespace FundTrack.WebUI.Controllers
{
    [Route("api/[controller]")]
    public class EventController
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
    }
}
