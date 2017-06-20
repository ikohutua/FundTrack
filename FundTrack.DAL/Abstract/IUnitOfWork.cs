using FundTrack.DAL.Entities;

namespace FundTrack.DAL.Abstract
{
    /// <summary>
    /// Interface for one unit of work whis repositories
    /// </summary>
    public interface IUnitOfWork
    {
        /// <summary>
        /// Gets the organizations list repository.
        /// </summary>
        /// <value>
        /// The organizations list repository.
        /// </value>
        IOrganizationsListRepository OrganizationsListRepository { get; }
        /// <summary>
        /// Gets the users repository.
        /// </summary>
        /// <value>
        /// The users repository.
        /// </value>
        IRepository<User> UsersRepository {get;}
        /// <summary>
        /// Saves all changes made in this context to the database.
        /// </summary>
        void SaveChanges();
    }
}
