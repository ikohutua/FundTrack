using FundTrack.DAL.Entities;
using System.Collections.Generic;

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
