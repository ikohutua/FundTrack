using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FundTrack.BLL.Abstract;
using FundTrack.DAL.Abstract;
using FundTrack.DAL.Entities;
using FundTrack.Infrastructure;
using FundTrack.Infrastructure.ViewModel.FinanceViewModels;
using FundTrack.Infrastructure.ViewModel.FinanceViewModels.DonateViewModels;

namespace FundTrack.BLL.Concrete
{
    public class OrganizationStatisticsService : IOrganizationStatisticsService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly ITargetService _targetService;

        public OrganizationStatisticsService(IUnitOfWork unitOfWork, ITargetService targetService)
        {
            _unitOfWork = unitOfWork;
            _targetService = targetService;
        }

        public IEnumerable<TargetReportViewModel> GetReportForIncomeFinopsByTargets(int orgId, int finOpType, DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                var finOps = _unitOfWork.FinOpRepository.Read()
                    .Where(f => IfDateInRange(dateFrom, dateTo, f.FinOpDate) &&
                                f.FinOpType == finOpType
                                && (f.OrgAccountTo.OrgId == orgId || f.OrgAccountFrom.OrgId == orgId)).ToList();

                finOps.ForEach(f =>
                {
                    if (f.Target?.ParentTargetId != null)
                    {
                        f.TargetId = f.Target?.ParentTargetId;
                        f.Target.TargetName = f.Target.ParentTarget.TargetName;
                    }
                });

                var result = finOps.GroupBy(f => f.TargetId).Select(t => new TargetReportViewModel
                {
                    Id = t.First().TargetId ?? -1,
                    TargetName = t.First().Target?.TargetName ?? "Призначення не вказано",
                    Sum = t.Sum(s => s.Amount)
                }).OrderBy(t => t.Id);

                return result;
            }
            catch (Exception e)
            {
                throw new BusinessLogicException(ErrorMessages.CantGetFinOpsForReport, e);
            }
            
        }

        public IEnumerable<TargetReportViewModel> GetSubTargets(int orgId, int finOpType, int baseTargetId, DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                var targetIds = _targetService.GetTargets(orgId, baseTargetId).Select(t => t.TargetId).ToList();
                var finOps = _unitOfWork.FinOpRepository.Read()
                    .Where(f => IfDateInRange(dateFrom, dateTo, f.FinOpDate) &&
                                f.FinOpType == finOpType
                                && (f.OrgAccountTo.OrgId == orgId || f.OrgAccountFrom.OrgId == orgId)).ToList();

                finOps = finOps.Where(f => f.TargetId == baseTargetId || targetIds.Contains(f.TargetId ?? -1)).ToList();

                var result = finOps.GroupBy(f => f.TargetId).Select(t => new TargetReportViewModel
                {
                    Id = t.First().TargetId ?? -1,
                    TargetName = t.First().TargetId == baseTargetId ? "Базове призначення" : t.First().Target.TargetName,
                    Sum = t.Sum(s => s.Amount)
                });

                return result;
            }
            catch (Exception e)
            { 
                throw new BusinessLogicException(ErrorMessages.CantGetFinOpsForReport, e);
            }
        }

        public IEnumerable<FinOpViewModel> GetFinOpsByTargetId(int finOpType, int? targetId, DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                if (targetId == -1)
                {
                    targetId = null; // to get all finOps without target
                }
                var finOps = _unitOfWork.FinOpRepository.Read()
                    .Where(f => f.FinOpType == finOpType && f.TargetId == targetId && IfDateInRange(dateFrom, dateTo, f.FinOpDate)).ToList();
                return finOps.Select(ConvertFinOpToViewModel);
            }
            catch (Exception e)
            {
                throw new BusinessLogicException(ErrorMessages.CantGetFinOpsForReport, e);
            }
        }

        private bool IfDateInRange(DateTime dateFrom, DateTime dateTo, DateTime current)
        {
            return current.Date >= dateFrom.Date && current.Date <= dateTo.Date;
        }

        public static FinOpViewModel ConvertFinOpToViewModel(FinOp f)
        {
            return new FinOpViewModel
            {
                Id = f.Id,
                Description = f.Description,
                Amount = f.Amount,
                CardToId = f.AccToId ?? 0,
                CardFromId = f.AccFromId ?? 0,
                OrgId = f.OrgAccountTo?.OrgId??f.OrgAccountFrom.OrgId,
                Target = f.Target?.TargetName,
                TargetId = f.TargetId,
                UserId = f.UserId ?? 0,
                FinOpType = f.FinOpType,
                Date = f.FinOpDate,
                DonationId = f.DonationId
            };
        }
    }
}
