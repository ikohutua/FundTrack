using FundTrack.BLL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
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
                throw new BusinessLogicException(ErrorMessages.CantCreateAccountWithundefinedType);
            }
        }

        public DeleteOrgAccountViewModel DeleteOrganizationAccount(DeleteOrgAccountViewModel model)
        {

            try
            {
                var userRole = this._unitOfWork.MembershipRepository.GetRole(model.UserId);
                User user = this._unitOfWork.UsersRepository.Get(model.UserId);
               
                if (user.Password == PasswordHashManager.GetPasswordHash(user.Salt, model.AdministratorPassword))
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
                            Error = ErrorMessages.YouArentAdminOfThisOrganization
                        };
                    }
                }
                else
                {
                    return new DeleteOrgAccountViewModel
                    {
                        Error = ErrorMessages.WrongAdminPasswond
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
                throw new BusinessLogicException(ErrorMessages.GetOrganizationAccount, e);
            }
        }

        public OrgAccountViewModel GetOrganizationAccountById(int accountId)
        {
            try
            {
                var account = this._unitOfWork.OrganizationAccountRepository.Read(accountId);
                if (account == null)
                {
                    return new OrgAccountViewModel();
                }
                OrgAccountViewModel model = this.InitializeOrgAccountViewModel(account);
                return model;
            }
            catch (Exception e)
            {
                throw new BusinessLogicException(ErrorMessages.GetOrganizationAccount, e);
            }
        }

        public OrgAccountViewModel UpdateOrganizationAccount(OrgAccountViewModel model)
        {
            try
            {
                var result = _unitOfWork.OrganizationAccountRepository.GetOrgAccountById(model.Id);
                result.UserId = model.UserId;
                result.TargetId = model.TargetId;
                _unitOfWork.OrganizationAccountRepository.Edit(result);
                _unitOfWork.SaveChanges();
                return result;
            }
            catch (Exception e)
            {
                throw new BusinessLogicException(ErrorMessages.UpdateOrganizationAccount, e);
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
                Description = item.Description,
                UserId = item.UserId,
                FirstName = item.User?.FirstName,
                LastName = item.User?.LastName,
                CreationDate = item.CreationDate
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
                    account.AccNumber = item.BankAccount.AccNumber;
                    account.EDRPOU = item.BankAccount.EDRPOU;
                    account.CardNumber = item.BankAccount.CardNumber;
                    account.BankAccId = item.BankAccId;

                    account.BankId = item.BankAccount.BankId;
                    account.BankName = item.BankAccount.Bank.BankName;
                    account.MFO = item.BankAccount.Bank.MFO;
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
                account.UserId = model.UserId;
                account.CreationDate = model.CreationDate;
                this._unitOfWork.OrganizationAccountRepository.Create(account);
                this._unitOfWork.SaveChanges();
                return (OrgAccountViewModel)account;

            }
            catch (Exception e)
            {
                throw new BusinessLogicException(ErrorMessages.CantCreateAccountOfOrganization, e);
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
                bankAccount.EDRPOU = model.EDRPOU;
                bankAccount.Organization = this._unitOfWork.OrganizationRepository.Get(model.OrgId);
                bankAccount.OrgId = model.OrgId;
                bankAccount.CardNumber = model.CardNumber;
                bankAccount.BankId = model.BankId;
                this._unitOfWork.BankAccountRepository.Create(bankAccount);
                account.BankAccId = bankAccount.Id;
                account.BankAccount = bankAccount;
                account.UserId = model.UserId;
                account.CreationDate = model.CreationDate;
                account.Description = model.Description;
                this._unitOfWork.OrganizationAccountRepository.Create(account);
                this._unitOfWork.SaveChanges();
                return (OrgAccountViewModel)account;

            }
            catch (Exception e)
            {
                throw new BusinessLogicException(ErrorMessages.CantCreateAccountOfOrganization, e);
            }
        }
        public string ValidateOrgAccount(OrgAccountViewModel model)
        {
            ///Checks if account with such name already exists within organization
            foreach (var item in this._unitOfWork.OrganizationAccountRepository.ReadAllOrgAccounts(model.OrgId))
            {
                if (item.OrgAccountName == model.OrgAccountName)
                {
                    return ErrorMessages.OrganizarionAccountWithNameExists;
                }
            }
            if (model.AccountType == "bank")
            {
                ///Checks if account with such bank number already exists within all organizations
                foreach (var item in this._unitOfWork.OrganizationAccountRepository.GetAllOrgAccounts().Where(a => a.AccountType == "Банк"))
                {
                    if (item.BankAccount.AccNumber != null && item.BankAccount.AccNumber == model.AccNumber)
                    {
                        return ErrorMessages.OrganizarionAccountWithNumberExists;
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
                    OrgAccountNumber = orgAccount.BankAccount.AccNumber,
                    TargetId = orgAccount.TargetId
                };
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ErrorMessages.GetOrganizationAccount, ex);
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
                if (orgAccount.BankAccount.MerchantId != null)
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
                if (orgAccount.BankAccount.IsDonationEnabled != null && (bool)orgAccount.BankAccount.IsDonationEnabled)
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
                var result = new BankAccountDonateViewModel()
                {
                    BankAccountId = orgAccount.BankAccount.Id,
                    MerchantId = (int)orgAccount.BankAccount.MerchantId,
                    MerchantPassword = orgAccount.BankAccount.MerchantPassword
                };
                return result;
            }

            catch (Exception ex)
            {
                throw new BusinessLogicException(ErrorMessages.CantGetInfoForAccount, ex);
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
                throw new BusinessLogicException(ErrorMessages.CantEditInfoForAccount, ex);
            }
        }

        public BankAccountDonateViewModel ConnectDonateFunction(BankAccountDonateViewModel info)
        {
            var bankAccount = _unitOfWork.BankAccountRepository.Get(info.BankAccountId);

            if (bankAccount != null)
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

            if (orgAccount != null && orgAccount.BankAccId.HasValue)
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

        public BankAccountDonateViewModel ExtractCredentials(int orgAccountId)
        {
            var bankAccount = _unitOfWork.OrganizationAccountRepository.GetOrgAccountById(orgAccountId)?.BankAccount;
            if (bankAccount == null)
            {
                throw new BusinessLogicException(ErrorMessages.CantGetInfoForAccount);
            }

            var result = new BankAccountDonateViewModel()
            {
                BankAccountId = bankAccount.Id,
                MerchantId = bankAccount.ExtractMerchantId.Value,
                MerchantPassword = bankAccount.ExtractMerchantPassword
            };
            return result;
        }

        public bool IsExtractsEnabled(int orgAccountId)
        {
            OrgAccount orgAccount = _unitOfWork.OrganizationAccountRepository.GetOrgAccountById(orgAccountId);
            return orgAccount?.BankAccount?.IsExtractEnabled ?? false;
        }

        public BankAccountDonateViewModel ConnectExtractsFunction(BankAccountDonateViewModel info)
        {
            var bankAccount = _unitOfWork.BankAccountRepository.Get(info.BankAccountId);

            if (bankAccount != null)
            {
                bankAccount.ExtractMerchantId = (int)info.MerchantId;
                bankAccount.ExtractMerchantPassword = info.MerchantPassword;
                bankAccount.IsExtractEnabled = true;

                _unitOfWork.BankAccountRepository.Update(bankAccount);
                _unitOfWork.SaveChanges();

                return info;
            }
            else
            {
                throw new BusinessLogicException(ErrorMessages.BadRequestMessage);
            }
        }

        public bool ToggleExtractsFunction(int orgAccountId)
        {
            var orgAccount = _unitOfWork.OrganizationAccountRepository.GetOrgAccountById(orgAccountId);

            try
            {
                var bankAccount = _unitOfWork.BankAccountRepository.Get((int)orgAccount.BankAccId);
                bankAccount.IsExtractEnabled = !bankAccount.IsExtractEnabled;

                _unitOfWork.BankAccountRepository.Update(bankAccount);
                _unitOfWork.SaveChanges();

                return (bool)bankAccount.IsExtractEnabled;
            }
            catch (Exception ex)
            {
                throw new BusinessLogicException(ErrorMessages.CantEditInfoForAccount, ex);
            }

        }

        public BankAccountDonateViewModel DisableExtractsFunction(int bankAccountId)
        {
            var bankAccount = _unitOfWork.BankAccountRepository.Get(bankAccountId);

            if (bankAccount != null)
            {
                bankAccount.ExtractMerchantId = null;
                bankAccount.ExtractMerchantPassword = null;
                bankAccount.IsExtractEnabled = false;

                _unitOfWork.BankAccountRepository.Update(bankAccount);
                _unitOfWork.SaveChanges();

                return new BankAccountDonateViewModel
                {
                    BankAccountId = bankAccountId
                };
            }
            else
            {
                return null;
            }
        }

        public bool IsExtractsConnected(int orgAccountId)
        {
            var bankAccount = _unitOfWork.OrganizationAccountRepository.GetOrgAccountById(orgAccountId)?.BankAccount;

            if (bankAccount == null)
            {
                return false;
            }
            if (bankAccount.ExtractMerchantId.HasValue &&
                    !String.IsNullOrEmpty(bankAccount.ExtractMerchantPassword))
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }
}
