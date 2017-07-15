using FundTrack.BLL.Abstract;
using FundTrack.Infrastructure.ViewModel.EventViewModel;
using FundTrack.Infrastructure.ViewModel.SuperAdminViewModels;
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
        [HttpGet("[action]/{idOrganization}/{currentPage}/{pageSize}")]
        public IEnumerable<EventManagementViewModel> GetEventsByOrganizationIdForPage(int idOrganization, int currentPage, int pageSize)
        {
            return this._service.GetEventsByOrganizationIdForPage(idOrganization, currentPage, pageSize);
        }

        /// <summary>
        /// Gets the one event by identifier.
        /// </summary>
        /// <param name="id">The identifier of event.</param>
        /// <returns> EventManagementViewModel </returns>
        [HttpGet("[action]/{idOrganization}")]
        public EventManagementViewModel GetOneEventById(int idOrganization)
        {
            return this._service.GetOneEventById(idOrganization);
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
        [HttpDelete("[action]/{id}")]
        public void DeleteEvent(int id)
        {
            this._service.DeleteEvent(id);
        }

        /// <summary>
        /// Updates the event.
        /// </summary>
        /// <param name="updatedEvent">The updated event.</param>
        /// <returns> EventManagementViewModel </returns>
        [HttpPut("[action]")]
        public EventManagementViewModel UpdateEvent([FromBody]EventManagementViewModel updatedEvent)
        {
            return this._service.UpdateEvent(updatedEvent);
        }

        /// <summary>
        /// Gets Events Data for pagination
        /// </summary>
        /// <returns>Events pagination data</returns>
        [HttpGet("[action]/{idOrganization}")]
        public PaginationInitViewModel GetEventsInitData(int idOrganization)
        {
            return this._service.GetEventsInitData(idOrganization);
        }
    }
}
