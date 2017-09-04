using System.Collections.Generic;
using System.Linq;
using FundTrack.BLL.Abstract;
using FundTrack.DAL.Abstract;
using FundTrack.DAL.Entities;
using FundTrack.Infrastructure.ViewModel.FinanceViewModels.DonateViewModels;

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
            try
            {
                var target = _unitOfWork.TargetRepository.GetTargetById(id);
                return ConvertTargetToViewModel(target);
            }
            catch (System.Exception ex)
            {
                throw new BusinessLogicException(ex.Message, ex);
            }
        }

        public IEnumerable<TargetViewModel> GetTargetsByOrganizationId(int id)
        {
            try
            {
                var targets = _unitOfWork.TargetRepository.GetTargetsByOrganizationId(id);
                return ConvertTargetsToTargetViewModel(targets);
            }
            catch (System.Exception ex)
            {
                throw new BusinessLogicException(ex.Message, ex);
            }
        }

        private IEnumerable<TargetViewModel> ConvertTargetsToTargetViewModel(IEnumerable<Target> targets)
        {
            return targets.Select(ConvertTargetToViewModel);
        }

        private TargetViewModel ConvertTargetToViewModel(Target target)
        {
            return new TargetViewModel()
            {
                TargetId = target.Id,
                Name = target.TargetName,
                OrganizationId = target.OrganizationId
            };
        }

        public TargetViewModel EditTarget(TargetViewModel item)
        {
            try
            {
                var targetToUpdate = new Target()
                {
                    Id = item.TargetId,
                    TargetName = item.Name,
                    OrganizationId = item.OrganizationId
                };
                var result = _unitOfWork.TargetRepository.Update(targetToUpdate);
                _unitOfWork.SaveChanges();

                return ConvertTargetToViewModel(result);
            }
            catch (System.Exception ex)
            {
                throw new BusinessLogicException(ex.Message, ex);
            }
        }

        public TargetViewModel CreateTarget(TargetViewModel addTarget)
        {
            try
            {
                var addTarg = new Target()
                {
                    TargetName = addTarget.Name,
                    OrganizationId = addTarget.OrganizationId
                };

                var result = _unitOfWork.TargetRepository.Create(addTarg);
                _unitOfWork.SaveChanges();

                return ConvertTargetToViewModel(result);
            }
            catch (System.Exception ex)
            {
                throw new BusinessLogicException(ex.Message, ex);
            }

        }

        public void DeleteTarget(int id)
        {
            try
            {
                _unitOfWork.TargetRepository.Delete(id);
            }
            catch (System.Exception ex)
            {
                throw new BusinessLogicException(ex.Message, ex);
            }
        }
    }
}
