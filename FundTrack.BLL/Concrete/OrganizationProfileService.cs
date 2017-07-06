using FundTrack.BLL.Abstract;
using FundTrack.DAL.Abstract;
using FundTrack.Infrastructure.ViewModel;
using System.Linq;
using FundTrack.DAL.Entities;

namespace FundTrack.BLL.Concrete
{
    public class OrganizationProfileService : IOrganizationProfileService
    {
        private readonly IUnitOfWork _unitOfWork;
        
        /// <summary>
        /// Creates new instance of OrganizationProfileService
        /// </summary>
        /// <param name="unitOfWork">Unit of work</param>
        public OrganizationProfileService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public OrganizationViewModel GetOrganizationById(int id)
        {
            var organization = _unitOfWork.OrganizationRepository.Get(id);
            var result = new OrganizationViewModel
            {
                Id = organization.Id,
                Name = organization.Name,
                Description = organization.Description,
                IsBanned = false
            };
            return result;
        }
    }
}
