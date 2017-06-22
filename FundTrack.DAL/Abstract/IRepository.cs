using System.Collections.Generic;

namespace FundTrack.DAL.Abstract
{
    /// <summary>
    /// Interface IRepository for work whith data base. Realized CRUD.
    /// </summary>
    /// <typeparam name="T">Only classes</typeparam>
    public interface IRepository<T> where T : class
    {
        /// <summary>
        /// Gets all entries in database
        /// </summary>
        /// <returns> Collection all entries </returns>
        IEnumerable<T> Read();

        /// <summary>
        /// Get one entry for id
        /// </summary>
        /// <param name="id"> Recives id of entry </param>
        /// <returns> one entry <T> </returns>
        T Get(int id);


        /// <summary>
        /// Creates the specified item.
        /// </summary>
        /// <param name="item"> Recives new item of <T> </param>
        T Create(T item);

        /// <summary>
        /// Updates information in entry
        /// </summary>
        /// <param name="item"> Recives new or changed item of <T> </param>
        T Update(T item);

        /// <summary>
        /// Deletes entry from data base
        /// </summary>
        /// <param name="id"> Recives id of entry </param>
        void Delete(int id);
    }
}
