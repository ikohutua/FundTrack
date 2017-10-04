using FundTrack.BLL.Abstract;
using FundTrack.DAL.Abstract;
using FundTrack.DAL.Entities;
using FundTrack.Infrastructure;
using FundTrack.Infrastructure.ViewModel.FinanceViewModels.DonateViewModels;
using System;
using System.Collections.Generic;
using System.Linq;

namespace FundTrack.BLL.Concrete
{
    public class DonateMoneyService : IDonateMoneyService
    {
        private readonly IUnitOfWork _unitOfWork;

        public DonateMoneyService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public OrganizationDonateAccountsViewModel GetAccountForDonation(int organizationId)
        {
            var orgAccounts = _unitOfWork.OrganizationAccountRepository.ReadOrgAccountsForDonations(organizationId);
            var result = new OrganizationDonateAccountsViewModel()
            {
                OrganizationId = organizationId,
                OrgName = _unitOfWork.OrganizationRepository.Get(organizationId).Name,
                Accounts = new List<DonateAccountViewModel>()
            };
            if (orgAccounts != null)
            {
                foreach (var orgAccount in orgAccounts)
                {
                    result.Accounts.Add(new DonateAccountViewModel
                    {
                        Description = orgAccount.Description,
                        BankAccountId = orgAccount.BankAccount.Id,
                        MerchantId = (int)orgAccount.BankAccount.MerchantId,
                        MerchantPassword = orgAccount.BankAccount.MerchantPassword,
                        Name = orgAccount.OrgAccountName
                    });
                }
                return result;
            }
            else
            {
                result.Accounts = null;
                return result;
            }
        }

        public string GetOrderId()
        {
            return Guid.NewGuid().ToString();
        }

        public IEnumerable<TargetViewModel> GetTargets(int id)
        {
            var targets = _unitOfWork.TargetRepository.GetTargetsByOrganizationId(id);
            var result = new List<TargetViewModel>();
            foreach (var target in targets)
            {
                result.Add(new TargetViewModel
                {
                    TargetId = target.Id,
                    Name = target.TargetName,
                    OrganizationId = target.OrganizationId
                });
            }
            return result;
        }

        public IEnumerable<CurrencyViewModel> GetCurrencies()
        {
            var currencies = _unitOfWork.CurrencyRepositry.Read();
            var result = new List<CurrencyViewModel>();
            foreach (var currency in currencies)
            {
                result.Add(new CurrencyViewModel
                {
                    CurrencyId = currency.Id,
                    CurrencyShortName = currency.ShortName
                });
            }
            return result;
        }

        public DonateViewModel AddDonation(DonateViewModel item)
        {
            var itemToAdd = new Donation
            {
                OrderId = new Guid(item.OrderId),
                Amount = item.Amount,
                BankAccountId = item.BankAccountId,
                CurrencyId = item.CurrencyId,
                Description = item.Description,
                UserId = item.UserId,
                TargetId = item.TargetId,
                DonatorEmail = item.DonatorEmail
            };
            var created = _unitOfWork.DonationRepository.Create(itemToAdd);
            _unitOfWork.SaveChanges();
            var result = new DonateViewModel
            {
                OrderId = created.OrderId.ToString(),
                Amount = created.Amount,
                BankAccountId = created.BankAccountId,
                CurrencyId = created.CurrencyId,
                Description = created.Description,
                UserId = created.UserId,
                TargetId = created.TargetId,
                DonatorEmail = created.DonatorEmail
            };
            return result;
        }

        public IEnumerable<UserDonationsViewModel> GetUserDonations(int userid)
        {
            try
            {
                var result = _unitOfWork.DonationRepository.Read()
                    .Where(donation => donation.UserId == userid)
                    .Select(donation => new UserDonationsViewModel
                    {
                        Id = donation.Id,
                        Organization = donation.BankAccount?.Organization.Name,
                        Target = donation.Target?.TargetName,
                        Date = donation.DonationDate,
                        Amount = donation.Amount,
                        Description = donation.Description
                    }).OrderBy(donation => donation.Date).ToList();
                return result;
            }
            catch(Exception ex)
            {
                throw new BusinessLogicException(ErrorMessages.CantFindItem, ex);
            }

        }

        public IEnumerable<UserDonationsViewModel> GetUserDonationsByDate(int userid, DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                dateTo = dateTo.AddDays(1);
                var result = GetUserDonations(userid)
                    .Where(donat => donat.Date <= dateTo
                    && donat.Date >= dateFrom)
                    .OrderBy(donation => donation.Date).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ErrorMessages.CantFindItem, ex);
            }
        }
    }
}