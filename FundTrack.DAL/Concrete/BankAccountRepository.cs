using FundTrack.DAL.Abstract;
using FundTrack.DAL.Entities;
using System.Collections.Generic;
using System.Linq;

namespace FundTrack.DAL.Concrete
{
    /// <summary>
    /// Bank account repository
    /// </summary>
    public class BankAccountRepository :IRepository<BankAccount>
    {
        private readonly FundTrackContext _context;

        /// <summary>
        /// Creates new instance of bank account repository
        /// </summary>
        /// <param name="context">Db context</param>
        public BankAccountRepository(FundTrackContext context)
        {
            _context = context;
        }

        /// <summary>
        /// Creates bank account.
        /// </summary>
        /// <param name="item">Item to create</param>
        /// <returns></returns>
        public BankAccount Create(BankAccount item)
        {
            var bankAccount = _context.BankAccounts.Add(item);
            return bankAccount.Entity;
        }

        /// <summary>
        /// Deletes bank account from data base
        /// </summary>
        /// <param name="id">Recives id of bank account</param>
        public void Delete(int id)
        {
            BankAccount _bankAccount = _context.BankAccounts.FirstOrDefault(c => c.Id == id);
            if (_bankAccount != null)
                _context.BankAccounts.Remove(_bankAccount);
        }

        /// <summary>
        /// Gets bank account by id.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns></returns>
        public BankAccount Get(int id)
        {
            return _context.BankAccounts.FirstOrDefault(e => e.Id == id);
        }

        public BankAccount Get(int? bankAccId)
        {
            int? id = 0;
            id = bankAccId;
            return _context.BankAccounts.FirstOrDefault(e => e.Id == id);
        }

        /// <summary>
        /// Gets all bank account in database
        /// </summary>
        /// <returns>
        /// Collection all bank account
        /// </returns>
        public IEnumerable<BankAccount> Read()
        {
            return _context.BankAccounts;
        }

        /// <summary>
        /// Updates the specified bank account.
        /// </summary>
        /// <param name="item">Bank account to update.</param>
        public BankAccount Update(BankAccount item)
        {
            var updated = _context.BankAccounts.Update(item);
            return updated.Entity;
        }
    }
}
