using System.Collections.Generic;
using System.Linq;
using FundTrack.BLL.Abstract;
using FundTrack.DAL.Abstract;
using FundTrack.DAL.Entities;
using FundTrack.Infrastructure.ViewModel.EditOrganizationViewModels;

namespace FundTrack.BLL.Concrete
{
    public class TargetService : ITargetService
    {
        private readonly IUnitOfWork _unitOfWork;
        public TargetService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public TargetViewModel GetTargetById(int id)
        {
            var target = _unitOfWork.TargetRepository.GetTargetById(id);
            return target != null ? ConvertToTargetViewModel(target) : null;
        }

        public IEnumerable<TargetViewModel> GetTargetsByOrganizationId(int id)
        {
            var targets = _unitOfWork.TargetRepository.GetTargetsByOrganizationId(id);
            return ConvertTargetsToTargetViewModel(targets);
        }

        private IEnumerable<TargetViewModel> ConvertTargetsToTargetViewModel(IEnumerable<Target> targets)
        {
            return targets.Select(ConvertToTargetViewModel);
        }

        private TargetViewModel ConvertToTargetViewModel(Target target)
        {
            return new TargetViewModel()
            {
                Id = target.Id,
                TargetName = target.TargetName,
                OrganizationId = target.OrganizationId
            };
        }

        public TargetViewModel EditTarget(TargetViewModel item)
        {
            var targetToUpdate = new Target()
            {
                Id = item.Id,
                TargetName = item.TargetName,
                OrganizationId = item.OrganizationId
            };
            var result = _unitOfWork.TargetRepository.Update(targetToUpdate);
            _unitOfWork.SaveChanges();

            return ConvertToTargetViewModel(result);
        }

        public TargetViewModel AddTarget(TargetViewModel addTarget)
        {
            var adTarg = new Target()
            {
                TargetName = addTarget.TargetName,
                OrganizationId = addTarget.OrganizationId
            };

            var result = _unitOfWork.TargetRepository.Create(adTarg);
            _unitOfWork.SaveChanges();

            addTarget.Id = result.Id;
            return addTarget;
        }

        public void DeleteTarget(int id)
        {
            var target = _unitOfWork.TargetRepository.GetTargetById(id);
            if (target != null)
            {
                _unitOfWork.TargetRepository.Delete(target.Id);
            }
        }
    }
}
