using FundTrack.DAL.Entities;

namespace FundTrack.DAL.Abstract
{
    public interface IPhoneRepository
    {
        /// <summary>
        /// Add new phone to database.
        /// </summary>
        /// <param name="phone">The new phone.</param>
        /// <returns></returns>
        Phone Add(Phone phone);

        /// <summary>
        /// Get phone number by user id
        /// </summary>
        /// <param name="userId">The user id.</param>
        /// <returns>user number or empty string</returns>
        Phone GetPhoneByUserId(int userId);

        /// <summary>
        /// Edit phone
        /// </summary>
        /// <param name="phone">The phone for edit.</param>
        /// <returns>updated phone</returns>
        Phone Update(Phone phone);
    }
}
