using FundTrack.BLL.Abstract;
using FundTrack.Infrastructure.ViewModel.EventViewModel;
using FundTrack.Infrastructure.ViewModel.SuperAdminViewModels;
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
        /// Gets events by organization identifier for current page.
        /// </summary>
        /// <param name="idOrganization">The identifier organization.</param>
        /// <param name="currentPage">The current page.</param>
        /// <param name="pageSize">Size of the page.</param>
        /// <returns>IEnumerable<EventManagementViewModel></returns>
        [HttpGet("[action]/{idOrganization}/{currentPage}/{pageSize}")]
        public IEnumerable<EventManagementViewModel> GetEventsByOrganizationIdForPage(int idOrganization, int currentPage, int pageSize)
        {
            try
            {
                return this._service.GetEventsByOrganizationIdForPage(idOrganization, currentPage, pageSize);
            }
            catch (Exception ex)
            {
                return new List<EventManagementViewModel>()
                {
                    new EventManagementViewModel()
                    {
                        ErrorMessage = ex.Message
                    }
                };
            }
        }

        /// <summary>
        /// Gets the event by identifier.
        /// </summary>
        /// <param name="idOrganization">The identifier organization.</param>
        /// <returns>EventManagementViewModel</returns>
        [HttpGet("[action]/{idOrganization}")]
        public EventManagementViewModel GetOneEventById(int idOrganization)
        {
            try
            {
                return this._service.GetOneEventById(idOrganization);
            }
            catch (Exception ex)
            {
                return new EventManagementViewModel()
                {
                    ErrorMessage = ex.Message
                };
            }
        }

        /// <summary>
        /// Adds the new event.
        /// </summary>
        /// <param name="newEvent">The new event.</param>
        /// <returns>EventManagementViewModel</returns>
        [HttpPost("[action]")]
        public EventManagementViewModel AddNewEvent([FromBody]EventManagementViewModel newEvent)
        {
            try
            {
                return this._service.AddNewEvent(newEvent);
            }
            catch (Exception ex)
            {
                return new EventManagementViewModel()
                {
                    ErrorMessage = ex.Message
                };
            }
        }

        /// <summary>
        /// Deletes the event.
        /// </summary>
        /// <param name="id">The identifier.</param>
        [HttpDelete("[action]/{id}")]
        public JsonResult DeleteEvent(int id)
        {
            try
            {
                this._service.DeleteEvent(id);
                return new JsonResult(string.Empty);
            }
            catch (Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }

        /// <summary>
        /// Updates the event.
        /// </summary>
        /// <param name="updatedEvent">The updated event.</param>
        /// <returns>EventManagementViewModel</returns>
        [HttpPut("[action]")]
        public EventManagementViewModel UpdateEvent([FromBody]EventManagementViewModel updatedEvent)
        {
            try
            {
                return this._service.UpdateEvent(updatedEvent);
            }
            catch (Exception ex)
            {
                return new EventManagementViewModel()
                {
                    ErrorMessage = ex.Message
                };
            }
        }

        /// <summary>
        /// Gets the events initialize data.
        /// </summary>
        /// <param name="idOrganization">The identifier organization.</param>
        /// <returns>PaginationInitViewModel</returns>
        [HttpGet("[action]/{idOrganization}")]
        public PaginationInitViewModel GetEventsInitData(int idOrganization)
        {
            return this._service.GetEventsInitData(idOrganization);
        }

        /// <summary>
        /// Deletes the current image.
        /// </summary>
        /// <param name="idImage">The identifier for image.</param>
        /// <returns>JsonResult</returns>
        [HttpDelete("[action]/{idImage}")]
        public JsonResult DeleteCurrentImage(int idImage)
        {
            try
            {
                this._service.DeleteCurrentImage(idImage);
                return new JsonResult(string.Empty);
            }
            catch(Exception ex)
            {
                return new JsonResult(ex.Message);
            }
        }
    }
}
