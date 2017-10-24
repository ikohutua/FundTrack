using FundTrack.BLL.Abstract;
using FundTrack.DAL.Abstract;
using FundTrack.DAL.Entities;
using FundTrack.Infrastructure;
using FundTrack.Infrastructure.ViewModel.FinanceViewModels.DonateViewModels;
using Microsoft.EntityFrameworkCore.Internal;
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
            var orgAccounts = _unitOfWork.OrganizationAccountRepository.ReadOrgAccountsForDonations(organizationId)
                              .Distinct((c1, c2) => c1.Target == c2.Target);

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
                        MerchantId = orgAccount.BankAccount.MerchantId,
                        MerchantPassword = orgAccount.BankAccount.MerchantPassword,
                        Name = orgAccount.OrgAccountName,
                        TargetId = orgAccount.TargetId,
                        Target = orgAccount.TargetId == null ? "Загальний" : _unitOfWork.TargetRepository
                                .GetTargetById(orgAccount.TargetId.GetValueOrDefault()).TargetName

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
                DonatorEmail = item.DonatorEmail,
                DonationDate = Convert.ToDateTime(item.DonationDate)
            };
            var created = _unitOfWork.DonationRepository.Create(itemToAdd);
            _unitOfWork.SaveChanges();
            var result = new DonateViewModel
            {
                Id = created.Id,
                OrderId = created.OrderId.ToString(),
                Amount = created.Amount,
                BankAccountId = created.BankAccountId,
                CurrencyId = created.CurrencyId,
                Description = created.Description,
                UserId = created.UserId,
                TargetId = created.TargetId,
                DonatorEmail = created.DonatorEmail,
                DonationDate = Convert.ToDateTime(item.DonationDate)
            };
            return result;
        }

        public IEnumerable<DonateViewModel> GetAllDonatons()
        {
            var donations = _unitOfWork.DonationRepository.Read();
            return donations.Select(ConvertEntityToModel);
        }

        public DonateViewModel GetDonationById(int id)
        {
            return ConvertEntityToModel(_unitOfWork.DonationRepository.Get(id));
        }

        private DonateViewModel ConvertEntityToModel(Donation donation)
        {
            return new DonateViewModel
            {
                Id = donation.Id,
                OrderId = donation.OrderId.ToString(),
                UserId = donation.UserId,
                CurrencyId = donation.CurrencyId,
                TargetId = donation.TargetId,
                BankAccountId = donation.BankAccountId,
                Amount = donation.Amount,
                Description = donation.Description,
                DonatorEmail = donation.DonatorEmail,
                DonationDate = donation.DonationDate
            };
        }

        public IEnumerable<DonateViewModel> GetSuggestedDonations(int finOpId)
        {
            var finOp = _unitOfWork.FinOpRepository.GetById(finOpId);
            var finOpMaxPossibleDate = finOp.FinOpDate.AddMinutes(30);
            var suggestedDonations =
                _unitOfWork.DonationRepository
                    .Read()
                    .Where(d =>                                                 // suggested conditions are: 
                        (d.DonationDate >= finOp.FinOpDate) &&                  // min bound for date/time
                        (d.DonationDate <= finOpMaxPossibleDate) &&             // max bound for date/time
                        (d.Amount == (double)finOp.Amount))                    // same amount
                        .ToList();
            return suggestedDonations.Select(ConvertEntityToModel);
        }


        public IEnumerable<UserDonationsViewModel> GetUserDonations(int userId)
        {
            try
            {
                var result = _unitOfWork.DonationRepository.Read()
                    .Where(donation => donation.UserId == userId).ToList()
                    .Select(donation => new UserDonationsViewModel
                    {
                        Id = donation.Id,
                        Organization = donation.BankAccount.Organization.Name,
                        Target = donation.Target?.TargetName,
                        Date = donation.DonationDate,
                        Amount = donation.Amount,
                        Description = donation.Description
                    }).OrderByDescending(donation => donation.Date);
                return result;
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ErrorMessages.CantFindItem, ex);
            }
        }

        public IEnumerable<UserDonationsViewModel> GetUserDonationsByDate(int userId, DateTime dateFrom, DateTime dateTo)
        {
            try
            {
                dateTo = dateTo.AddDays(1);
                var result = GetUserDonations(userId)
                    .Where(donat =>
                    (donat.Date <= dateTo)
                    && (donat.Date >= dateFrom))
                    .OrderByDescending(donation => donation.Date).ToList();
                return result;
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ErrorMessages.CantFindItem, ex);
            }
        }
    }
}