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
        IOrganizationsForFilteringRepository OrganizationsForFilteringRepository { get; }
        /// <summary>
        /// Gets the users repository.
        /// </summary>
        /// <value>
        /// The users repository.
        /// </value>
        IUserResporitory UsersRepository { get; }
        
        /// <summary>
        /// Gets the event repository.
        /// </summary>
        /// <value>
        /// The event repository.
        /// </value>
        IRepository<Event> EventRepository { get; }

        /// <summary>
        /// Gets the organization repository
        /// </summary>
        IOrganizationRepository OrganizationRepository { get; }

        /// <summary>
        /// Gets the membership repository.
        /// </summary>
        /// <value>
        /// The membership repository.
        /// </value>
        IMembershipRepository MembershipRepository { get; }

        /// <summary>
        /// Saves all changes made in this context to the database.
        /// </summary>
        void SaveChanges();
    }
}
