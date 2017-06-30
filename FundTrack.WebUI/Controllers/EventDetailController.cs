using FundTrack.BLL.Abstract;
using FundTrack.Infrastructure.ViewModel;
using Microsoft.AspNetCore.Mvc;

namespace FundTrack.WebUI.Controllers
{
    /// <summary>
    /// Controller witch works detail info of event
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.Controller" />
    [Route("api/[controller]")]
    public sealed class EventDetailController
    {
        private readonly IViewService<EventDetailViewModel> _service;

        /// <summary>
        /// Initializes a new instance of the "EventController" class
        /// </summary>
        /// <param name="service">The instance of service.</param>
        public EventDetailController(IViewService<EventDetailViewModel> service)
        {
            _service = service;
        }

        /// <summary>
        /// Events the detail by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>Returns to WEB detail info about specific event</returns>
        [HttpGet("EventDetailById/{id}")]
        public EventDetailViewModel EventDetailById(int id)
        {
            return _service.ReadById(id);
        }
    }
}
