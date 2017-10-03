using System;
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

        public IEnumerable<TargetViewModel> GetTargetsByOrganizationIdWithEditableField(int id)
        {
            try
            {
                var targets = GetTargetsByOrganizationId(id).ToList();
                CheckIsDeletableField(targets);
                return targets;
            }
            catch (System.Exception ex)
            {
                throw new BusinessLogicException(ErrorMessages.CantFindItem, ex);
            }
        }

        private IEnumerable<TargetViewModel> ConvertTargetsToTargetViewModel(IEnumerable<Target> targets)
        {
            var targetsVm = targets.Select(ConvertTargetToViewModel).ToList();
            return targetsVm;
        }

        private void CheckIsDeletableField(List<TargetViewModel> targets)
        {
            CheckInTargets(targets);
            CheckInOrgAccounts(targets);
            CheckInDonations(targets);
            CheckInFinOps(targets);
        }

        private void CheckInTargets(List<TargetViewModel> targets)
        {
            foreach (var target in targets)
            {
                foreach (var subTarget in targets)
                {
                    if (subTarget.ParentTargetId == target.TargetId)
                    {
                        target.IsDeletable = false;
                        break;
                    }
                    target.IsDeletable = true;
                }
            }
        }

        private void CheckInOrgAccounts(List<TargetViewModel> targets)
        {
            try
            {
                var accounts = _unitOfWork.OrganizationAccountRepository.ReadAllOrgAccounts(targets[0].OrganizationId)
                    .ToList();
                foreach (var target in targets.FindAll(t => t.ParentTargetId == null))
                {
                    if (accounts.Exists(acc => acc.TargetId == target.TargetId))
                    {
                        target.IsDeletable = false;
                    }
                }
            }
            catch (Exception e)
            {
                throw new BusinessLogicException(ErrorMessages.CantFindDataById, e);
            }
        }

        private void CheckInDonations(List<TargetViewModel> targets)
        {
            try
            {
                var donations = _unitOfWork.DonationRepository.Read().ToList();
                foreach (var target in targets)
                {
                    if (donations.Exists(d => d.TargetId == target.TargetId))
                    {
                        target.IsDeletable = false;
                    }
                }
            }
            catch (Exception e)
            {
                throw new BusinessLogicException(ErrorMessages.CantFindDataById, e); 
            }
        }

        private void CheckInFinOps(List<TargetViewModel> targets)
        {
            try
            {
                var finOps = _unitOfWork.FinOpRepository.Read().ToList();
                foreach (var target in targets)
                {
                    if (finOps.Exists(f => f.TargetId == target.TargetId))
                    {
                        target.IsDeletable = false;
                    }
                }
            }
            catch (Exception e)
            {
                throw new BusinessLogicException(ErrorMessages.CantFindDataById, e); 
            }
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
