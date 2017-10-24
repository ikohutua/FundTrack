using FundTrack.BLL.Abstract;
using System;
using System.Collections.Generic;
using FundTrack.Infrastructure.ViewModel;
using FundTrack.DAL.Abstract;
using System.Linq;
using FundTrack.Infrastructure;
using System.Threading.Tasks;

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
                if (dateTo == null)
                {
                    dateTo = DateTime.Now;
                }

                if (dateFrom == null)
                {
                    dateFrom = DateTime.MinValue;
                }

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
                if (dateTo == null)
                {
                    dateTo = DateTime.Now;
                }

                if (dateFrom == null)
                {
                    dateFrom = DateTime.MinValue;
                }

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

        public async Task<int> GetCountOfUsersDonationsReport(int orgId, DateTime dateFrom, DateTime dateTo)
        {
            var t = await GetUserDonations(orgId, dateFrom, dateTo);
            return t.ToList().Count;
        }

        public async Task<IEnumerable<UsersDonationsReportViewModel>> GetUsersDonationsPaginatedReportn(int orgId, DateTime dateFrom, DateTime dateTo, int pageIndex, int pageSize)
        {
            var t = await GetUserDonations(orgId, dateFrom, dateTo);
            var list = t.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            list.Sort();
            return list;
        }

        private Task<IQueryable<UsersDonationsReportViewModel>> GetUserDonations(int orgId, DateTime dateFrom, DateTime dateTo)
        {
            return Task.Run(() =>
            {
                var orgAccountsIds = _unitOfWork.OrganizationAccountRepository.ReadAllOrgAccounts(orgId).Select(oa => oa.Id);

                if (orgAccountsIds == null || orgAccountsIds.Count() == 0)
                {
                    throw new BusinessLogicException(ErrorMessages.OrganizationNotFound);
                }

                dateFrom = dateFrom.AddDays(-1);
                dateTo = dateTo.AddDays(1);
                var donations = _unitOfWork.DonationRepository.Read()
                    .Where(d => d.DonationDate >= dateFrom &&
                                d.DonationDate <= dateTo &&
                                orgAccountsIds.Contains(d.OrgAccountId));

                var query = donations.Select(d => new UsersDonationsReportViewModel()
                {
                    Id = d.Id,
                    Target = d.Target.TargetName,
                    UserFirstName = d.User.FirstName,
                    UserLastName = d.User.LastName,
                    UserLogin = d.User.Login,
                    Date = d.DonationDate,
                    MoneyAmount = (decimal)d.Amount,
                    Description = d.Description
                }).Where(d => !String.IsNullOrEmpty(d.UserLogin));
                return query;
            });
        }
    }
}