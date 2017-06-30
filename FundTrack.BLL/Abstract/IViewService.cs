using System.Collections.Generic;

namespace FundTrack.BLL.Abstract
{
    /// <summary>
    /// Interface to work with Events
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public interface IViewService<T>
    {
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>Collections of instances</returns>
        IEnumerable<T> Get();

        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns>Collections of specific instances</returns>
        IEnumerable<T> Get(int id);

        /// <summary>
        /// Reads the by identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        /// <returns>the specified item</returns>
        T ReadById(int id);

        /// <summary>
        /// Deletes the specified identifier.
        /// </summary>
        /// <param name="id">The identifier.</param>
        void Delete(int id);

        /// <summary>
        /// Adds the specified new item.
        /// </summary>
        /// <param name="newItem">The new item.</param>
        void Add(T newItem);

        /// <summary>
        /// Updates the specified update t.
        /// </summary>
        /// <param name="updateT">The update t.</param>
        void Update(T updateT);
    }
}
