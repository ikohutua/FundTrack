using System.Collections.Generic;
using Microsoft.AspNetCore.Mvc;
using FundTrack.BLL.Abstract;
using FundTrack.Infrastructure.ViewModel.EventViewModel;
using System;
using System.Linq;

namespace FundTrack.WebUI.Controllers
{
    /// <summary>
    /// Controller witch works with list of events
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("api/[controller]")]
    public sealed class EventController
    {
        private readonly IEventService _service;

        /// <summary>
        /// Initializes a new instance of the "EventController" class
        /// </summary>
        /// <param name="service">The instance of service.</param>
        public EventController(IEventService service)
        {
            _service = service;
        }

        /// <summary>
        /// Returns to WEB info about all events
        /// </summary>
        [HttpGet("[action]")]
        public IEnumerable<EventViewModel> AllEvents()
        {
            return _service.GetAllEvents();
        }

        /// <summary>
        /// Returns to WEB info about some count of events (events for one page)
        /// </summary>
        [HttpGet("AllEventsByScroll/{countOfEventsToLoad}/{koefToLoadEvent}")]
        public IEnumerable<EventViewModel> AllEventsbyScroll(int countOfEventsToLoad, int koefToLoadEvent)
        {
            return _service.GetAllEventsByScroll(countOfEventsToLoad, koefToLoadEvent);
        }

        /// <summary>
        /// Alls the events of organization.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Returns to WEB info about all events of specific organization</returns>
        [HttpGet("AllEventsOfOrganization/{id}")]
        public IEnumerable<EventViewModel> AllEventsOfOrganization(int id)
        {
            return _service.GetAllEventsById(id);
        }

        /// <summary>
        /// Gets Events Data for pagination
        /// </summary>
        /// <returns>Events pagination data</returns>
        [HttpGet("[action]")]
        public EventPaginationInitViewModel GetEventsPaginationData()
        {
            return _service.GetEventPaginationData();
        }
    }
}
