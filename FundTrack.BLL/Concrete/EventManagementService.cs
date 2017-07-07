using FundTrack.BLL.Abstract;
using FundTrack.DAL.Abstract;
using FundTrack.DAL.Entities;
using FundTrack.Infrastructure.ViewModel;
using FundTrack.Infrastructure.ViewModel.EventViewModel;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Linq;

namespace FundTrack.BLL.Concrete
{
    /// <summary>
    /// Service for event management
    /// </summary>
    /// <seealso cref="FundTrack.BLL.Abstract.IEventManagementService" />
    public sealed class EventManagementService : IEventManagementService
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="EventManagementService"/> class.
        /// </summary>
        /// <param name="unitOfWorkParam">The unit of work parameter.</param>
        public EventManagementService(IUnitOfWork unitOfWorkParam)
        {
            this._unitOfWork = unitOfWorkParam;
        }

        /// <summary>
        /// Gets all events by organization identifier.
        /// </summary>
        /// <param name="id">The identifier for organization</param>
        /// <returns>IEnumerable<EventManagementViewModel></returns>
        public IEnumerable<EventManagementViewModel> GetAllEventsByOrganizationId(int id)
        {
            var result = ((DbSet<Event>)_unitOfWork.EventRepository.Read())
            .Include(ev => ev.EventImages)
            .Select(events => new EventManagementViewModel()
            {
                Id = events.Id,
                Description = events.Description,
                CreateDate = events.CreateDate,
                OrganizationId = events.OrganizationId,
                Images = events.EventImages.Where(im => im.EventId == events.Id).Select(images => new ImageViewModel() { Id = images.Id, ImageUrl = images.ImageUrl })
            }).OrderByDescending(e => e.CreateDate);
            return result;
        }
    }
}
