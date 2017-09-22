using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using FundTrack.BLL.Abstract;
using FundTrack.DAL.Abstract;
using FundTrack.DAL.Entities;
using FundTrack.Infrastructure.ViewModel.FinanceViewModels.DonateViewModels;
using FundTrack.Infrastructure;

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
                throw new BusinessLogicException(ErrorMessages.CantFindItem, ex);
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
                throw new BusinessLogicException(ErrorMessages.CantFindItem, ex);
            }
        }

        private IEnumerable<TargetViewModel> ConvertTargetsToTargetViewModel(IEnumerable<Target> targets)
        {
            return targets.Select(ConvertTargetToViewModel).ToList();
        }

        private TargetViewModel ConvertTargetToViewModel(Target target)
        {
            return new TargetViewModel()
            {
                TargetId = target.Id,
                Name = target.TargetName,
                OrganizationId = target.OrganizationId,
                ParentTargetId = target.ParentTargetId
            };
        }

        private Target ConvertViewModelToTarget(TargetViewModel targetVm)
        {
            return new Target()
            {
                Id = targetVm.TargetId,
                TargetName = targetVm.Name,
                OrganizationId = targetVm.OrganizationId,
                ParentTargetId = targetVm.ParentTargetId
            };
        }

        public TargetViewModel EditTarget(TargetViewModel item)
        {
            if (item.Name.Length == 0)
            {
                throw new System.ArgumentException(ErrorMessages.RequiredFieldMessage);
            }

            try
            {
                var targetToUpdate = ConvertViewModelToTarget(item);

                var result = _unitOfWork.TargetRepository.Update(targetToUpdate);
                _unitOfWork.SaveChanges();

                return ConvertTargetToViewModel(result);
            }
            catch (System.Exception ex)
            {
                throw new BusinessLogicException(ErrorMessages.UpdateDataError, ex);
            }
        }

        public TargetViewModel CreateTarget(TargetViewModel addTarget)
        {
            if (addTarget.Name.Length == 0)
            {
                throw new System.ArgumentException(ErrorMessages.RequiredFieldMessage);
            }

            try
            {
                var addTarg = ConvertViewModelToTarget(addTarget);

                var result = _unitOfWork.TargetRepository.Create(addTarg);
                _unitOfWork.SaveChanges();

                return ConvertTargetToViewModel(result);
            }
            catch (System.Exception ex)
            {
                throw new BusinessLogicException(ErrorMessages.CantCreatedItem, ex);
            }
        }

        public void DeleteTarget(int id)
        {
            try
            {
                _unitOfWork.TargetRepository.Delete(id);
                _unitOfWork.SaveChanges();
            }
            catch (System.Exception ex)
            {
                throw new BusinessLogicException(ErrorMessages.DeleteDataError, ex);
            }
        }

        public IEnumerable<TargetViewModel> GetTargets(int orgId, int parentTargetId = 0)
        {
            try
            {
                IEnumerable<Target> targets;
                if (parentTargetId == 0)
                {
                    targets = _unitOfWork.TargetRepository.GetTargetsByOrganizationId(orgId)
                        .Where(t => t.ParentTargetId == null);
                }
                else
                {
                    targets = _unitOfWork.TargetRepository.GetTargetsByOrganizationId(orgId)
                        .Where(t => t.ParentTargetId == parentTargetId);
                }
                return ConvertTargetsToTargetViewModel(targets);
            }
            catch (System.Exception ex)
            {
                throw new BusinessLogicException(ex.Message, ex);
            }
        }
    }
}
