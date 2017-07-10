using FundTrack.BLL.Abstract;
using FundTrack.DAL.Abstract;
using FundTrack.Infrastructure.ViewModel;
using System.Linq;
using System.Collections.Generic;
using System;
using FundTrack.Infrastructure;

namespace FundTrack.BLL.Concrete
{
    /// <summary>
    /// Class for work with OrganizationForFilteringViewModel
    /// </summary>
    /// <seealso cref="FundTrack.BLL.Abstract.IOrganizationsForLayout" />
    public sealed class OrganizationsForFilteringService : IOrganizationsForFilteringService
    {
        private readonly IUnitOfWork _unitOfWork;

        /// <summary>
        /// Initializes a new instance of the <see cref="OrganizationForFilteringViewModel"/> class.
        /// </summary>
        /// <param name="unitOfWorkParam">The unit of work parameter.</param>
        public OrganizationsForFilteringService(IUnitOfWork unitOfWorkParam)
        {
            this._unitOfWork = unitOfWorkParam;
        }

        /// <summary>
        /// Gets all organizations for search
        /// </summary>
        /// <returns> Collection of OrganizationForFilteringViewModel </returns>
        public IEnumerable<OrganizationForFilteringViewModel> GetAll()
        {
            try
            {
                //return this._unitOfWork
                //    .OrganizationsForFilteringRepository
                //    .GetAll
                //    .Select(o => new OrganizationForFilteringViewModel()
                //    {
                //        Id = o.Id,
                //        Name = o.Name
                //    });
                var temp =  this._unitOfWork
                    .OrganizationsForFilteringRepository
                    .GetAll
                    .Where(o => o.BannedOrganization == null)
                    .Select(o => new OrganizationForFilteringViewModel()
                    {
                        Id = o.Id,
                        Name = o.Name
                    });
                return temp;
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ErrorMessages.NoEntriesInDatabase, ex);
            }
        }
    }
}
