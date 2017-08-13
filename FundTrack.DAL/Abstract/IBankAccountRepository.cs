using FundTrack.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FundTrack.DAL.Abstract
{
    public interface IBankAccountRepository
    {
        BankAccount GetBankAccountById(int accountId);
        IEnumerable<BankAccount> GetOrganizationBankAccounts(int orgId);
        BankAccount UpdateBankAccount(int accountId);
        IEnumerable<BankAccount> GetAllBankAccounts();
        void DeleteBankAccount(int accountId);

    }
}
