﻿using FundTrack.BLL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FundTrack.DAL.Entities;
using FundTrack.Infrastructure.ViewModel;
using FundTrack.DAL.Abstract;
using FundTrack.Infrastructure.ViewModel.FinanceViewModels;
using FundTrack.Infrastructure;

namespace FundTrack.BLL.Concrete
{
    public class OrganizationAccountService : IOrganizationAccountService
    {
        private readonly IUnitOfWork _unitOfWork;
        public OrganizationAccountService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        public OrgAccountViewModel CreateOrganizationAccount(OrgAccountViewModel model)
        {
            if (model.AccountType == "cash")
            {
                var item = this.CreateCashAccount(model);
                return item;
            }
            else if (model.AccountType == "bank")
            {
                var item = this.CreateBankAccount(model);
                return item;
            }
            else
            {
                throw new BusinessLogicException("Неможливо створити рахунок невизначеного типу");
            }
        }

        public DeleteOrgAccountViewModel DeleteOrganizationAccount(DeleteOrgAccountViewModel model)
        {

            try
            {
                var userRole = this._unitOfWork.MembershipRepository.GetRole(model.UserId);
                User user = this._unitOfWork.UsersRepository.Get(model.UserId);
                if (user.Password == PasswordHashManager.GetPasswordHash(model.AdministratorPassword))
                {
                    if (this._unitOfWork.MembershipRepository.GetOrganizationId(model.UserId) == model.OrganizationId && userRole == "admin")
                    {
                        var orgAccount = this._unitOfWork.OrganizationAccountRepository.Read(model.OrgAccountId);
                        this._unitOfWork.OrganizationAccountRepository.Delete(model.OrgAccountId);
                        if (orgAccount.AccountType == "Банк")
                        {
                            var bankAccount = this._unitOfWork.BankAccountRepository.Get(orgAccount.BankAccount.Id);
                            this._unitOfWork.BankAccountRepository.Delete(bankAccount.Id);
                        }
                        this._unitOfWork.SaveChanges();
                        return new DeleteOrgAccountViewModel();
                    }
                    else
                    {
                        return new DeleteOrgAccountViewModel
                        {
                            Error = "Ви не адміністратор цієї організації"
                        };
                    }
                }
                else
                {
                    return new DeleteOrgAccountViewModel
                    {
                        Error = "Невірний пароль адміністратора організації"
                    };
                }
            }
            catch (Exception e)
            {
                return new DeleteOrgAccountViewModel
                {
                    Error = e.Message
                };
            }
        }

        public IEnumerable<OrgAccountViewModel> GetAccountsByOrganizationId(int organizationId)
        {
            try
            {
                var accounts = this._unitOfWork.OrganizationAccountRepository.ReadAllOrgAccounts(organizationId);
                var accountsModels = new List<OrgAccountViewModel>();
                foreach (var item in accounts)
                {
                    var account = this.InitializeOrgAccountViewModel(item);
                    accountsModels.Add(account);
                }
                return accountsModels;
            }
            catch (Exception e)
            {
                string message = string.Format("Неможливо отримати рахунки організації. Помилка: {0}", e.Message);
                throw new BusinessLogicException(message, e);
            }
        }

        public OrgAccountViewModel GetOrganizationAccountById(int organizationAccountId)
        {
            try
            {
                var account = this._unitOfWork.OrganizationAccountRepository.Read(organizationAccountId);
                OrgAccountViewModel model = this.InitializeOrgAccountViewModel(account);
                return model;
            }
            catch (Exception e)
            {
                string message = string.Format("Неможливо отримати рахунок організації. Помилка: {0}", e.Message);
                throw new BusinessLogicException(message, e);
            }
        }

        public OrgAccountViewModel UpdateOrganizationAccount(OrgAccountViewModel model)
        {
            try
            {
                var result = _unitOfWork.OrganizationAccountRepository.Edit(model);
                _unitOfWork.SaveChanges();
                return result;
            }
            catch (Exception e)
            {
                string message = string.Format("Неможливо оновити рахунок організації. Помилка: {0}", e.Message);
                throw new BusinessLogicException(message, e);
            }
        }

        public OrgAccountViewModel InitializeCommonProperties(OrgAccount item)
        {
            return new OrgAccountViewModel
            {
                Id = item.Id,
                OrgAccountName = item.OrgAccountName,
                OrgId = item.OrgId,
                Currency = item.Currency.FullName,
                CurrencyId = item.CurrencyId,
                CurrencyShortName = item.Currency.ShortName,
                CurrentBalance = item.CurrentBalance,
                TargetId = item.TargetId,
                Description = item.Description
            };
        }
        public OrgAccountViewModel InitializeOrgAccountViewModel(OrgAccount item)
        {
            var account = new OrgAccountViewModel();
            switch (item.AccountType)
            {
                case "Готівка":
                    account = this.InitializeCommonProperties(item);
                    account.AccountType = "Готівка";
                    break;
                case "Банк":
                    account = this.InitializeCommonProperties(item);
                    account.AccountType = "Банк";
                    account.BankName = item.BankAccount.BankName;
                    account.AccNumber = item.BankAccount.AccNumber;
                    account.EDRPOU = item.BankAccount.EDRPOU;
                    account.MFO = item.BankAccount.MFO;
                    account.CardNumber = item.BankAccount.CardNumber;
                    account.BankAccId = item.BankAccId;
                    break;
                default:
                    break;
            }
            return account;
        }
        public OrgAccountViewModel CreateCashAccount(OrgAccountViewModel model)
        {
            var account = new OrgAccount();
            try
            {
                var message = this.ValidateOrgAccount(model);
                if (message != String.Empty)
                {
                    return new OrgAccountViewModel
                    {
                        Error = message
                    };
                }
                account.AccountType = "Готівка";
                Int32.TryParse(model.Currency, out var currencyId);
                account.Currency = this._unitOfWork.CurrencyRepositry.Get(currencyId);
                account.CurrentBalance = model.CurrentBalance;
                account.OrgAccountName = model.OrgAccountName;
                account.Organization = this._unitOfWork.OrganizationRepository.Get(model.OrgId);
                account.TargetId = model.TargetId;
                account.Description = model.Description;
                this._unitOfWork.OrganizationAccountRepository.Create(account);
                this._unitOfWork.SaveChanges();
                return (OrgAccountViewModel)account;

            }
            catch (Exception e)
            {
                string message = string.Format("Неможливо створити рахунок організації. Помилка: {0}", e.Message);
                throw new BusinessLogicException(message, e);
            }
        }
        public OrgAccountViewModel CreateBankAccount(OrgAccountViewModel model)
        {
            var account = new OrgAccount();
            var bankAccount = new BankAccount();
            try
            {
                var message = this.ValidateOrgAccount(model);
                if (message != String.Empty)
                {
                    return new OrgAccountViewModel
                    {
                        Error = message
                    };
                }
                account.AccountType = "Банк";
                Int32.TryParse(model.Currency, out var currencyId);
                account.Currency = this._unitOfWork.CurrencyRepositry.Get(currencyId);
                account.CurrentBalance = model.CurrentBalance;
                account.OrgAccountName = model.OrgAccountName;
                account.Organization = this._unitOfWork.OrganizationRepository.Get(model.OrgId);
                account.TargetId = model.TargetId;
                bankAccount.AccNumber = model.AccNumber;
                bankAccount.BankName = model.BankName;
                bankAccount.EDRPOU = model.EDRPOU;
                bankAccount.MFO = model.MFO;
                bankAccount.Organization = this._unitOfWork.OrganizationRepository.Get(model.OrgId);
                bankAccount.OrgId = model.OrgId;
                bankAccount.CardNumber = model.CardNumber;
                this._unitOfWork.BankAccountRepository.Create(bankAccount);
                account.BankAccId = bankAccount.Id;
                account.BankAccount = bankAccount;
                account.Description = model.Description;
                this._unitOfWork.OrganizationAccountRepository.Create(account);
                this._unitOfWork.SaveChanges();
                return (OrgAccountViewModel)account;

            }
            catch (Exception e)
            {
                string message = string.Format("Неможливо створити рахунок організації. Помилка: {0}", e.Message);
                throw new BusinessLogicException(message, e);
            }
        }
        public string ValidateOrgAccount(OrgAccountViewModel model)
        {
            ///Checks if account with such name already exists within organization
            foreach (var item in this._unitOfWork.OrganizationAccountRepository.ReadAllOrgAccounts(model.OrgId))
            {
                if (item.OrgAccountName == model.OrgAccountName)
                {
                    return "Рахунок організації з таким іменем уже існує";
                }
            }
            if (model.AccountType == "bank")
            {
                ///Checks if account with such bank number already exists within all organizations
                foreach (var item in this._unitOfWork.OrganizationAccountRepository.GetAllOrgAccounts().Where(a => a.AccountType == "Банк"))
                {
                    if (item.BankAccount.AccNumber != null && item.BankAccount.AccNumber == model.AccNumber)
                    {
                        return "Рахунок з таким номером уже зареєстрований";
                    }
                }
            }
            return String.Empty;
        }

        /// <summary>
        /// Gets the account for select.
        /// </summary>
        /// <param name="organizationId">The organization identifier.</param>
        /// <param name="card">The card.</param>
        /// <returns></returns>
        /// <exception cref="BusinessLogicException"></exception>
        public OrgAccountSelectViewModel GetAccountForSelect(int organizationId, string card)
        {
            try
            {
                var orgAccount = this._unitOfWork.OrganizationAccountRepository.GetOrgAccountByCardNumber(organizationId, card);
                return new OrgAccountSelectViewModel
                {
                    Id = orgAccount.Id,
                    OrgAccountName = orgAccount.OrgAccountName,
                    OrgAccountNumber=orgAccount.BankAccount.AccNumber
                };
            }
            catch (Exception ex)
            {
                string message = string.Format("Неможливо отримати рахунок організації. Помилка: {0}", ex.Message);
                throw new BusinessLogicException(message, ex);
            }
        }

        /// <summary>
        /// Checks if donation function is enabled
        /// </summary>
        /// <param name="bankAccountId"></param>
        /// <returns></returns>
        public bool IsDonationConnected(int orgAccountId)
        {
            var orgAccount = _unitOfWork.OrganizationAccountRepository.GetOrgAccountById(orgAccountId);

            try
            {               
                if(orgAccount.BankAccount.MerchantId != null)
                return true;
                else return false;              
            }

            catch (Exception)
            {
                return false;
            }            
        }

        public bool IsDonationEnabled(int orgAccountId)
        {
            var orgAccount = _unitOfWork.OrganizationAccountRepository.GetOrgAccountById(orgAccountId);

            try
            {
                if (orgAccount.BankAccount.IsDonationEnabled!= null && (bool)orgAccount.BankAccount.IsDonationEnabled)
                    return true;
                else return false;
            }

            catch (Exception)
            {
                return false;
            }
        }

        public BankAccountDonateViewModel GetDonateCredentials(int orgAccountId)
        {
            var orgAccount = _unitOfWork.OrganizationAccountRepository.GetOrgAccountById(orgAccountId);           

            try
            {
                var result = new BankAccountDonateViewModel() {
                    BankAccountId = orgAccount.BankAccount.Id,
                    MerchantId = (int)orgAccount.BankAccount.MerchantId,
                    MerchantPassword = orgAccount.BankAccount.MerchantPassword
                };
                return result;
            }

            catch (Exception ex)
            {
                string message = string.Format("Неможливо отримати інформацію для рахунку. Помилка: {0}", ex.Message);
                throw new BusinessLogicException(message, ex);
            }
        }

        public bool ToggleDonateFunction(int orgAccountId)
        {
            var orgAccount = _unitOfWork.OrganizationAccountRepository.GetOrgAccountById(orgAccountId);

            try
            {
                var bankAccount = _unitOfWork.BankAccountRepository.Get((int)orgAccount.BankAccId);

                bankAccount.IsDonationEnabled = !bankAccount.IsDonationEnabled;

                _unitOfWork.BankAccountRepository.Update(bankAccount);

                _unitOfWork.SaveChanges();

                return (bool)bankAccount.IsDonationEnabled;
            }

            catch (Exception ex)
            {
                string message = string.Format("Неможливо змінити інформацію для рахунку. Помилка: {0}", ex.Message);
                throw new BusinessLogicException(message, ex);
            }           
        }

        public BankAccountDonateViewModel ConnectDonateFunction(BankAccountDonateViewModel info)
        {
            var bankAccount = _unitOfWork.BankAccountRepository.Get(info.BankAccountId);

            if(bankAccount != null)
            {
                bankAccount.MerchantId = (int)info.MerchantId;
                bankAccount.MerchantPassword = info.MerchantPassword;
                bankAccount.IsDonationEnabled = true;

                _unitOfWork.BankAccountRepository.Update(bankAccount);

                _unitOfWork.SaveChanges();

                return info;
            }
            else
            {
                return info;
            }
        }

        public int GetBankAccountIdByOrgAccountId(int orgAccountId)
        {
            var orgAccount = _unitOfWork.OrganizationAccountRepository.GetOrgAccountById(orgAccountId);

            if (orgAccount!=null && orgAccount.BankAccId.HasValue)
            {
                return (int)orgAccount.BankAccId;
            }

            else return 0;           
        }

        public BankAccountDonateViewModel DisableDonateFunction(int bankAccountId)
        {
            var bankAccount = _unitOfWork.BankAccountRepository.Get(bankAccountId);

            if (bankAccount != null)
            {
                bankAccount.MerchantId = null;
                bankAccount.MerchantPassword = null;
                bankAccount.IsDonationEnabled = false;

                _unitOfWork.BankAccountRepository.Update(bankAccount);

                _unitOfWork.SaveChanges();

                var result = new BankAccountDonateViewModel
                {
                    BankAccountId = bankAccountId,
                    MerchantId = null,
                    MerchantPassword = null
                };

                return result;
            }
            else
            {
                return null;
            }
        }
    }
}
