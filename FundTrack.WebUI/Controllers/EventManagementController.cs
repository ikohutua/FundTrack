using FundTrack.BLL.Abstract;
using FundTrack.Infrastructure.ViewModel.EventViewModel;
using Microsoft.AspNetCore.Mvc;
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
        /// <returns> IEnumerable<EventManagementViewModel> </returns>
        [HttpGet("GetAllEventsByOrganizationId/{id}")]
        public IEnumerable<EventManagementViewModel> GetAllEventsByOrganizationId(int id)
        {
            return this._service.GetAllEventsByOrganizationId(id);
        }

        /// <summary>
        /// Gets the one event by identifier.
        /// </summary>
        /// <param name="id">The identifier of event.</param>
        /// <returns> EventManagementViewModel </returns>
        [HttpGet("GetOneEventById/{id}")]
        public EventManagementViewModel GetOneEventById(int id)
        {
            return this._service.GetOneEventById(id);
        }

        /// <summary>
        /// Adds the new event.
        /// </summary>
        /// <param name="newEvent">The new event.</param>
        /// <returns> EventManagementViewModel </returns>
        [HttpPost("[action]")]
        public EventManagementViewModel AddNewEvent([FromBody]EventManagementViewModel newEvent)
        {
            return this._service.AddNewEvent(newEvent);
        }

        /// <summary>
        /// Deletes the event.
        /// </summary>
        /// <param name="id">The identifier.</param>
        [HttpDelete("DeleteEvent/{id}")]
        public void DeleteEvent(int id)
        {
            this._service.DeleteEvent(id);
        }
    }
}
