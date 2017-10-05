using System.Collections.Generic;
using FundTrack.DAL.Entities;

namespace FundTrack.DAL.Abstract
{
    public interface IBankRepository
    {
        /// <summary>
        /// Get all banks from database
        /// </summary>
        IEnumerable<Bank> Read();

        /// <summary>
        /// Get bank by Id
        /// </summary>
        Bank Get(int bankId);

        /// <summary>
        /// Add new bank
        /// </summary>
        Bank Create(Bank item);

        /// <summary>
        /// Update existing bank
        /// </summary>
        Bank Update(Bank bank);

        /// <summary>
        /// Delete bank by Id
        /// </summary>
        void Delete(int bankId);
    }
}
