using FundTrack.BLL.Abstract;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using FundTrack.DAL.Entities;
using FundTrack.Infrastructure.ViewModel;
using FundTrack.DAL.Abstract;

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
            try
            {
                OrgAccount orgAccount = (OrgAccount)model;
                orgAccount.Organization = this._unitOfWork.OrganizationRepository.Get(model.OrganizationId);
                orgAccount.BankAccount = this._unitOfWork.BankAccountRepository.Get((int)model.BankAccId);
                orgAccount.Currency = this._unitOfWork.CurrencyRepositry.Get(model.CurrencyId);
                this._unitOfWork.SaveChanges();
                return (OrgAccountViewModel)orgAccount;
            }
            catch (Exception e)
            {
                string message = string.Format("Рахунок організації не створено. Помилка: {0}", e.Message);
                throw new BusinessLogicException(message, e);
            }
        }

        public void DeleteOrganizationAccount(int organizationAccountId)
        {
            try
            {
                this._unitOfWork.OrganizationAccountRepository.Delete(organizationAccountId);
                this._unitOfWork.SaveChanges();
            }
            catch (Exception e)
            {
                string message = string.Format("Рахунок організації не видалено. Помилка: {0}", e.Message);
                throw new BusinessLogicException(message, e);
            }
        }

        public IEnumerable<OrgAccountViewModel> GetAccountsByOrganizationId(int organizationId)
        {
            try
            {
                var accounts= this._unitOfWork.OrganizationAccountRepository.ReadAllOrgAccounts(organizationId);
                var accountsModels = new List<OrgAccountViewModel>();
                foreach (var item in accounts)
                {
                    accountsModels.Add(item);
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
                return this._unitOfWork.OrganizationAccountRepository.Read(organizationAccountId);
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
                var orgAccount= this._unitOfWork.OrganizationAccountRepository.Edit(model);
                orgAccount.Organization = this._unitOfWork.OrganizationRepository.Get(model.OrganizationId);
                orgAccount.BankAccount = this._unitOfWork.BankAccountRepository.Get((int)model.BankAccId);
                orgAccount.Currency = this._unitOfWork.CurrencyRepositry.Get(model.CurrencyId);
                this._unitOfWork.SaveChanges();
                return orgAccount;
            }
            catch (Exception e)
            {
                string message = string.Format("Неможливо оновити рахунок організації. Помилка: {0}", e.Message);
                throw new BusinessLogicException(message, e);
            }
        }
    }
}
