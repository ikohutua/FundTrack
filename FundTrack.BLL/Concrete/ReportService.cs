using FundTrack.BLL.Abstract;
using System;
using System.Collections.Generic;
using FundTrack.Infrastructure.ViewModel;
using FundTrack.DAL.Abstract;
using System.Linq;
using FundTrack.Infrastructure;

namespace FundTrack.BLL.Concrete
{
    public class ReportService : IReportService
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReportService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public IEnumerable<ReportIncomeViewModel> GetIncomeReports(int orgId, DateTime? dateFrom, DateTime? dateTo)
        {
            try
            {
                return _unitOfWork.FinOpRepository.Read()
                    .Where(finOps =>
                        (finOps.FinOpType == Constants.FinOpTypeIncome)
                        && (finOps.OrgAccountTo.OrgId == orgId)
                        && (finOps.FinOpDate.Date >= dateFrom)
                        && (finOps.FinOpDate.Date <= dateTo)).ToList()
                    .Select(finOps => new ReportIncomeViewModel
                    {
                        From = finOps.Donation?.User?.LastName,
                        Description = finOps.Description,
                        TargetTo = finOps.Target?.TargetName,
                        MoneyAmount = finOps.Amount,
                        Date = finOps.FinOpDate,
                    }).OrderByDescending(finop => finop.Date);
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException("Error while getting income report from finops.", ex);
            }
        }

        public IEnumerable<ReportOutcomeViewModel> GetOutcomeReports(int orgId, DateTime? dateFrom, DateTime? dateTo)
        {
            try
            {
                return _unitOfWork.FinOpRepository.Read()
                    .Where(finOps =>
                        (finOps.FinOpType == Constants.FinOpTypeSpending)
                        && finOps.OrgAccountFrom.OrgId == orgId
                        && finOps.FinOpDate.Date >= dateFrom.Value.Date
                        && finOps.FinOpDate.Date <= dateTo.Value.Date).ToList()
                    .Select(finOps => new ReportOutcomeViewModel
                    {
                        Id = finOps.Id,
                        Description = finOps.Description,
                        Target = finOps.Target?.TargetName,
                        MoneyAmount = finOps.Amount,
                        Date = finOps.FinOpDate,
                    }).OrderByDescending(finop=>finop.Date);
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException("Error while getting outcome report from finops.", ex);
            }
        }

        public IEnumerable<String> GetImagesById(int finOpId)
        {
            try
            {
                return _unitOfWork.FinOpImages.Read().ToList()
                    .Where(finOpImg =>
                        finOpImg.FinOpId == finOpId)
                    .Select(finOpImages => finOpImages.ImageUrl).ToList();
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException("Error while getting image path list from FinOPImages entities by finOpId.", ex);
            }
        }

       public IEnumerable<InvoiceDeclarationReportViewModel> GetInvoiceDeclarationReport(int orgId, DateTime? dateFrom, DateTime? dateTo)
        {
            try
            {
                var orgAccounts = _unitOfWork.OrganizationAccountRepository.ReadAllOrgAccounts(orgId).Where(m => m.AccountType==Constants.BankType);
                var result = orgAccounts.Select(m => new InvoiceDeclarationReportViewModel
                {
                    BankAccount = m.BankAccount.CardNumber,
                    BankAccountTooltip = m.OrgAccountName + " : "+m.Description,
                    BeginIncomeMonthSum = m.Balances.Where(z =>  z.BalanceDate.Date == dateFrom.Value.Date && z.BalanceDate.Date <= dateTo.Value.Date).OrderBy(c=>c.BalanceDate).LastOrDefault()?.Amount,
                    TotalIncomeSum = m.FinOpsTo.Where(z => z.FinOpDate.Date >= dateFrom.Value.Date && z.FinOpDate.Date <= dateTo.Value.Date).Sum(s=>s.Amount),
                    TransferIncome = m.FinOpsTo.Where(z => z.FinOpDate.Date >= dateFrom.Value.Date && z.FinOpDate.Date <= dateTo.Value.Date).Where(a=>a.AccFromId!=null && a.AccToId!=null).Sum(s => s.Amount),
                    FlowOutcome = m.FinOpsFrom.Where(z => z.FinOpDate.Date >= dateFrom.Value.Date && z.FinOpDate.Date <= dateTo.Value.Date).Sum(s => s.Amount),
                    TransferOutcome = m.FinOpsFrom.Where(z => z.AccFromId != null && z.AccToId != null && z.FinOpDate.Date >= dateFrom.Value.Date && z.FinOpDate.Date <= dateTo.Value.Date).Sum(s => s.Amount),
                }).ToList();
                result.ForEach(sum => sum.TotalSum = sum.TotalIncomeSum + sum.TransferIncome - sum.FlowOutcome - sum.TransferOutcome + sum.BeginIncomeMonthSum ?? 0);
                return result;
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException("Error while genereting invoice declaration report by orgId.", ex);
            }
        }
    }
}