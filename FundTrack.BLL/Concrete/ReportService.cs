using FundTrack.BLL.Abstract;
using System;
using System.Collections.Generic;
using FundTrack.Infrastructure.ViewModel;
using FundTrack.DAL.Abstract;
using System.Linq;
using FundTrack.Infrastructure;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Extensions;

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
                        && (finOps.FinOpDate > dateFrom)
                        && (finOps.FinOpDate < dateTo)).ToList()
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
                throw new BusinessLogicException(ex.Message, ex);
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
                        && finOps.FinOpDate > dateFrom
                        && finOps.FinOpDate < dateTo).ToList()
                    .Select(finOps => new ReportOutcomeViewModel
                    {
                        Id = finOps.Id,
                        Description = finOps.Description,
                        Target = finOps.Target?.TargetName,
                        MoneyAmount = finOps.Amount,
                        Date = finOps.FinOpDate,
                    }).OrderByDescending(finop => finop.Date);
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ex.Message, ex);
            }
        }

        public IEnumerable<string> GetImagesById(int finOpId)
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
                throw new BusinessLogicException(ErrorMessages.CantGetImages, ex);
            }
        }

        public IEnumerable<InvoiceDeclarationReportViewModel> GetInvoiceDeclarationReport(int orgId, DateTime? dateFrom, DateTime? dateTo)
        {
            try
            {
                var orgAccounts = _unitOfWork.OrganizationAccountRepository.ReadAllOrgAccounts(orgId).Where(m => m.AccountType == Constants.BankType);
                var result = orgAccounts.Select(m => new InvoiceDeclarationReportViewModel
                {
                    BankAccount = m.BankAccount.CardNumber,
                    BankAccountTooltip = m.OrgAccountName + " : " + m.Description,
                    BeginIncomeMonthSum = m.Balances.Where(z => z.BalanceDate.Date == dateFrom.Value.Date && z.BalanceDate.Date <= dateTo.Value.Date).OrderBy(c => c.BalanceDate).LastOrDefault()?.Amount,
                    TotalIncomeSum = m.FinOpsTo.Where(z => z.FinOpDate.Date >= dateFrom.Value.Date && z.FinOpDate.Date <= dateTo.Value.Date).Sum(s => s.Amount),
                    TransferIncome = m.FinOpsTo.Where(z => z.FinOpDate.Date >= dateFrom.Value.Date && z.FinOpDate.Date <= dateTo.Value.Date).Where(a => a.AccFromId != null && a.AccToId != null).Sum(s => s.Amount),
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

        public async Task<int> GetCountOfUsersDonationsReportAsync(int orgId, DateTime dateFrom, DateTime dateTo)
        {
            var count = await GetRequestedDonation(orgId, dateFrom, dateTo);
            return await count.CountAsync();
        }

        public async Task<int> GetFilteredCountOfUsersDonationsReportAsync(int orgId, DateTime dateFrom, DateTime dateTo, string filterValue)
        {
            var donations = await GetRequestedDonation(orgId, dateFrom, dateTo);
            var count = await donations.Where(d => d.User != null && d.User.Login.Contains(filterValue)).CountAsync();
            return count;
        }

        public async Task<int> GetCountOfCommonUsersDonationsReportAsync(int orgId, DateTime dateFrom, DateTime dateTo)
        {
            var donations = await GetRequestedDonation(orgId, dateFrom, dateTo);
            var count = await donations.GroupBy(d => d.User.Login).CountAsync();
            return count;
        }


        public async Task<IEnumerable<UsersDonationsReportViewModel>> GetUsersDonationsPaginatedReportAsync(int orgId, DateTime dateFrom, DateTime dateTo, int pageIndex, int pageSize)
        {
            var donations = await GetUserDonations(orgId, dateFrom, dateTo);
            var result = PaginateReport(pageIndex, pageSize, donations);
            return result;
        }

        public async Task<IEnumerable<UsersDonationsReportViewModel>> GetUsersDonationsPaginatedReportByUserLoginAsync(int orgId, DateTime dateFrom, DateTime dateTo, int pageIndex, int pageSize, string filterValue)
        {
            var t = await GetUserDonations(orgId, dateFrom, dateTo);
            var filtered = t.ToList().Where(d => d.UserLogin.Contains(filterValue));
            var result = PaginateReport(pageIndex, pageSize, filtered.AsQueryable());
            return result;
        }

        public async Task<IEnumerable<UsersDonationsReportViewModel>> GetCommonUsersDonationsPaginatedReportAsync(int orgId, DateTime dateFrom, DateTime dateTo, int pageIndex, int pageSize)
        {
            var donations = await GetUserDonations(orgId, dateFrom, dateTo);
            var grouped = GroupDonationsByUserLogin(donations.ToList());
            var result = PaginateReport(pageIndex, pageSize, grouped);
            return result;
        }

        private static IQueryable<UsersDonationsReportViewModel> GroupDonationsByUserLogin(IEnumerable<UsersDonationsReportViewModel> list)
        {
            return list.GroupBy(donat => donat.UserLogin)
                .Select(group => new UsersDonationsReportViewModel()
                {
                    UserLogin = group.Select(u => u.UserLogin).FirstOrDefault(),
                    UserFulName = group.Select(u => u.UserFulName).FirstOrDefault(),
                    MoneyAmount = group.Sum(u => u.MoneyAmount),
                    Target = string.Join(", ", group.Select(u => u.Target).Where(t => !String.IsNullOrEmpty(t)).Distinct().ToArray())
                }).AsQueryable();
        }

        private static IEnumerable<UsersDonationsReportViewModel> PaginateReport(int pageIndex, int pageSize, IQueryable<UsersDonationsReportViewModel> t)
        {
            var list = t.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            return list.OrderByDescending(i => i.Date);
        }

        private async Task<IQueryable<UsersDonationsReportViewModel>> GetUserDonations(int orgId, DateTime dateFrom, DateTime dateTo)
        {
            var donations = await GetRequestedDonation(orgId, dateFrom, dateTo);

            return donations.Select(d => new UsersDonationsReportViewModel
            {
                Id = d.Id,
                Target = d.Target == null ? null : d.Target.TargetName,
                UserFulName = d.User == null ? null : $"{d.User.FirstName} {d.User.LastName}",
                UserLogin = String.IsNullOrEmpty(d.User.Login) ? Constants.Anonymous : d.User.Login,
                Date = d.DonationDate,
                MoneyAmount = (decimal)d.Amount,
                Description = d.Description
            });
        }

        private Task<IQueryable<DAL.Entities.Donation>> GetRequestedDonation(int orgId, DateTime dateFrom, DateTime dateTo)
        {
            return Task.Run(() =>
            {
                var orgAccountsIds = _unitOfWork.OrganizationAccountRepository.ReadAllOrgAccounts(orgId).Select(oa => oa.Id);

                if (orgAccountsIds.Count() == 0)
                {
                    throw new BusinessLogicException(ErrorMessages.OrganizationNotFound);
                }

                dateFrom = dateFrom.Date;
                dateTo = dateTo.Date;
                var donations = _unitOfWork.DonationRepository.Read()
                    .Where(d => d.DonationDate.Date >= dateFrom &&
                                d.DonationDate.Date <= dateTo &&
                                orgAccountsIds.Contains(d.OrgAccountId));
                return donations;
            });

        }

        public async Task<IEnumerable<DataSetViewModel>> GetDonationsReportPerDayAsync(int orgId, DateTime dateFrom, DateTime dateTo)
        {
            var donations = await GetRequestedDonation(orgId, dateFrom, dateTo);
            var groupedDonations = GetGroupedDonationsAmountByDate(donations.ToList());
            var groupedDonationsWithMissingDates = GetDonationWithMissingDate(groupedDonations, dateFrom, dateTo);

            return ConvertToDataSet(groupedDonationsWithMissingDates);
        }

        public async Task<IEnumerable<DataSetViewModel>> GetDonationsReportPerDayByTargetAsync(int orgId, DateTime dateFrom, DateTime dateTo, int targetId)
        {
            var donations = await GetRequestedDonation(orgId, dateFrom, dateTo);
            var filtered = donations.Where(d => d.TargetId == targetId);
            var groupedDonations = GetGroupedDonationsAmountByDate(filtered.ToList());
            var groupedDonationsWithMissingDates = GetDonationWithMissingDate(groupedDonations, dateFrom, dateTo);

            return ConvertToDataSet(groupedDonationsWithMissingDates);
        }

        private static IEnumerable<DAL.Entities.Donation> GetGroupedDonationsAmountByDate(IEnumerable<DAL.Entities.Donation> list)
        {
            return list.GroupBy(donat => donat.DonationDate.Date)
                .Select(group => new DAL.Entities.Donation
                {
                    Amount = group.Sum(u => u.Amount),
                    DonationDate = group.Select(d => d.DonationDate.Date).FirstOrDefault()
                });
        }

        private static IEnumerable<DataSetViewModel> ConvertToDataSet(IEnumerable<DAL.Entities.Donation> list)
        {
            return list.Select(item => new DataSetViewModel
            {
                Name = item.DonationDate.Date.ToString("dd/MM/yyyy"),
                Value = item.Amount
            });
        }

        private static IEnumerable<DAL.Entities.Donation> GetDonationWithMissingDate(IEnumerable<DAL.Entities.Donation> list, DateTime dateFrom, DateTime dateTo)
        {
            var listWithMissingDate = list.ToList();

            for (int i = 0; i <= (dateTo - dateFrom).Days; i++)
            {
                DateTime dateTime = dateFrom.AddDays(i);
                bool v = listWithMissingDate.Any(d => d.DonationDate.Date == dateTime.Date);
                if (!v)
                {
                    listWithMissingDate.Add(new DAL.Entities.Donation()
                    {
                        DonationDate = dateTime
                    });
                }
            }

            return listWithMissingDate.OrderBy(d=>d.DonationDate);
        }
    }
}